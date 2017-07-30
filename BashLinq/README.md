# BashLinq
.NET Core bridge to use LINQ from the command line

Example usages:

Run a terminal and go the the binary folder

Add aliases: 

```bash
alias map='./BashLinq.exe select'
alias select='./BashLinq.exe select'
alias filter='./BashLinq.exe where'
alias where='./BashLinq.exe where'
```

Try some C# LINQ to map/filter other command's output

```bash
ls | select "ToUpper()" | where '!StartsWith("Bash")'

ls | where 'i => i.ToLower().Contains("a")'
```