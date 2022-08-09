from PyQt5.QtGui import *
from PyQt5.QtWidgets import *
from PyQt5.QtCore import *
import cv2
import sys
import time

from core.face_recognize import FaceRecognize
from core.face_capture import FaceCapture
from core.face_update import FaceUpdate
from services.user_service import UserService

class RecognizeWorker(QThread):
    image_update = pyqtSignal(QImage)
    last_time = time.time()
    user_service = UserService()
    name = ""
    
    def run(self):
        self.thread_active = True
        cap = cv2.VideoCapture(0)
        face_recognizer = FaceRecognize(cap)
        
        while self.thread_active:
            ret, frame = cap.read()
            if ret:
                now = time.time()
                if (now - self.last_time > 5):
                    self.name = face_recognizer.recognize_face(frame)
                    self.last_time = now
                    self.register()
                image = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)
                convert_qtFormat = QImage(image.data, image.shape[1], image.shape[0], QImage.Format_RGB888)
                pic = convert_qtFormat.scaled(640, 480, Qt.KeepAspectRatio)
                self.image_update.emit(pic)
    
    def register(self):
        res = self.user_service.register(self.name)
        
        if res and res.ok:
            print(self.name + " registered")
        else:
            print("Register failed!")
                
    def stop(self):
        self.thread_active = False
        self.quit()

class CaptureWorker(QThread):
    image_update = pyqtSignal(QImage)
    progress = 0
    userId = ""
    
    def run(self):
        if not self.userId:
            self.stop()
            return
        
        self.thread_active = True
        cap = cv2.VideoCapture(0)
        face_capture = FaceCapture(self.userId, cap)
        face_update = FaceUpdate()
        
        while self.thread_active and face_capture.count:
            ret, frame = cap.read()
            if ret:
                face_capture.capture(frame)
                image = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)
                self.progress = (1 - face_capture.count/face_capture.MAX_COUNT)
                convert_qtFormat = QImage(image.data, image.shape[1], image.shape[0], QImage.Format_RGB888)
                pic = convert_qtFormat.scaled(640, 480, Qt.KeepAspectRatio)
                self.image_update.emit(pic)
        
        self.progress = 1
        face_update.update()
        self.stop()
                
    def stop(self):
        self.thread_active = False
        self.quit()

class UpdateFaceWorker(QThread):
    def run(self):
        self.thread_active = True
        face_update = FaceUpdate()
        
        
        if self.thread_active:
            res = face_update.update()
            
        print(res)
        self.stop()
                
    def stop(self):
        self.thread_active = False
        self.quit()