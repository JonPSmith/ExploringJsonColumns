# ExploringJsonColumns 

## 10, 100: Simple, only showing the tasks that work

```text
One Author per book
10 books		| 100 books
10 Authors		| 100 Authors
0 Find book		| 0 Find book 
0 Reviews		| 0 Reviews
0 Promotions	| 0 Promotions
```

| Method     | NumBooks | Mean         | Error        | StdDev       |
|----------- |--------- |-------------:|-------------:|-------------:|
| AddSql     | 10       | 489,463.1 us | 22,264.82 us | 33,324.93 us |
| AddJson    | 10       | 445,212.8 us | 19,245.94 us | 28,806.42 us |
| ReadSql    | 10       |     646.1 us |      4.42 us |      6.47 us |
| ReadJson   | 10       |     604.3 us |     12.50 us |     18.32 us |
| AddSql     | 100      | 483,833.7 us | 19,888.58 us | 29,768.29 us |
| AddJson    | 100      | 460,351.0 us | 20,565.71 us | 30,781.78 us |
| ReadSql    | 100      |   1,164.7 us |     28.04 us |     41.97 us |
| ReadJson   | 100      |   2,105.1 us |     14.26 us |     20.90 us |


## 10, 100, 1,000: Complex

```text
10 books		| 100 books			| 1000 books
20 Authors		| 200 Authors		| 2000 Authors
10 Find book	| 10 Authors books	| 10 Authors books
20 Reviews		| 200 Reviews		| 2000 Reviews
1 Promotions	| 10 Promotions		| 100 Promotions
```

| Method     | NumBooks | Mean           | Error        | StdDev       | 
|----------- |--------- |---------------:|-------------:|-------------:|
| AddSql     | 10       | 484,560.7 us   | 21,045.04 us | 31,499.23 us | 
| AddJson    | 10       | 465,823.8 us   | 20,801.46 us | 31,134.65 us | 
| ReadSql    | 10       |     872.1 us   |      8.35 us |     12.25 us | 
| ReadJson   | 10       |     782.5 us   |     13.69 us |     19.19 us | 
| OrderSql   | 10       |     960.9 us   |     11.75 us |     17.58 us | 
| OrderJson  | 10       |     750.2 us   |      5.83 us |      8.55 us | 
| AuthorSql  | 10       |     490.1 us   |     19.80 us |     29.64 us | 
| AuthorJson | 10       |     572.4 us   |     30.40 us |     45.50 us | 
| AddSql     | 100      | 527,989.4 us   | 21,540.53 us | 32,240.85 us | 
| AddJson    | 100      | 507,386.9 us   | 25,168.28 us | 36,891.34 us | 
| ReadSql    | 100      |   1,819.9 us   |     41.26 us |     60.47 us | 
| ReadJson   | 100      |  14,299.6 us   |    183.66 us |    263.41 us | 
| OrderSql   | 100      |   1,915.2 us   |     20.51 us |     28.08 us | 
| OrderJson  | 100      |  13,786.0 us   |     86.24 us |    123.69 us | 
| AuthorSql  | 100      |     569.3 us   |     22.49 us |     33.66 us | 
| AuthorJson | 100      |   1,099.5 us   |      7.74 us |     11.58 us | 
| AddSql     | 1000     | 1,841,422.6 us | 22,380.39 us | 32,804.89 us |
| AddJson    | 1000     |   982,724.1 us | 27,639.94 us | 40,514.27 us |
| ReadSql    | 1000     |    16,409.8 us |     69.89 us |    100.24 us |
| ReadJson   | 1000     |   155,170.5 us |  1,934.49 us |  2,835.55 us |
| OrderSql   | 1000     |    21,807.2 us |    208.52 us |    299.05 us |
| OrderJson  | 1000     |   153,265.9 us |  1,144.50 us |  1,677.59 us |
| AuthorSql  | 1000     |       795.0 us |     12.47 us |     18.66 us |
| AuthorJson | 1000     |     6,123.1 us |     62.10 us |     87.05 us |