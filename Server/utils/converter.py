from flask import jsonify
from flask.json import dumps


def message_to_json(message):
    return jsonify({"message": message})


def position_to_json(position):
    return jsonify({'x': position.x, 'y': position.y})


def to_json(obj):
    return jsonify(obj)


def list_to_json(list):
    return dumps(list)
