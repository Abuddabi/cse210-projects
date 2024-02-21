# Console Chat Application (for studying purposes)

After installation you need to run the program. I use "Run and Debug" in VS Code.

### Authentication

After running you will see following text in the console:

```bash
Welcome to the Console Chat Application!

Do you want to:

1. Login
2. Signup
Please write your answer:
```

Here you need to choose if you want to:<br>
Signup (<u>create a new user</u>)<br>
or Login (<u>use already existing user</u>).

Here are existing users (<u>you can find them in <b>users.txt</b> file</u>):

| Username | Password | Type         |
| -------- | -------- | ------------ |
| test     | test     | Regular User |
| test2    | test2    | Moderator    |
| test3    | test3    | Admin        |

Menu for each type of user is slightly different.<br>
Moderator can create and delete chat rooms.<br>
Admin additionally can block/unblock user and change user type.

That's how regular user's menu looks like:

```bash
MENU:
  1. Show other Users
  2. Show available chat rooms
  3. Choose the chat room
  ====================
  4. Exit
Please choose menu option:
```

To start using chat you need to choose the chat room, by entering the name of the room.<br>
To see names of all available chat rooms you need to choose 2 in the menu. (<u>You can also find all available rooms in <b>rooms.txt</b> file</u>)

### Robot API

Application uses API with automatic answers to messages. When you will choose the chat room application will ask you if you want to use robot which will answer you in the chat:

```bash
MENU:
  1. Show other Users
  2. Show available chat rooms
  3. Choose the chat room
  ====================
  4. Exit
Please choose menu option: 3
Please write the name of the chat room you want to start speaking in:
Main
Do you want to use API robot to answer in the chat? (yes/no):
```

You will need to enter API key to using it:

```bash
Do you want to use API robot to answer in the chat? (yes/no): yes
Please, enter API KEY (or type "cancel"):
```

You can create API key here: [Link](https://workshop.simsimi.com/en)<br>

#### <u><b>For graders I wrote existing API key in comments to assignment.</b></u>

```bash
20 Feb 20:02 test2: hello there!
20 Feb 21:14 test: tell me a joke
Waiting for Robot answer...


To exit the room type "exit"
test:
```

```bash
20 Feb 20:02 test2: hello there!
20 Feb 21:14 test: tell me a joke
20 Feb 21:14 Robot: You're a wonderful developer.


To exit the room type "exit"
test:
```

### Exit the application

To exit the application you will need to choose the last option in the main menu:

```bash
MENU:
  1. Show other Users
  2. Show available chat rooms
  3. Choose the chat room
  ====================
  4. Exit
Please choose menu option: 4

Chat ended. Goodbye!
```

## Happy coding!
