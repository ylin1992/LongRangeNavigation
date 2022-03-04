from operator import lt
from random import Random, random
import matplotlib.pyplot as plt
from mpl_toolkits import mplot3d
import numpy as np
import cv2
import json
NUM_POINTS = 10
DIMENSIONS = 3
LOWER_Z = 0.0
UPPER_Z = 100.0
LOWER_W = 1
UPPER_W = 10
RESOLUTION = 1.0 # 1 M / block

def generate_list():
    dev_w = UPPER_W - LOWER_W
    dev_z = UPPER_Z - LOWER_Z
    res = []
    idx = 0
    for i in range(0, NUM_POINTS):
        for j in range(0, NUM_POINTS):
            # w = np.random.rand() * dev_w + LOWER_W
            z = np.random.rand() * dev_z + LOWER_Z
            nieghbors_num = int(np.random.rand() * NUM_POINTS)
            temp = dict()
            temp["Index"] = idx
            temp["X"] = i
            temp["Y"] = j
            temp["Z"] = int(z)
            # temp["W"] = w
            # temp["Neighbors"] = set()
            # for i in range(0, nieghbors_num):
            #     nei = int(np.random.rand() * NUM_POINTS)
            #     if nei == temp["Index"]:
            #         continue
            #     temp["Neighbors"].add(nei)
            # temp["Neighbors"] = list(temp["Neighbors"])
            res.append(temp)
            idx += 1
    return res

def generate_edges():
    nieghbors_num = int(np.random.rand() * NUM_POINTS**4) + (NUM_POINTS**2) * 2
    res = dict()
    for i in range(0, nieghbors_num):
        source = int(np.random.rand() * NUM_POINTS)
        target = int(np.random.rand() * NUM_POINTS)
        while (source == target):
            target = int(np.random.rand() * NUM_POINTS)
        if source not in res:
            res[source] = []
        if target not in res[source]:
            res[source].append(target)
    print(res)
    edges = []
    for s in res:
        for t in res[s]:
            dev_w = UPPER_W - LOWER_W
            weight = np.random.rand() * dev_w + LOWER_W
            edge = {}
            edge["From"] = s
            edge["To"] = t
            edge["Weight"] = weight
            edges.append(edge)
    return edges

