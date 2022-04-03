import game_analytics
import requests

def test_post_data():
   url = 'http://34.242.150.74/upload_data'
   payload = {"completion_time": 2000,
              "level": 7
               }
   r = requests.post(url, json=payload)
   print(r) 

def test_get_data():
   #game_analytics.connect_to_database_outside_docker()
   print("Connecting to database in test_get_data")
   game_analytics.connect_to_database_outside_docker()
   
   df = game_analytics.get_dataframe()
   print(df)
   
def test_get_mean_data():
    game_analytics.connect_to_database_outside_docker()
    print()
    
    mean = game_analytics.get_mean_completion_time()
    print("*******************************************************")
    print("average completion time is " + str(mean))
    print("*******************************************************")
    
    distance = game_analytics.get_mean_distance()
    print("*******************************************************")
    print("average distance to level exit is " + str(distance))
    print("*******************************************************")
    
    #averageDamage = 
    damage = game_analytics.get_mean_damage_dealt()
    print("*******************************************************")
    print("average bomber damage is " + str(damage[0]))
    print("average follower damage is " + str(damage[1]))
    print("average runner damage is " + str(damage[2]))
    print("average boss damage is " + str(damage[3]))
    print("*******************************************************")
    

#test_post_data()
test_get_data()
test_get_mean_data()