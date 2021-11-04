# RASA PERFILES - INTENSIDAD - EMOCIONES

## GUIA DE USO

1. Activar en dos consolas diferentes el entorno de python donde tienen instalado rasa (el mismo que se uso para la demo de antes).
    
        Si no lo tienen hecho, fijense que en los videos de instalacion de Rasa en Youtube tanto en el  Ubuntu como en el de Windows (2021) se explica como hacerlo.

2. Ir a Rasa/bot y dentro de la carpeta bot en una de las consolas usar el comando: rasa run -p 5005.

3. Ir a Rasa/bot y dentro de la carpeta bot en la otra consola restante, usar el comando: rasa run actions

4. Esto ya dejaria andando y funcionando el bot. Recuerden que hay que adaptar Unity para que se permita el uso del mismo (el tema de los chats y todo eso que estaba en el proyecto viejo).



## Probar sin necesidad de Unity


Pueden usar postman o la extension de Visual Studio Code: Thunder Client para probar el bot sin necesidad de Unity.

Tienen que hacer un POST a la url: http://localhost:5005/webhooks/rest/webhook y en el Body poner algo del formato:

{
    "sender": "Camila",
    "Message": "Hola"
}

Cambie el perfil de Mateo por el de Facu (solo el nombre, los perfiles no los toque), por lo que si quieren probarlo con Mateo, no va a funcionar.

# Importante

La respuesta dada por rasa tendra el formato:


  {

    "recipient_id": "Camila",
    "custom": {
        "text": "Bueeeeenassss",
        "intensidad_receptor": "4.204999999999999",
        "emocion_receptor": "confianza"
    }
  }

Intensidad_receptor y emocion_receptor representan la intensidad y la emocion que detecta la persona que recibio el mensaje.

La persona que recibio el mensaje es el sender de la seccion "Probar Rasa sin necesidad de Unity". 

