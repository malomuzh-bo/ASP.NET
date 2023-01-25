a = int(input('Enter a: '))
b = int(input('Enter b: '))

print(a, '+', b, '=', a + b)
print(a, '-', b, '=', a - b)
print(a, '*', b, '=', a * b)
print(a, '/', b, '=', a / b)
print(a, '**', b, '=', a ** b)
print(a, '//', b, '=', a // b)
print(a, '%', b, '=', a % b)

s = a + b
mn = a - b
mp = a * b
d = a / b
pw = a ** b
f = a // b
p = a % b

with open('results.txt', 'w') as f:
    f.write('Sum: {} + {} = {}\n'.format(a, b, s))
    f.write('Minus: {} - {} = {}\n'.format(a, b, mn))
    f.write('Multiply: {} * {} = {}\n'.format(a, b, mp))
    f.write('Div: {} / {} = {}\n'.format(a, b, d))
    f.write('Pow: {} ** {} = {}\n'.format(a, b, pw))
    f.write('Int part of fraction: {} // {} = {}\n'.format(a, b, f))
    f.write('Percent: {} % {} = {}\n'.format(a, b, p))