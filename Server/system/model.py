from mesa import Agent, Model

from mesa.space import SingleGrid

from mesa.time import BaseScheduler

from mesa.datacollection import DataCollector
import numpy as np
import random

from system.agent import CarSimulation

LIMIT_RANGE = 20


class HighWay (Model):
    def __init__(self, width=3, height=100, initial_population=150, choosen_agent=False):
        self.schedule = BaseScheduler(self)
        self.grid = SingleGrid(width, height, False)
        self.initial_population = initial_population
        self.id_increment = 0
        self.choosen_agent = choosen_agent
        self.num_steps = 0
        self.chose = 0
        self.width = width
        self.height = height

        # self.datacollector = DataCollector(
        #     model_reporters={"Grid": get_grid}
        # )

        #pos_x = np.random.choice([0, 1, 2])
        pos_x = np.random.choice([0, 1, 2])

        pos_y = 0
        car = CarSimulation(0, self, pos_x, pos_y)

        self.grid.place_agent(car, (pos_x, pos_y))
        self.schedule.add(car)

        self.id_increment += 1

    def step(self):
        # self.datacollector.collect(self)
        self.schedule.step()

        if self.id_increment < self.initial_population and self.id_increment >= 1:
            prob = random.randint(0, 3)
            if prob == 1 or prob == 3 or prob == 2:
                if self.num_steps == 10 and self.chose == 0:
                    # if self.num_steps == 10:
                    pos_x = 1
                    pos_y = 0

                    car = CarSimulation(self.id_increment, self,
                                        pos_x, pos_y, choosen=True)

                    self.grid.place_agent(car, (pos_x, pos_y))

                    self.schedule.add(car)

                    self.id_increment += 1
                    self.num_steps += 1
                    self.chose = 1

                else:
                    self.chose = 0
                    pos_x = np.random.choice([0, 1, 2])
                    pos_y = 0

                    if pos_x == 1 and pos_y < LIMIT_RANGE - 1:
                        car = CarSimulation(
                            self.id_increment, self, pos_x, pos_y)

                        self.grid.place_agent(car, (pos_x, pos_y))

                        self.schedule.add(car)

                        self.id_increment += 1

                        self.num_steps += 1

    def __str__(self):
        return f"Width: {self.width},Height: {self.height}, Numero de carros iniciales: {self.initial_population}, Numero de steps: {self.num_steps}, Numero de carros: {self.id_increment},Carro elegido: {self.choosen_agent}"

    def json(self):

        PositionList = []
        for idx in range(0, len(self.schedule.agents)):
            p = self.schedule.agents[idx].json()
            PositionList.append(p)
        positions = PositionList
        return {"width": self.width, "height": self.height, "initial_population": self.initial_population, "num_steps": self.num_steps, "Numero de Agentes": (self.id_increment+1), "positions": positions}
