import glob
import torch 
from torchvision import transforms
from facenet_pytorch import InceptionResnetV1, fixed_image_standardization
import os
from PIL import Image
import numpy as np


class FaceUpdate:
    IMG_PATH = './data/user_images'
    DATA_PATH = './data'

    device =  torch.device('cuda:0' if torch.cuda.is_available() else 'cpu')

    model = InceptionResnetV1(
            classify=False,
            pretrained="casia-webface"
            #pretrained="vggface2"  
        ).to(device)
    model.eval()

    def trans(self, img):
        transform = transforms.Compose([
                transforms.ToTensor(),
                fixed_image_standardization
            ])
        return transform(img)
    
    def update(self):
        embeddings = []
        names = []

        for usr in os.listdir(self.IMG_PATH):
            embeds = []
            for file in glob.glob(os.path.join(self.IMG_PATH, usr)+'/*.jpg'):
                # print(usr)
                try:
                    img = Image.open(file)
                except:
                    continue
                with torch.no_grad():
                    # print('smt')
                    embeds.append(self.model(self.trans(img).to(self.device).unsqueeze(0))) #1 anh, kich thuoc [1,512]
            if len(embeds) == 0:
                continue
            embedding = torch.cat(embeds).mean(0, keepdim=True) #dua ra trung binh cua 30 anh, kich thuoc [1,512]
            embeddings.append(embedding) # 1 cai list n cai [1,512]
            # print(embedding)
            names.append(usr)
            
        embeddings = torch.cat(embeddings) #[n,512]
        names = np.array(names)

        if self.device == 'cpu':
            torch.save(embeddings, self.DATA_PATH+"/faceslistCPU.pth")
        else:
            torch.save(embeddings, self.DATA_PATH+"/faceslist.pth")
        np.save(self.DATA_PATH+"/usernames", names)
        
        return ('Update Completed! There are {0} people in FaceLists'.format(names.shape[0]))