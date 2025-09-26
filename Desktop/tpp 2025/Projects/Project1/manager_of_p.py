def create():
    pass
def delete():
    pass
def search():
    pass
def close():
    pass
def show():
    pass
def interface():
    pass

print('''здаравствуйте дорогой пользователь''')
while True:
    print('''чо нада для выбора номер выбери:
    1 если создать заметку,
    2 если удалить заметку,
    3 поиск заметки,
    4 закрыть заметку,
    5 показать заметку''')
    answer=input()
    match answer:
        case '1':
            create()
        case '2':
            delete()
        case '3':
            search()
        case '4':
            close()
        case '5':
            show()
        case _ :
            print("надо цифру!!!")
            continue


