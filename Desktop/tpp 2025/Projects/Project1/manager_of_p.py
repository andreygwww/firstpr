import os
def create():
    notes = input("Enter note text: ")
    with open("notes.txt", "a") as file:
        file.write(notes + "\n")
    print("Note created successfully.")
def delete():
    with open("notes.txt", "r") as file:
        notes = file.readlines()
    if not notes:
        print("No notes to delete.")
        return

    print("\nNotes list:")
    for i, note in enumerate(notes, 1):
        print(f"{i}. {note.strip()}")
    numb = int(input("Enter note number to delete: "))
    if numb < 1 or numb > len(notes):
        print("Invalid note number.")
        return
    del notes[numb - 1]
    with open("notes.txt", "w") as file:
        for note in notes:
            file.write(note)
    print(f"Note #{numb} deleted successfully.")
def search():
    try:
        with open("notes.txt", "r") as file:
            notes = file.readlines()
    except FileNotFoundError:
        print("No notes found.")
        return
    if not notes:
        print("No notes to search.")
        return
    print("\nAll notes:")
    for i, note in enumerate(notes, 1):
        print(f"{i}. {note.strip()}")
    keyword = input("\nEnter keyword to search: ").lower()
    found_notes = []
    for i, note in enumerate(notes, 1):
        if keyword in note.lower():
            found_notes.append((i, note.strip()))
    if found_notes:
        print(f"\nFound {len(found_notes)} note(s):")
        for num in found_notes:
            print(num)
        for note in found_notes:
            print(note)
    else:
        print("No notes found with this keyword.")

def close():
    exit()
def show():
    try:
        with open("notes.txt", "r") as file:
            notes = file.readlines()
    except FileNotFoundError:
        print("No notes found.")
        return
    if not notes:
        print("No notes available.")
    else:
        print("\nAll notes:")
        for i, note in enumerate(notes, 1):
            print(f"{i}. {note.strip()}")
def interface():
    if not os.path.exists("notes.txt"):
        open("notes.txt", "w").close()
print('''здаравствуйте дорогой пользователь''')
interface()
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


