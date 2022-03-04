import open3d as o3d
import numpy as np
import matplotlib.pyplot as plt
from mpl_toolkits.mplot3d import Axes3D
import json

FILE = "dtm.ply"

def read_pc(file_name):
    textured_mesh = o3d.io.read_point_cloud(file_name)
    
    arr = None
    if textured_mesh is not None:
        arr = np.asarray(textured_mesh.points)

    return arr

def read_pc_from_traingle(file_name):
    textured_mesh = o3d.io.read_triangle_mesh(file_name)
    alist = None
    v = None
    if textured_mesh is not None:
        textured_mesh.compute_adjacency_list()
        if textured_mesh.has_adjacency_list():
            alist = textured_mesh.adjacency_list
            v = textured_mesh.vertices
    return alist, np.asarray(v)


def draw_3d(arr):
    fig = plt.figure()
    ax = fig.gca(projection='3d')
    ax.scatter(arr[:,0], arr[:,1], arr[:,2])
    plt.show()

def convert_vertices(vs):
    assert type(vs) == np.ndarray
    assert len(vs.shape) == 2
    assert vs.shape[1] == 3
    
    res = []
    idx = 0
    for v in vs:
        single_v = {}
        single_v['Index'] = idx
        single_v['X'] = v[0]
        single_v['Y'] = v[1]
        single_v['Z'] = v[2]
        idx += 1
        
        res.append(single_v)
    return res

def convert_edges(alist, vertices):
    # alist = adjacencylist 
    # [set{vertex_indices}], i.e. alist[0] = {0, 1, 3, 20, 5}
    # vertices = array of vertex
    assert type(alist) == list
    assert type(vertices) == np.ndarray
    assert len(vertices.shape) == 2
    assert vertices.shape[1] == 3
    
    edges = [] 
    for i, l in enumerate(alist):
        assert type(l) == set
        for neighbor in l:
            edge = {}
            edge["From"] = i
            edge["To"] = neighbor
            # weight is initially set as actual distance (considering z)
            edge["Weight"] = _get_dist(vertices[i], vertices[neighbor])
            edges.append(edge)
    return edges

def _get_dist(v1, v2):
    return ((v1[0] - v2[0])**2 + (v1[1] - v2[1])**2 + (v1[2] - v2[2])**2)**0.5

def parse_to_json(mesh_dict, file_name):
    f = open(file_name, "w")
    json.dump(mesh_dict, f)
    f.close()

def main():
    arr = read_pc(FILE)
    draw_3d(arr)
    
if __name__ == "__main__":
    alist, vs = read_pc_from_traingle(FILE)
    v_dict = convert_vertices(vs)
    edges = convert_edges(alist, vs)
    parse_to_json(v_dict, "dtmV.json")
    parse_to_json(edges, "dtmE.json")
    #draw_3d(arr)