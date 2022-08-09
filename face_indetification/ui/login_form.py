import dotenv
import os

from PyQt5.QtWidgets import QLineEdit,QDialogButtonBox,QFormLayout,QDialog,QMessageBox
from PyQt5 import QtWidgets
from PyQt5.QtCore import Qt
from PyQt5 import QtGui

from services.user_service import UserService

class LoginDialog(QDialog):
    dotenv_file = dotenv.find_dotenv()
    dotenv.load_dotenv(dotenv_file) 
    
    def __init__(self, parent=None):
        super(LoginDialog,self).__init__(parent)
        self.init_ui()
        self.user_service = UserService()

    def init_ui(self):
        ### delete question mark
        self.setWindowFlags(self.windowFlags()
                            ^ Qt.WindowContextHelpButtonHint)

        ### login & password fields
        self.username = QLineEdit(self)
        self.password = QLineEdit(self)
        self.password.setEchoMode(QLineEdit.Password)
        loginLayout = QFormLayout()
        loginLayout.addRow("Username", self.username)
        loginLayout.addRow("Password", self.password)
        self.buttons = QDialogButtonBox(QDialogButtonBox.Ok | QDialogButtonBox.Cancel)
        self.buttons.accepted.connect(self.control)
        self.buttons.rejected.connect(self.reject)
        layout = QtWidgets.QVBoxLayout(self)
        layout.addLayout(loginLayout)
        layout.addWidget(self.buttons)
        self.setLayout(layout)

        ### set window title & stylesheet
        self.setWindowTitle('Login Box')
        ###lock resize
        self.setSizeGripEnabled(False)
        self.setFixedSize(self.sizeHint())


    ###log by usins sql credentials
    def control(self):
        username = self.username.text()
        password = self.password.text()
        res = self.user_service.log_in(username, password)
        
        if res.ok:
            dotenv.set_key(self.dotenv_file, 'AUTHORIZE_TOKEN', res.json()['token'])
            self.accept()
        else:
            QMessageBox.warning(self, 'Error', res.json()['Message'])