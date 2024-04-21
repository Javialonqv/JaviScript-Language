# MY OWN LANGUAGE

## Syntaxis
Each line of the code file represents a different command to execute.

The commands and the parameters are separated by spaces, if one of the parameters contains spaces (like arithmetic operations), you must use the **"|"** character to separate them.

The commands **INSIDE** other commands, such as `convert.to_string` within `print convert.to_string`, require parameters enclosed in parentheses. The complete command should look something like this: `print convert.to_string(myVar)`.

## Main Library
The Main commands are the following ones: **[]** Indicates a parameter is optional.\
`var <name> <value>` Declares a new variable that can be used during the runtime.\
`print <values>` Prints the specified values in the console window.\
`printl <values>` Prints the specified values in the console window and a **\n** at the end.\
`pause [<message>]` Pauses the the app execution and waits for any user key input.\
`exit [<exit_code>]` Exit of the app.\
`reassign <var_name> <new_value>` Reassign the value of a specified variable with a new value.\
`import <library>` Imports the specified library into the runtime.\
`::<name>` Defines a new label during the runtime.\
`goto <label_name>` Jumps the code execution where the specified label is located if it exists.\
`If <conditions>` Define a code block that will only be executed if the conditions are met.\
`EndIf` Defines where the "If" code block ends.\
`Func <func_name>` Defines a code block that only can be executed with the "call" instruction.\
`EndFunc` Defines where the "Func" code block ends.\
`call` Calls a Func block if it exists.
### IMPORT COMMAND
Use `import <library>` to import a library into the app's execution in order to use functions from the specified library. If you don't import them, you won't be able to use their functions.
### LABELS
Use `::<name>` (WITHOUT SPACES) in order to create a **label** than can be used to jump directly to the line where its written using the `goto <label_name>` command.
### IF BLOCKS
Use `If <conditions>` in order to define an If block, than only will be executed when the conditions are met. These conditions must be written with `<value> <operator> <value>`. An example of this is `10 == 12` or `<var_name> != 10 + 2`. The number of conditions its undefined, you can write as much as you want. To separate every condition you can use `and` or `or`.

To define where the If block ends write `EndIf` at the end of the block.
### FUNCTIONS
Use `Func <name>` to define a new function block, and `EndFunc` to define where it ends. These blocks will ONLY be executed when called by the `call <func_name>` command.

**TODO:** Add parameters for the functions.

## Console Library
Remember to use `import console` in order to use these commands.

The commands of the **Console** library are the following ones:\
`console.clear` Clears the console window.\
`console.fore_color <color>` Gets and/or sets the color of the console window.\
`console.title <title>` Gets and/or sets the title of the console window.
### CLEAR COMMAND
Use `console.clear` to clear the console window. This command **CAN'T** be used with other ones.
### FOREGROUND AND BACKGROUND COLOR COMMANDS
Use `console.fore_color <color>` to set the color of the console window, the `<color>` parameter is a string with the name of the color, such as `red, blue, black, white`. Also you can use the this command with **NO** parameters to only get the current console window color.

These conditions also apply to the `console.back_color <color>` command.
### TITLE COMMAND
Use `console.title <title>` to set the title of the console window, this command can also be used with **NO** parameters to only get the current console window title.

## Convert Library
Remember to use `import convert` in order to use these commands.

The commands of the **Convert** library are the following ones:\
`convert.to_string <value>` Converts the specified value into a string value.\
`convert.to_int <value>` Converts the specified value into a int value.\
`convert.to_float <value>` Converts the specified value into a float value.\
`convert.to_bool <value>` Converts the specified value into a bool value.\
These commands can be used with other ones, such as `print convert.to_string(test)`.

## File Library
Remember to use `import file` in order to use these commands.

The commands of the **File** library are the following ones:\
`file.create <file_path>` Creates an empty file at the specified path.
`file.copy <original_file_path> <destination_file_path> [<overwrite>]` Copy a file at the specified file path.
`file.move <original_file_path> <destination_file_path>` Move a file at the specified file path.
`file.exists <file_path>` Return true if the specified file path exists.
`file.read_line <file_path> <line>` Returns the content of the specified line of the specified file.
`file.write_line <file_path> <line> <new_value>` Writes the specified content on the specified line on the specified file.
### OVERWRITE PARAMETER
Use `[<overwrite>]`, in case if the destination file path already exists, if false, an exception will occur.
### READ AND WRITE LINE COMMANDS
Use `file.read_line <file_path> <line>` and `file.write_line <file_path> <line> <new_value>` to read or write lines in a plain text file.

## Input Library
Remember to use `import input` in order to use these commands.

The commands of the **Input** library are the following ones:\
`input.user_input` Gets the input of the user in a string.\
`input.user_input_int` Gets the input of the user in a int.\
`input.user_input_float` Gets the input of the user in a float.\
`input.user_key` Gets the pressed key of the user.
### USER INPUT COMMAND
Use `input.user_input` command and all the other ones gets the input of the user and can be used with OTHER commands, such as `var test input.user_input`.
### USER KEY COMMAND
Use `input.user_key` to get the presed key by the user and can be used with OTHER commands, such as `var test input.user_key`.
