import numpy as np
import pandas as pd
import time
import datetime
import random

from mesa import Agent, Model
from mesa.space import SingleGrid
from mesa.time import BaseScheduler
from mesa.datacollection import DataCollector


LIMIT_RANGE = 20


class CarSimulation(Agent):
    def __init__(self, unique_id, model, pos_x, pos_y, length=0, orientation=0, choosen=False):
        super().__init__(unique_id, model)

        '''Posiciones en x,y.
          Velocidad Inicial'''
        self.pos_x = pos_x
        self.pos_y = pos_y
        self.speed = 1
        self.Activated = True
        self.length = length
        self.orientation = orientation
        self.choosen = choosen

    # Hace una comprobación de la misma fila para ver si hay alguien adelante
    def check_neighbors(self):
        # Verifica si hay un vehículo en la siguiente celda
        for neighbor in self.model.grid.get_neighbors(self.pos, moore=True, include_center=False):
            if neighbor.pos_x == self.pos_x and neighbor.pos_y == (self.pos_y + 1):
                return False
            # Verifica si hay un vehículo en las siguientes tres celdas
            for i in range(3):
                if neighbor.pos_x == self.pos_x and neighbor.pos_y == (self.pos_y + i):
                    return False

        return True

    # Función que mueve el agente hacia adelante
    def move_forward(self):
        movement = self.check_neighbors()
        if movement == True:
            self.pos_y = self.pos_y + self.speed
            self.model.grid.move_agent(self, (self.pos_x, self.pos_y))
        elif movement == False:
            self.change_lane()

    # Function that checks if the cell above or below is available, if so, moves the agent to one of them
    def change_lane(self):
        # Check if the cell above is available
        for neighbor in self.model.grid.get_neighbors(self.pos, moore=True, include_center=False):
            if neighbor.pos_x == (self.pos_x - 1) and neighbor.pos_y == self.pos_y:
                return False

        # Check if the cell below is available
        for neighbor in self.model.grid.get_neighbors(self.pos, moore=True, include_center=False):
            if neighbor.pos_x == (self.pos_x + 1) and neighbor.pos_y == self.pos_y:
                return False

        # If both cells are available, randomly choose one of them
        random_lane = random.randint(0, 1)

        if random_lane == 0:
            self.pos_x = self.pos_x - 1
            self.model.grid.move_agent(self, (self.pos_x, self.pos_y))
        else:
            self.pos_x = self.pos_x + 1
            self.model.grid.move_agent(self, (self.pos_x, self.pos_y))

    # Function that takes just one random car that is in the middle row and makes it to stop just in the middle of the grid

    def stop_random_car(self):
        # for agent in self.model.schedule.agents:
        if self.choosen == True and self.pos_y >= LIMIT_RANGE:
            self.model.grid.move_agent(self, (self.pos_x, self.pos_y))

    def step(self):
        if self.choosen == True and self.pos_y >= LIMIT_RANGE:
            self.stop_random_car()
        else:
            self.move_forward()

    def __str__(self):
        return f"Id del vehiculo: {self.unique_id}, Posicion en x : {self.pos_x}, Posicion en y : {self.pos_y}, Velocidad: {self.speed}, Elegido: {self.choosen}"

    def json(self):
        return {"unique_id": self.unique_id, "position": {"Posicion en x: ": int(self.pos_x), "speed": int(self.speed), "choosen": self.choosen, "Posicion en y: ": int(self.pos_y)}}
