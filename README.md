# CAINE Jeopardy Board Manager  
This was a quick project to try and streamline trivia nights for UVA's CIO CAINE.  
The whole goal was to make a customizable and fairly intuitive board system that could read in questions and provide them in a much simpler format than, say, index cards.  

## Folders and Files  
The application does **NOT** automatically generate the required folders and files. To do so:
 1. In the directory the .exe is in, create the `settings` and `questions` folder.   
 2. Add into `settings` a file named `s.txt`.  
 3. Add into `questions` a file named `q.txt`.  
 4. The application will show you if you did not set this up correctly.  
 
## s.txt Format  
The following are acceptable lines in the s.txt file:
 * time: The amount of time (in seconds) that a question may run for. The last time setting in the file is used.  
 * category: The name of the i'th category. The first five categories are actually read in.  
 
 EXAMPLE: 
 ```
time 45
category First
category Second
category Third
category Fourth
category Fifth 
```
	
## q.txt Format
Each question is split as such:  
```
q The question text
a The answer to the question
c The numerical category (1-5)
v The value of the question (100-500)
# Used to denote the end of a question in the file.
```
	
You may string together as many questions as needed, each one being written as the block shown above.  
Basically, the line after the \# is when you start the next question.

