def subset(s):
	if len(s)==0:
		return [[]]
	result = []
	[a,b]=[s[0],s[1:]]
	t = subset(b)
	for x in t:
		result.append(x[:])
		x.append(a)
		result.append(x[:])
	return result
