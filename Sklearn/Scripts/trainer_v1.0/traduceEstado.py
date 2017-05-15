import numpy as np


def histo2DRow(estado):
	retorno = estado[0:8]
	[retorno.append(i) for i in estado[10:26:3]]
	x = [estado[8],estado[11],estado[14],estado[17],estado[20],estado[23]]
	y = [estado[9],estado[12],estado[15],estado[18],estado[21],estado[24]]
	xedges = [-2,  -1, 0,   1,  2]
	yedges = [ 0, 2.5, 5, 7.5, 10]
	H, xedges, yedges = np.histogram2d(x, y, bins=(xedges, yedges))
	H = H.T  # Let each row list bins with common y range

	histogramaPlano = H.tolist()
	[retorno.append(i) for i in histogramaPlano]
	return retorno


estado = ['101123932','-0.91','0.56','25','999','999','999','999','1.82','8.92','5','-1.04','8.09','3','-1.07','4.83','3','-0.85','6.07','4','999','999','0','999','999','0']
#newEstado = estado[0:4]
#[newEstado.append(i) for i in histo2DRow(estado)]
print(histo2DRow(list(map(float,estado))))