from matplotlib import colors
import random
import pandas as pd
import numpy as np
from mesa import Agent, Model

from mesa import Agent, Model
from mesa.space import SingleGrid
from mesa.time import BaseScheduler
from mesa.datacollection import DataCollector

from threading import Timer

LIMIT_RANGE = 20
STOP = 10


class CarAgent(Agent):
    def __init__(self, unique_id, model, choosen=False):
        super().__init__(unique_id, model)

        self.speed = 3
        self.choosen = choosen
        self.steps = 0

    def check_forward(self):
        neighbor = 0
        for i in range(1, 4):
            if not self.model.grid.is_cell_empty((self.pos[0], self.pos[1] + i)):
                neighbor += 1
        return neighbor

    """ def check_forward(self):
        x = self.model.grid.get_neighbors(self.pos, moore = False, include_center = False, radius = 5) """

    def change_lane(self):
        random_lane = random.randint(0, 1)

        if random_lane == 0:
            if self.model.grid.is_cell_empty((self.pos[0] - 1, self.pos[1])) and self.model.grid.is_cell_empty((self.pos[0] - 1, self.pos[1] - 1)) and self.model.grid.is_cell_empty((self.pos[0] - 1, self.pos[1] - 2)):
                self.model.grid.move_agent(
                    self, (self.pos[0] - 1, self.pos[1] + 1))
        else:
            if self.model.grid.is_cell_empty((self.pos[0] + 1, self.pos[1])) and self.model.grid.is_cell_empty((self.pos[0] + 1, self.pos[1] - 1)) and self.model.grid.is_cell_empty((self.pos[0] + 1, self.pos[1] - 2)):
                self.model.grid.move_agent(
                    self, (self.pos[0] + 1, self.pos[1] + 1))

    def move(self):
        if self.pos != None:
            if self.model.grid.out_of_bounds((self.pos[0], self.pos[1] + self.speed)):
                self.model.grid.remove_agent(self)
                self.model.schedule.remove(self)
            else:
                if self.choosen == True and self.steps >= STOP:
                    self.model.grid.move_agent(
                        self, (self.pos[0], self.pos[1]))
                else:
                    new_speed = self.speed - 1

                    if self.check_forward() == 0:
                        self.model.grid.move_agent(
                            self, (self.pos[0], self.pos[1] + self.speed))
                    else:
                        if self.check_forward() == 2:
                            self.model.grid.move_agent(
                                self, (self.pos[0], self.pos[1] + (new_speed)))
                        else:
                            if self.check_forward() == 1 and self.pos[0] == 1:
                                self.change_lane()

        self.steps += 1

    def step(self):
        self.move()

    def __str__(self):
        return f"Id del vehiculo: {self.unique_id}, Posicion en x : {self.pos_x}, Posicion en y : {self.pos_y}, Velocidad: {self.speed}, Elegido: {self.choosen}"

    def json(self):
        return {"PosX": int(self.pos[0]), "PosY": int(self.pos[1]), "unique_id": self.unique_id, "speed": int(self.speed), "choosen": self.choosen}
