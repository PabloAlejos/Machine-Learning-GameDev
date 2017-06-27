import json 
import requests

TOKEN = "371099199:AAFy_hyhXjc4Ew8pGS1engI6RdJ3YDT0VSU" #token del bot
URL = "https://api.telegram.org/bot{}/".format(TOKEN) #api de telgram

def get_url(url):
    response = requests.get(url)
    content = response.content.decode("utf8")
    return content

def get_json_from_url(url):
    content = get_url(url)
    js = json.loads(content)
    return js

def get_updates():
    url = URL + "getUpdates"
    js = get_json_from_url(url)
    return js

def get_last_chat_id_and_text(updates):
    num_updates = len(updates["result"])
    last_update = num_updates - 1
    text = updates["result"][last_update]["message"]["text"]
    chat_id = updates["result"][last_update]["message"]["chat"]["id"]
    return (text, chat_id)
    print (get_last_chat_id_and_text(updates))

#send_message("Hola!", 4191538)