from flask import Flask, request
from utils.converter import to_json, message_to_json

from system.model import Highway
from system.modelOptimo import HighwayOptimizer


# Se inicializa la aplicacion
app = Flask(__name__)

model = Highway(3, 200)


@app.route('/', methods=['GET'])
def hello_world():
    return message_to_json("The server is up and running")

# Se inicializa el primer modelo


@app.route('/init', methods=['POST'])
def initial_model():
    # Se obtienen los parámetros obtenidos del body
    width = request.json['width']
    height = request.json['height']
    # Se inicializa el modelo
    global model
    model = Highway(width, height)

    # Retorno del mensaje
    return to_json(model.json())

# Se inicializa el segundo modelo


@app.route('/init2', methods=['POST'])
def initial_model2():
    # Se obtienen los parámetros obtenidos del body
    width = request.json['width']
    height = request.json['height']

    # Se inicializa el modelo
    global model2
    model2 = HighwayOptimizer(width, height)

    # Retorno del mensaje
    return to_json(model2.json())


# Hace un reincio del modelo
@app.route('/reset', methods=['GET'])
def reset_model():

    global model
    global model2

    model = Highway(3, 900)
    model2 = HighwayOptimizer(3, 1200)

    return message_to_json('El modelo ha sido reiniciado con exito')


# Se hace la ejecución de un paso en el primer modelo
@app.route('/step', methods=['GET'])
def step_model():
    model.step()
    return model.json()

# Se hace la ejecución de un paso en el segundo modelo


@app.route('/step2', methods=['GET'])
def step_model2():
    model2.step()
    return model2.json()

# Brinda la información inicial del modelo


@app.route('/info', methods=['GET'])
def info_model():
    return message_to_json(model2.__str__())
