#Cambiar el get mood para que devuelva el un numero en vez de un vector,
#Cambiar el dominio para que tambien devuelva un numero en vez del vector. 
#Verificar que la multiplicacion del get_mood de un numero razonable. 
#Agregar al dispatcher y al dominio que tambien devuelve la emocion_receptor en formato text.
        























from typing import Any, Text, Dict, List

from rasa_sdk import Action, Tracker
from rasa_sdk.executor import CollectingDispatcher
from rasa_sdk.events import SlotSet
import json

class ActionIntensidad(Action):
    """Esta accion se encarga de seleccionar la respuesta determinada al intent detectado por rasa, acorde a la persona que interpreta el asistente"""

    def name(self) -> Text:
        return "accion_intensidad"

    def run(self, dispatcher: CollectingDispatcher,
        tracker: Tracker,
        domain: Dict[Text, Any]) -> List[Dict[Text, Any]]:
         
        ultimo_intent =  tracker.latest_message['intent'].get('name')
  
        emocion_receptor = tracker.current_state()['latest_message']['response_selector']['default']['response']['responses'][0]['text'].split('/')[1]

        sender_id = tracker.current_state()['sender_id']

        print("Sender: ", sender_id)
        print("Intent detectado: ", ultimo_intent)
        print("emocion_receptor detectada: ", emocion_receptor)

        accion_a_responder = "utter_" + ultimo_intent + "_" + emocion_receptor + "_" + sender_id
        
        index = self.get_mood(emocion_receptor)
        print("INDEX: ", index)
        intensidad_receptor = self.calcularintensidad_receptor(sender_id, index)
        

        print("intensidad: ", intensidad_receptor)
        print("Accion a responder: ", accion_a_responder) 

        dispatcher.utter_message(response=accion_a_responder, intensidad_receptor=intensidad_receptor, emocion_receptor=emocion_receptor)

        return []

    def calcularintensidad_receptor(self, name, mood):

        #Dependiendo de la persona y la emocion_receptor detectada en el intent, se calcula la intensidad de la emocion_receptor de respuesta. 

        with open("profiles/personalities.json", "r") as file:
            personality = json.load(file)[name]
        BF = personality["Personality"]
        sum=0
        multiplicador = personality["Emotions"][mood]     
        for i in range(0, len(BF)):
            sum = sum + (float(BF[i]) * float(multiplicador))
        return str(sum)

    def get_mood(self, intentname): 

        
        with open("profiles/index.json", "r") as file:
            personality = json.load(file)["EMOTIONS-INDEX"]
        for key, value in personality.items():
            if key == intentname:
                print ("El valor de ", key, "es ", value)
                return value