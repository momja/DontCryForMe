from PIL import Image
import numpy as np
import random
import math

max_size = 101

def generate_seed():
    # generate a random value to determine the planet's size
    max_radius = math.floor(max_size/2)
    seed = random.randint(0,max_radius)
    return seed

# generate a circle from a given radius with a max radius of 100px
def make_planet(radius):
    if radius > 50 or  radius < 0:
        # out of bounds exception
        raise IndexError('radius must be less than 50 and greater than 0')
    w, h = (max_size,max_size)    # width and height are defaulted to 100px
    center_x = w//2     # used later for translation to the center of the array
    center_y = h//2
    planet = np.zeros((w,h),dtype=np.uint8) # initialize the planet array
    y = 0
    for i in range(2):          # loop through and draw circle shape. Repeat twice, to draw both top and bottom
        for x in range(-radius, radius+1):
            y = math.ceil(math.sqrt(radius*radius - x*x))   # y = +/-sqrt(r^2 - x^2)
            if i == 0:  # do top half of planet
                planet[center_x + x, center_y + y] = 255
            else:           # do bottom half of planet
                planet[center_x + x, center_y - y] = 255
        for y in range(-radius, radius+1):      # repeat loop to make sure there are no empty pixels
            x = math.ceil(math.sqrt(radius*radius - y*y))   # x = +/-sqrt(r^2 - y^2)
            if i == 0:
                planet[center_x + x, center_y + y] = 255
            else:
                planet[center_x - x, center_y + y] = 255
    Image.fromarray(planet).show()
    return planet

if __name__ == '__main__':
    seed = generate_seed()
    make_planet(seed)