def generate_neighbors_edge():
    dev_w = UPPER_W - LOWER_W
    res = []
    right = np.zeros((NUM_POINTS, NUM_POINTS))
    left = right.copy()
    up = right.copy()
    down = up.copy()
    for i in range(0, NUM_POINTS**2):
        print(i//NUM_POINTS)
        print(i%NUM_POINTS)
        if i - NUM_POINTS >= 0:
            #down
            edge = {}
            weight = int(np.random.rand() * dev_w + LOWER_W)
            edge["From"] = i
            edge["To"] = i - NUM_POINTS
            edge["Weight"] = weight
            res.append(edge)
            down[i//NUM_POINTS][i%NUM_POINTS] = weight
        if i + NUM_POINTS < NUM_POINTS**2:
            edge = {}
            weight = int(np.random.rand() * dev_w + LOWER_W)
            edge["From"] = i
            edge["To"] = i + NUM_POINTS
            edge["Weight"] = weight    
            res.append(edge)
            up[i//NUM_POINTS][i%NUM_POINTS] = weight
        if (i + 1)%NUM_POINTS != 0:
            edge = {}
            weight = int(np.random.rand() * dev_w + LOWER_W)
            edge["From"] = i
            edge["To"] = i + 1
            edge["Weight"] = weight
            res.append(edge)
            right[i//NUM_POINTS][i%NUM_POINTS] = weight
        if i%NUM_POINTS > 0:
            edge = {}
            weight = int(np.random.rand() * dev_w + LOWER_W)
            edge["From"] = i
            edge["To"] = i - 1
            edge["Weight"] = weight
            res.append(edge)
            left[i//NUM_POINTS][i%NUM_POINTS] = weight
    return res, [right, left, up, down]

def plot_terrain(vertices):

    z = np.zeros((NUM_POINTS, NUM_POINTS))
    for v in vertices:
        x = v['X']
        y = v['Y']
        z[y][x] = v['Z']
    xx = np.linspace(0, NUM_POINTS-1, NUM_POINTS)
    yy = xx.copy()
    X, Y = np.meshgrid(xx, yy)
    # print(X)
    # print(Y)
    # print(z)
    fig = plt.figure()
    ax = plt.axes(projection='3d')
    ax.plot_surface(X, Y, z)
    ax.set_zlim(0, UPPER_Z * 3)

def plot_grid(edges_arr, file_name):
    [right, left, up, down] = edges_arr
    CANVAS_SIZE = (2000, 2000, 3)
    BORDER = 100
    RADIUS = 50
    CIRCLE_COLOR = (0,0,0)
    CIRCLE_THICKNESS = 1
    ARROW_OFFSET = 10
    ARROW_THICKNESS = 3
    RIGHT_COLOR = (255, 0, 0)
    UP_COLOR = (0, 255, 0)
    LEFT_COLOR = (0, 0, 255)
    DOWN_COLOR = (0, 255, 255)
    FONT = cv2.FONT_HERSHEY_SIMPLEX
    FONT_SCALE = 1
    WEIGHT_THICKNESS = 2
    canvas = np.zeros(CANVAS_SIZE) + 160
    end = CANVAS_SIZE[0] - BORDER
    start = BORDER
    div = int((end - start) / (NUM_POINTS - 1))

    
    # right
    for i in range(0, NUM_POINTS - 1):
        for j in range(0, NUM_POINTS):
            x = i * div + start
            y = j * div + start
            arr_start = (x + ARROW_OFFSET, y + ARROW_OFFSET)
            arr_end = (x + div - ARROW_OFFSET, y + ARROW_OFFSET)
            text_coor = (int((arr_start[0] + arr_end[0]) / 2), int((arr_start[1] + arr_end[1]) / 2 + 3 * ARROW_OFFSET))
            canvas = cv2.arrowedLine(canvas, arr_start, arr_end, RIGHT_COLOR, ARROW_THICKNESS)
            canvas = cv2.putText(canvas, str(right[j][i]), text_coor, FONT, FONT_SCALE, RIGHT_COLOR, WEIGHT_THICKNESS, cv2.LINE_AA)
    
    # left
    for i in range(1, NUM_POINTS):
        for j in range(0, NUM_POINTS):
            x = i * div + start
            y = j * div + start
            arr_start = (x - ARROW_OFFSET, y - ARROW_OFFSET)
            arr_end = (x - div + ARROW_OFFSET, y - ARROW_OFFSET)
            text_coor = (int((arr_start[0] + arr_end[0]) / 2), int((arr_start[1] + arr_end[1]) / 2 - ARROW_OFFSET))
            canvas = cv2.arrowedLine(canvas, arr_start, arr_end, LEFT_COLOR, ARROW_THICKNESS)
            canvas = cv2.putText(canvas, str(left[j][i]), text_coor, FONT, FONT_SCALE, LEFT_COLOR, WEIGHT_THICKNESS, cv2.LINE_AA)
            
    # down
    for i in range(0, NUM_POINTS):
        for j in range(1, NUM_POINTS):
            x = i * div + start
            y = j * div + start
            arr_start = (x - ARROW_OFFSET, y - ARROW_OFFSET)
            arr_end = (x - ARROW_OFFSET, y - div + ARROW_OFFSET)
            text_coor = (int((arr_start[0] + arr_end[0]) / 2 - 5 * ARROW_OFFSET), int((arr_start[1] + arr_end[1]) / 2))
            canvas = cv2.arrowedLine(canvas, arr_start, arr_end, DOWN_COLOR, ARROW_THICKNESS)
            canvas = cv2.putText(canvas, str(down[j][i]), text_coor, FONT, FONT_SCALE, DOWN_COLOR, WEIGHT_THICKNESS, cv2.LINE_AA)
            
    # up (arrow from top to down)
    for i in range(0, NUM_POINTS):
        for j in range(0, NUM_POINTS - 1):
            x = i * div + start
            y = j * div + start
            arr_start = (x + ARROW_OFFSET, y + ARROW_OFFSET)
            arr_end = (x + ARROW_OFFSET, y + div - ARROW_OFFSET)
            text_coor = (int((arr_start[0] + arr_end[0]) / 2 + ARROW_OFFSET), int((arr_start[1] + arr_end[1]) / 2))
            canvas = cv2.arrowedLine(canvas, arr_start, arr_end, UP_COLOR, ARROW_THICKNESS)
            canvas = cv2.putText(canvas, str(up[j][i]), text_coor, FONT, FONT_SCALE, UP_COLOR, WEIGHT_THICKNESS, cv2.LINE_AA)
            
    for i in range(0, NUM_POINTS):
        for j in range(0, NUM_POINTS):
            x = i * div + start
            y = j * div + start
            canvas = cv2.circle(canvas, (x, y), RADIUS, CIRCLE_COLOR, CIRCLE_THICKNESS)
            canvas = cv2.putText(canvas, str(int(j * NUM_POINTS + i)), (x - ARROW_OFFSET//2, y + ARROW_OFFSET//2), FONT, FONT_SCALE, CIRCLE_COLOR, 2, cv2.LINE_AA)
    plt.figure()
    plt.imshow(canvas)
    cv2.imwrite(file_name, canvas)
    
def parse_to_json(mesh_dict, file_name):
    f = open(file_name, "w")
    json.dump(mesh_dict, f)
    f.close()

def main():
    vertices = generate_list()
    plot_terrain(vertices)
    parse_to_json(vertices, "verticesN100_3.json")
    edges, edges_arr = generate_neighbors_edge()
    plot_grid(edges_arr, "./img100_3.png")
    #print(vertices)
    #print(edges_arr[0])
    #print(edges_arr[1])
    #print(edges_arr[2])
    #print(edges_arr[3])
    parse_to_json(edges, "edgesN100_3.json")
    plt.show()



    
if __name__ == '__main__':
    main()