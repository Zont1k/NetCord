# Parameters

You can specify parameters by separating them with a separator (default: `:`) in `customId`, for example `unban:userId`.

## Remainder
Last parameter always accepts rest of an input.

## Variable number of parameters
You can use `params` keyword to accept a variable number of parameters.

## Optional parameters
To mark parameters as optional, give them a default value.

> [!NOTE]
> C# does not allow you to give a default value to `params` parameters. You need to use @System.Runtime.InteropServices.DefaultParameterValueAttribute to specify it.