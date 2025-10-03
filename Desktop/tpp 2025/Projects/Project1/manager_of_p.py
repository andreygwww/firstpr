import os
def create():
    notes = input("введите заметку: ").lower()
    with open("notes.txt", "a") as file:
        file.write(notes + "\n")
    print("сохранилась записка")
def delete():
    with open("notes.txt", "r") as file:
        notes = file.readlines()
    if not notes:
        print("нету таких заметок чтобы удалить.")
        return
    print("Список записок:")
    for i, note in enumerate(notes, 1):
        print(f"{i}. {note.strip()}")
    numb = int(input("номер для удаления: "))
    if numb < 1 or numb > len(notes):
        print("неправильный номер заметки")
        return
    del notes[numb - 1]
    with open("notes.txt", "w") as file:
        for note in notes:
            file.write(note)
    print(f"Заметка {numb} была удалена")
def search():
    try:
        with open("notes.txt", "r") as file:
            notes = file.readlines()
    except FileNotFoundError:
        print("Нету таких заметок")
        return
    keyword = input("введите слово для поиска ").lower()
    found_notes = []
    for i, note in enumerate(notes, 1):
        if keyword in note.lower():
            found_notes.append((i, note.strip()))
    if found_notes:
        print(f"\nНайдено {len(found_notes)} заметок")
        for i,note in found_notes:
            print(f"заметка {i},{note}")
    else:
        print("нету таких")
def close():
    exit()
def show():
        with open("notes.txt", "r") as file:
            notes = file.readlines()
            if not notes:
                print("Нету таких")
            else:
                print("Список заметок:")
            for i, note in enumerate(notes,1):
                print(f"{i}. {note.strip()}")
def interface():
    print('''здаравствуйте дорогой пользователь''')
    while True:
        print('''чо нада для выбора номер выбери:
    1 если создать заметку,
    2 если удалить заметку,
    3 поиск заметки,
    4 закрыть ,
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
            case _:
                print("надо цифру!!!")
                continue
interface()
