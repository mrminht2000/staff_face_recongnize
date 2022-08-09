import os
import requests
import pytz
from datetime import datetime
from dotenv import load_dotenv

class UserService:
    load_dotenv()
    API_URL = os.getenv('API_URL')
    AUTHORIZE_TOKEN = os.getenv('AUTHORIZE_TOKEN')
    auth_headers = {'Authorization' : 'Bearer ' + AUTHORIZE_TOKEN}
    
    def log_in(self, username, password):
        req_body = {'userName': username,
                    'password': password}
    
        res = requests.post(self.API_URL + '/api/authentication', json = req_body)
        return res
        
    def get_users(self):
        res = requests.get(self.API_URL + '/api/user', headers = self.auth_headers)
        
        return res
    
    def register(self, userName):
        if not userName or userName == 'Unknown':
            return 
        
        vn_tz = pytz.timezone('Asia/Ho_Chi_Minh')
        req_body = {'userName': userName,
                    'startTime': datetime.utcnow().strftime("%m/%d/%Y %H:%M:%S")}
        
        res = requests.post(self.API_URL + '/api/event/register', json = req_body, headers = self.auth_headers)
        
        return res
        