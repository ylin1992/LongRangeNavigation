# -*- coding: utf-8 -*-
"""
Created on Thu Mar 10 21:34:45 2022

@author: PS Donation
"""

import json
import matplotlib.pyplot as plt
import math
import numpy as np

ORIGINAL_FILE = 'dtmE.json'
ADJUSTED_FILE = 'dtmE_Adj.json'


VFILE = 'dtmV.json'


def load_json(file):
    with open(file) as f:
        data = json.load(f)
        
    return data

def extract_vertices(vs):
    res = []
    for v in vs:
        res.append([v['X'], v['Y'], v['Z']])
    return res

oe = load_json(ORIGINAL_FILE)
ae = load_json(ADJUSTED_FILE)
vs = extract_vertices(load_json(VFILE))

slopes = []
angles = []
for e in oe:
    source = vs[int(e['From'])]
    target = vs[int(e['To'])]
    
    dxy_s = ((source[0] - target[0])**2 + (source[1] - target[1])**2)**0.5
    dz = source[2] - target[2]
    slopes.append(dxy_s / dz)
    
    angle = np.rad2deg(np.arctan(dxy_s/dz))
    # if abs(angle) > 40:
    #     angle = 40 if angle > 0 else -40 
    angles.append(angle)

ratios = [float(o['Weight']) / float(a['Weight']) for (o, a) in zip(oe, ae)]
plt.figure()
plt.plot(ratios)

plt.figure()
plt.plot(angles)