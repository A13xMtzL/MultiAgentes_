import numpy as np
import pandas as pd
import time
import datetime

from mesa import Agent, Model
from mesa.space import SingleGrid
from mesa.time import BaseScheduler
from mesa.datacollection import DataCollector


class CarSimulation(Agent):
    def __init__(self, unique_id, model, pos_x, pos_y, length=0, orientation=0):
        super().__init__(unique_id, model)

        '''Posiciones en x,y.
          Velocidad Inicial'''
        self.unique_id = unique_id
        self.pos_x = pos_x
        self.pos_y = pos_y
        # self.speed = 60
        self.isActive = True
        self.length = length
        self.orientation = orientation

    # Make a function named 'check_neighbors' that checks if there is a car in 5 cells in front of the car in the same row

    # Hace una comprobación de la misma fila para ver si hay alguien adelante
    def check_neighbors(self):
        # Check if there is a car in the next cell
        for neighbor in self.model.grid.get_neighbors(self.pos, moore=True, include_center=False):
            if neighbor.pos_x == self.pos_x and neighbor.pos_y == (self.pos_y + 1):
                return False
        # Check if there is a car in the next 4 cells
            for i in range(4):
                if neighbor.pos_x == self.pos_x and neighbor.pos_y == (self.pos_y + i):
                    return False
        return True

    # Create a function that checks if the top row has the next five cells available using the check_neighbors function, if available the agent moves to this new row
    def change_lane(self):
        movement = self.check_neighbors()

        if movement == True:
            if self.pos_x == 0:
                self.pos_x = 1
                self.model.grid.move_agent(self, (self.pos_x, self.pos_y))
            elif self.pos_x == 1:
                self.pos_x = 0
                self.model.grid.move_agent(self, (self.pos_x, self.pos_y))

    def move_forward(self):
        movement = self.check_neighbors()

        if movement == True:
            self.pos_y = self.pos_y + 1
            self.model.grid.move_agent(self, (self.pos_x, self.pos_y))

        elif movement == False:
            self.change_lane()

    def step(self):
        self.move_forward()

    def __str__(self):
        return f"Id del vehiculo: {self.unique_id}, Position: {self.position}"

    def json(self):
        return {"unique_id": self.unique_id, "position": {"Posicion en x: ": int(self.pos_x), "Posicion en y: ": int(self.pos_y)}}
