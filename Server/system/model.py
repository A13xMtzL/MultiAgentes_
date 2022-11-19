from mesa import Agent, Model

from mesa.space import SingleGrid

from mesa.time import BaseScheduler

from mesa.datacollection import DataCollector
import numpy as np
import pandas as pd

from system.agent import CarSimulation


class HighWay (Model):
    def __init__(self, width, height, initial_population=4):
        self.step_count = 0
        self.width = width
        self.height = height
        self.schedule = BaseScheduler(self)
        self.grid = SingleGrid(width, height, False)
        self.initial_population = initial_population
        self.num_agents = 100
        self.id_increment = 0

        pos_x = np.random.choice([0, 1, 2])
        pos_y = 0
        car = CarSimulation(0, self, pos_x, pos_y)

        self.grid.place_agent(car, (pos_x, pos_y))
        self.schedule.add(car)

        self.id_increment += 1

    def step(self):
        self.step_count += 1
        self.schedule.step()

        if self.id_increment < self.num_agents and self.id_increment >= 1:
            pos_x = np.random.choice([0, 1, 2])
            pos_y = 0

            car = CarSimulation(self.id_increment, self, pos_x, pos_y)

            self.grid.place_agent(car, (pos_x, pos_y))

            self.schedule.add(car)

            self.id_increment += 1

    def __str__(self):
        return f"Width: {self.width},Height: {self.height}, Numero de carros iniciales: {self.initial_population}"

    def json(self):

        # positions_list = []
        # for idx in range(0, len(self.schedule.num_agents)):
        #     p = self.schedule.num_agents[idx].json()
        #     positions_list.append(p)
        # positions = positions_list
        return {"width": self.width, "height": self.height, "initial_population": self.initial_population}
