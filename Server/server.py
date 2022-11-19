from flask import Flask, request
from flask import jsonify
from flask.json import dumps

from system.model import HighWay
from utils.converter import message_to_json, to_json

# Funciones que nos permiten convertir los datos de los agentes en un formato JSON

# -------------------------------
# ----------------------------- #


app = Flask(__name__)

model = HighWay(3, 50, 30)


@app.route('/', methods=['GET'])
def hello_world():
    return message_to_json("The server is up and running")

# Se inicializa el modelo


@app.route('/init', methods=['POST'])
def initial_model():
    # Get the parameters
    width = request.json['width']
    height = request.json['height']
    initial_population = request.json['initial_population']

    # Se inicializa el modelo
    global model
    model = HighWay(width, height, initial_population)

    # Retorno del mensaje
    return to_json(model.json())

# Hace un reincio del modelo


@app.route('/reset', methods=['GET'])
def reset_model():

    global model

    model = HighWay(3, 50, 30)

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

# Hace un guardado de la animación del modelo


@app.route('/save', methods=['GET'])
def save_model():
    return message_to_json('Saving the app')
