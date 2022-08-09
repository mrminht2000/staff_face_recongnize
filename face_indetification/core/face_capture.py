import cv2
from facenet_pytorch import MTCNN
import torch
from datetime import datetime
import os

class FaceCapture:
    device =  torch.device('cuda:0' if torch.cuda.is_available() else 'cpu')

    IMG_PATH = './data/user_images/'
    MAX_COUNT = 50
    count = MAX_COUNT
    leap = 1
    mtcnn = MTCNN(margin = 20, keep_all=False, post_process=False, device = device)

    def __init__(self, user_iden, cap = cv2.VideoCapture(0), src = 0):
        self.usr_name = user_iden
        self.USR_PATH = os.path.join(self.IMG_PATH, self.usr_name)

        self.cap = cap
        self.cap.set(cv2.CAP_PROP_FRAME_WIDTH, 640)
        self.cap.set(cv2.CAP_PROP_FRAME_HEIGHT, 480)

    def save_frame(self, frame):
        if self.mtcnn(frame) is not None and self.leap%2:
            path = str(self.USR_PATH+'/{}.jpg'.format(str(datetime.now())[:-7].replace(":","-").replace(" ","-")+str(self.count)))
            frame = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)
            face_img = self.mtcnn(frame, save_path = path)
            self.count-=1

    def capture(self, frame):
        self.save_frame(frame)
        self.leap += 1
    