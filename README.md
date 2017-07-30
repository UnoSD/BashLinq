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
alias groupby='./BashLinq.exe groupby'
```

Try some C# LINQ to map/filter other command's output

```bash
ls | where 'i => i.ToLower().Contains("a")'

ls | groupby 'i => i.Length'

#18
#    BashLinq.deps.json
#12
#    BashLinq.dll
#    BashLinq.pdb
#31
#    BashLinq.runtimeconfig.dev.json
#27
#    BashLinq.runtimeconfig.json

ls | select "ToUpper()" | where '!StartsWith("Bash")'
```