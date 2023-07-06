# Introduction 

Template for Functional Testing.

# Installing the Template

```
dotnet new install .\templates\Specflow.EndtoEndTests\ --force
```

# Creating a new Solution

```
dotnet new Specflow.EndtoEndTests -o Some.Specflow.EndtoEndTests --provider SomeProviderName
```

Parameters:
- Provider Name

Optional Features:
- EnableDatabaseFeature (DB)

```
dotnet new Specflow.EndtoEndTests -o ./tests/Some.Specflow.EndtoEndTests --provider SomeProviderName --EnableDatabaseFeature false
```

# Listing Parameters

```
dotnet new Specflow.EndtoEndTests -h
```

# To Do

- Exclude template.json and dotnetcli from sln file in created solutions
