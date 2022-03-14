import pandas as pd
from pymongo import MongoClient

db = {}

def connect_to_database():
   print("Creating client in connect_to_database")
   client = MongoClient('mongo', 27017, username="test", password="test")
  
   
   global db
   
   print("Grabbing test_database in connect_to_database")
   db = client.test_database
   
   print("Returning test_database in connect_to_database")
   return db

def connect_to_database_outside_docker():
   client = MongoClient('localhost', 27017, username="test", password="test")

   global db
   db = client.test_database
   return db


def add_data(data):
   print("Getting posts in database in add_data")
   posts = db.posts

   print("Grabbing post_id using data in add_data")
   post_id = posts.insert_one(data).inserted_id
   
   print("Post ID: " + str(post_id))   
    

def get_dataframe():   
   print("Initializing cursor in get_dataframe")
   cursor = db.posts.find() # Expand the cursor and construct the DataFrame
   
   print(cursor)
   
   print("Creating pandas Dataframe in get_dataframe")
   df = pd.DataFrame(list(cursor))
   pd.set_option('display.max_rows', df.shape[0]+1)
   
   return df




