"""
普通のインプット
t = int(input())

スペースがある標準入力
s = list(map(int, input().split()))

カンマ区切り
s = input().rstrip().split(',')

 最初に入力回数が与えられて、そのあとデータが入力される。といったパターン
N = int(input())
a = []
for i in range(N):
    a.append(int(input()))

listの使い方
aList = []             #まず宣言
for i in aList:        #これでループする（vector的な？)
len(aList)             #リストの要素数を返す(.size的な？)
list.remove("B")       #指定した値を削除する
judge = 4 in aList[2]  #これで検索ができる（返り値はtrue or false
list.index("B")        #インデックス番号を返す（ない場合はエラーがくる)
list.count("B")        #指定した値の要素数を返す
list = range(5)        #[0, 1, 2, 3, 4]
list = range(2, 5)     #[2, 3, 4]
list = range(1, 6, 2)  #[1, 3, 5] 第三引数はstep(VBA的な？)
tmpAns = ['.' for i in range(y)] #初期化 '.'は0で初期化
"".join(listname)     #listを""のやつで区切って連結
"""

print("hello world")


