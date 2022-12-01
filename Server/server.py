from flask import Flask, request
from utils.converter import to_json, message_to_json

from system.model import Highway


# Se inicializa la aplicacion
app = Flask(__name__)

model = Highway(3, 200)


@app.route('/', methods=['GET'])
def hello_world():
    return message_to_json("The server is up and running")

# Se inicializa el modelo


@app.route('/init', methods=['POST'])
def initial_model():
    # Get the parameters
    width = request.json['width']
    height = request.json['height']
    # initial_population = request.json['initial_population']

    # Se inicializa el modelo
    global model
    model = Highway(width, height)

    # Retorno del mensaje
    return to_json(model.json())

# Hace un reincio del modelo


@app.route('/reset', methods=['GET'])
def reset_model():

    global model

    model = Highway(3, 200)

    return message_to_json('Reset the model')

# Se hace la ejecución de un paso del modelo


@app.route('/step', methods=['GET'])
def step_model():
    model.step()
    return model.json()

# Brinda la información inicial del modelo


@app.route('/info', methods=['GET'])
def info_model():
    return message_to_json(model.__str__())
