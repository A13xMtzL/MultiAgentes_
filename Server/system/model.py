from mesa import Agent, Model

from mesa.space import SingleGrid

from mesa.time import BaseScheduler

from mesa.datacollection import DataCollector
import numpy as np
import random

# from system.agent import CarSimulation
from system.agent import CarAgent

LIMIT_RANGE = 20


class Highway(Model):
    def __init__(self, width, height, choosen_agent=False, initial_population=30):
        self.grid = SingleGrid(width, height, False)
        self.schedule = BaseScheduler(self)
        self.choosen_agent = choosen_agent
        self.id_increment = 0
        self.initial_population = initial_population
        self.chose = 0
        self.steps = 0
        self.width = width
        self.height = height

    def step(self):
        # self.datacollector.collect(self)
        self.schedule.step()
        # if self.id_increment < self.initial_population and self.id_increment >= 0:
        prob = random.randint(0, 3)
        if prob == 1 or prob == 3 or prob == 2:
            if self.steps > 9 and self.chose == 0:
                pos_x = 1
                pos_y = 0

                car = CarAgent(self.steps, self, choosen=True)

                self.grid.place_agent(car, (pos_x, pos_y))

                self.schedule.add(car)
                self.id_increment += 1
                self.steps += 1
                self.chose = 1
            else:
                pos_x = np.random.choice([0, 1,  2])
                pos_y = 0

                car = CarAgent(self.steps, self)

                self.grid.place_agent(car, (pos_x, pos_y))

                self.schedule.add(car)

                self.id_increment += 1
                self.steps += 1

    def __str__(self):
        return f"Width: {self.width},Height: {self.height}, Numero de steps: {self.steps}, Numero de carros: {self.id_increment},Carro elegido: {self.choosen_agent}"

    def json(self):

        PositionList = []
        for idx in range(0, len(self.schedule.agents)):
            p = self.schedule.agents[idx].json()
            PositionList.append(p)
        positions = PositionList
        return {"width": self.width, "height": self.height, "num_steps": self.steps, "Num_Agents": (self.id_increment+1), "positions": positions}
