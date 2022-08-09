import cv2
from facenet_pytorch import MTCNN, InceptionResnetV1, fixed_image_standardization
import torch
from torchvision import transforms
import numpy as np
from PIL import Image
import time

class FaceRecognize:
    frame_size = (640,480)
    power = pow(10, 6)
    IMG_PATH = './data/user_images'
    DATA_PATH = './data'
    device = torch.device('cuda:0' if torch.cuda.is_available() else 'cpu')

    model = InceptionResnetV1(
        classify=False,
        pretrained="casia-webface"
        #pretrained="vggface2"
    ).to(device)
    model.eval()

    mtcnn = MTCNN(thresholds= [0.7, 0.7, 0.8] ,keep_all=True, device = device)

    def __init__(self, cap = cv2.VideoCapture(0)):
        self.cap = cap
        self.cap.set(cv2.CAP_PROP_FRAME_WIDTH,640)
        self.cap.set(cv2.CAP_PROP_FRAME_HEIGHT,480)

        self.embeddings, self.names = self.load_faceslist()

    def trans(self, img):
        transform = transforms.Compose([
                transforms.ToTensor(),
                fixed_image_standardization
            ])
        return transform(img)

    def load_faceslist(self):
        if self.device == 'cpu':
            embeds = torch.load(self.DATA_PATH+'/faceslistCPU.pth')
        else:
            embeds = torch.load(self.DATA_PATH+'/faceslist.pth')
        names = np.load(self.DATA_PATH+'/usernames.npy')
        return embeds, names

    def inference(self, model, face, local_embeds, threshold = 3):
        #local: [n,512] voi n la so nguoi trong faceslist
        embeds = []
        # print(trans(face).unsqueeze(0).shape)
        embeds.append(self.model(self.trans(face).to(self.device).unsqueeze(0)))
        detect_embeds = torch.cat(embeds) #[1,512]
        # print(detect_embeds.shape)
                        #[1,512,1]                                      [1,512,n]
        norm_diff = detect_embeds.unsqueeze(-1) - torch.transpose(local_embeds, 0, 1).unsqueeze(0)
        # print(norm_diff)
        norm_score = torch.sum(torch.pow(norm_diff, 2), dim=1) #(1,n), moi cot la tong khoang cach euclide so vs embed moi
        
        min_dist, embed_idx = torch.min(norm_score, dim = 1)
        print(min_dist*self.power, self.names[embed_idx])
        # print(min_dist.shape)
        if min_dist*self.power > threshold:
            return -1, -1
        else:
            return embed_idx, min_dist.double()

    def extract_face(self, box, img, margin=20):
        face_size = 160
        img_size = self.frame_size
        margin = [
            margin * (box[2] - box[0]) / (face_size - margin),
            margin * (box[3] - box[1]) / (face_size - margin),
        ] #tạo margin bao quanh box cũ
        box = [
            int(max(box[0] - margin[0] / 2, 0)),
            int(max(box[1] - margin[1] / 2, 0)),
            int(min(box[2] + margin[0] / 2, img_size[0])),
            int(min(box[3] + margin[1] / 2, img_size[1])),
        ]
        img = img[box[1]:box[3], box[0]:box[2]]
        face = cv2.resize(img,(face_size, face_size), interpolation=cv2.INTER_AREA)
        face = Image.fromarray(face)
        return face

    def recognize_face(self, frame):
        boxes, _ = self.mtcnn.detect(frame)
        if boxes is not None:
            for box in boxes:
                bbox = list(map(int,box.tolist()))
                face = self.extract_face(bbox, frame)
                idx, score = self.inference(self.model, face, self.embeddings)
                if idx != -1:
                    return self.names[idx]
                else:
                    return "Unknown"
