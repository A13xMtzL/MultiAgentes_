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
        return f"Street ID: {self.unique_id}, Width: {self.width}, Max number of cars: {self.max_num_cars}, Time: {self.time}, Time to stop: {self.time_stop}, Range: {self.range_stop}, Max speed: {self.max_speed}, Max steps: {self.max_steps}"

    def json(self):

        positions_list = []
        for idx in range(0, len(self.schedule.agents)):
            p = self.schedule.agents[idx].json()
            positions_list.append(p)
        positions = positions_list
        return {"unique_id": self.unique_id, "width": self.width, "height": self.height, "max_num_cars": self.max_num_cars, "time": self.time, "time_stop": self.time_stop, "range_stop": self.range_stop, "max_speed": self.max_speed, "max_steps": self.max_steps, "positions": positions, "step_count": self.step_count}
