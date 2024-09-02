# .NET LinqToKql Library

This project acts like a LINQ provider to convert a LINQ query to a KQL string that can be used against services like Azure Log Analytics.  For now this project is more of a learning project and has limited support for KQL operations and is by no means exhaustive.  See the [Supported Operations](#supported-operations) section for what is currently supported.

## Usage

Given the sample class defined in the tests `AzureResource` (note: that this class doesn't align with the actual Azure Resource objects coming out of Azure Resource Graph).

```C#
var query = Kql.Create<AzureResource>().Where(x => x.name == "test resource");
query = query.OrderBy(x => x.name);
var kql = query.ToKql();
```

would output the following:

```
resources | where name == 'test resource' | sort name asc
```

## Supported Operations

LINQ Operations:
- Where
- OrderBy
- OrderByDescending

KQL Operations:
- string: ==, !=, in (array) -- case sensitive/insensitive supported
- guid: ==
- datetime: ==, <, >, datetime.now
- numerical: tbd

## Todo List

- string: startsWith, endsWith, contains

