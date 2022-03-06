from flask import Flask, request, Blueprint, make_response
import json
import datetime
import game_analytics

api_basic_upload = Blueprint('api_basic_upload', __name__)

@api_basic_upload.route('/upload_data', methods=['POST'])
def post_upload():
 try:
   print("Grabbing data via POST method in post_upload")
   data = request.get_json()
   print("Adding timestamp to data dict in post_upload")
   data['timestamp'] = datetime.datetime.utcnow()
   print(data)   
   
   print("Attempting connection to database in post_upload")
   game_analytics.connect_to_database()
   
   print("Attempting to add data to database in post_upload")
   game_analytics.add_data(data)
   
   print("Attempts successful, returning success.")
   return "{'status': 'success'}"
 except Exception as e:
   print(e)
   resp = make_response(str(e), 500)
   return resp
