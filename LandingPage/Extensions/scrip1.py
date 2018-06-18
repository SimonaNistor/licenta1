import numpy as np
input = np.array([ [0,0,1],[0,1,1],[1,0,1],[1,1,1] ])
output = np.array([[0,1,1,0]]).T
syn0 = 2*np.random.random((3,4)) - 1
syn1 = 2*np.random.random((4,1)) - 1
for j in range(1000):
    firstLayer = 1/(1+np.exp(-(np.dot(input,syn0))))
    secondLayer = 1/(1+np.exp(-(np.dot(firstLayer,syn1))))
    secondLayer_delta = (output - secondLayer)*(secondLayer*(1-secondLayer))
    firstLayer_delta = secondLayer.dot(syn1.T) * (firstLayer * (1-firstLayer))
    syn1 += firstLayer.T.dot(secondLayer_delta)
    syn0 += input.T.dot(firstLayer_delta)


a = ""
a += "hello world\n"
a += "==========="
print("ceva")