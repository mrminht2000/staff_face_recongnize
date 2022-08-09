from PyQt5 import QtCore, QtGui, QtWidgets
from PyQt5.QtGui import QPixmap
from PyQt5.QtWidgets import QLineEdit, QDialogButtonBox, QFormLayout, QDialog, QMessageBox, QComboBox
from services.workers import RecognizeWorker, CaptureWorker, UpdateFaceWorker
from ui.login_form import LoginDialog
from services.user_service import UserService
from core.face_update import FaceUpdate

class Ui_MainWindow(object):
    user_service = UserService()
    capture_worker = CaptureWorker()
    recognize_worker = RecognizeWorker()
    update_face_worker = UpdateFaceWorker()
    
    def setupUi(self, MainWindow):
        MainWindow.setObjectName("MainWindow")
        MainWindow.resize(710, 646)
        MainWindow.setAcceptDrops(False)
        
        self.centralwidget = QtWidgets.QWidget(MainWindow)
        self.centralwidget.setObjectName("centralwidget")
        
        self.tabWidget = QtWidgets.QTabWidget(self.centralwidget)
        self.tabWidget.setGeometry(QtCore.QRect(0, 0, 701, 611))
        self.tabWidget.setObjectName("tabWidget")
        
        # Tab Capture
        self.capture = QtWidgets.QWidget()
        self.capture.setObjectName("capture")
        
        self.progressBar = QtWidgets.QProgressBar(self.capture)
        self.progressBar.setGeometry(QtCore.QRect(540, 10, 118, 31))
        self.progressBar.setProperty("value", 0)
        self.progressBar.setObjectName("progressBar")
        
        self.get_users_btn = QtWidgets.QPushButton(self.capture)
        self.get_users_btn.setGeometry(QtCore.QRect(10, 10, 81, 31))
        self.get_users_btn.setObjectName("get_users_btn")
        
        self.user_input = QtWidgets.QComboBox(self.capture)
        self.user_input.setGeometry(QtCore.QRect(100, 10, 231, 31))
        self.user_input.setObjectName("user_input")
        
        self.capture_btn = QtWidgets.QPushButton(self.capture)
        self.capture_btn.setGeometry(QtCore.QRect(350, 10, 71, 31))
        self.capture_btn.setObjectName("capture_btn")
        
        self.update_btn = QtWidgets.QPushButton(self.capture)
        self.update_btn.setGeometry(QtCore.QRect(440, 10, 71, 31))
        self.update_btn.setObjectName("update_btn")
        
        self.capture_camera = QtWidgets.QLabel(self.capture)
        self.capture_camera.setGeometry(QtCore.QRect(20, 80, 640, 480))
        self.capture_camera.setObjectName("capture_camera")
        
        self.capture_btn.clicked.connect(self.start_capture)
        self.update_btn.clicked.connect(self.update_face)
        self.get_users_btn.clicked.connect(self.get_users)
        
        self.tabWidget.addTab(self.capture, "")
        
        # Tab Recognize
        self.recognize = QtWidgets.QWidget()
        self.recognize.setObjectName("recognize")
        
        self.start_btn = QtWidgets.QPushButton(self.recognize)
        self.start_btn.setGeometry(QtCore.QRect(10, 10, 131, 28))
        self.start_btn.setObjectName("start_btn")
        
        self.stop_btn = QtWidgets.QPushButton(self.recognize)
        self.stop_btn.setGeometry(QtCore.QRect(150, 10, 93, 28))
        self.stop_btn.setObjectName("stop_btn")
        
        self.userId_display = QtWidgets.QLabel(self.recognize)
        self.userId_display.setGeometry(QtCore.QRect(470, 10, 191, 31))
        self.userId_display.setObjectName("userId_display")
        
        self.userId_display_label = QtWidgets.QLabel(self.recognize)
        self.userId_display_label.setGeometry(QtCore.QRect(350, 20, 131, 16))
        self.userId_display_label.setObjectName("userId_display_label")
        
        self.recognize_camera = QtWidgets.QLabel(self.recognize)
        self.recognize_camera.setGeometry(QtCore.QRect(20, 80, 640, 480))
        self.recognize_camera.setObjectName("recognize_camera")
        
        self.start_btn.clicked.connect(self.start_recognize)
        self.stop_btn.clicked.connect(self.cancel_recognize)
        
        self.tabWidget.addTab(self.recognize, "")
        
        # Menubar
        MainWindow.setCentralWidget(self.centralwidget)
        self.menubar = QtWidgets.QMenuBar(MainWindow)
        self.menubar.setGeometry(QtCore.QRect(0, 0, 710, 26))
        self.menubar.setObjectName("menubar")
        
        self.menuAccount = QtWidgets.QMenu(self.menubar)
        self.menuAccount.setObjectName("menuAccount")
        
        MainWindow.setMenuBar(self.menubar)
        
        self.actionLog_In = QtWidgets.QAction(MainWindow)
        self.actionLog_In.setObjectName("actionLog_In")
        
        self.menuAccount.addSeparator()
        self.menuAccount.addAction(self.actionLog_In)
        
        self.menubar.addAction(self.menuAccount.menuAction())
        
        self.actionLog_In.triggered.connect(self.log_in_action)
        
        # Retranslate UI
        self.retranslateUi(MainWindow)
        self.tabWidget.setCurrentIndex(1)
        QtCore.QMetaObject.connectSlotsByName(MainWindow)

    def retranslateUi(self, MainWindow):
        _translate = QtCore.QCoreApplication.translate
        MainWindow.setWindowTitle(_translate("MainWindow", "Face Recognize App"))
        self.get_users_btn.setText(_translate("MainWindow", "Get users"))
        self.capture_btn.setText(_translate("MainWindow", "Capture"))
        self.update_btn.setText(_translate("MainWindow", "Update"))
        self.tabWidget.setTabText(self.tabWidget.indexOf(self.capture), _translate("MainWindow", "Capture Face"))
        self.start_btn.setText(_translate("MainWindow", "Start recognizing"))
        self.stop_btn.setText(_translate("MainWindow", "Stop"))
        self.userId_display_label.setText(_translate("MainWindow", "Recognized UserId:"))
        self.tabWidget.setTabText(self.tabWidget.indexOf(self.recognize), _translate("MainWindow", "Recognize Face"))
        self.menuAccount.setTitle(_translate("MainWindow", "Account"))
        self.actionLog_In.setText(_translate("MainWindow", "Log In"))
    
    def recognize_update(self, Image):
        self.recognize_camera.setPixmap(QPixmap.fromImage(Image))
        self.userId_display.setText(self.recognize_worker.name)
        
    def start_recognize(self):
        self.recognize_worker.start()
        self.recognize_worker.image_update.connect(self.recognize_update)

    def cancel_recognize(self):
        self.recognize_worker.stop()
        
    def capture_update(self, Image):
        self.capture_camera.setPixmap(QPixmap.fromImage(Image))
        self.progressBar.setProperty("value", self.capture_worker.progress * 100)
        
    def start_capture(self):
        userName = self.user_input.currentText()
        if userName :
            self.capture_worker.userId = userName
            self.capture_worker.start()
            self.capture_worker.image_update.connect(self.capture_update)
        else:
            msg = QMessageBox()
            msg.setIcon(QMessageBox.Information)
            msg.setText("Please enter UserName")
            msg.setWindowTitle("Error")
            msg.setStandardButtons(QMessageBox.Cancel)
            msg.exec_()
    
    def log_in_action(self):
        dlg = LoginDialog()
        dlg.exec()
        
    def get_users(self):
        res = self.user_service.get_users()
        
        if res.ok:
            self.users = []
            for user in res.json()['users']:
                self.users.append(user['userName'])
            
            self.user_input.addItems(self.users)
        else:
            QMessageBox.warning(self, 'Error ' + res.json()['Code'], res.json()['Message'])
    
    def update_face(self):
        self.update_face_worker.start()

if __name__ == "__main__":
    import sys
    app = QtWidgets.QApplication(sys.argv)
    MainWindow = QtWidgets.QMainWindow()
    ui = Ui_MainWindow()
    ui.setupUi(MainWindow)
    MainWindow.show()
    sys.exit(app.exec_())

