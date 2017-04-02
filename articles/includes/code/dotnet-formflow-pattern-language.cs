// <patterns1> 
[Prompt("What kind of {&} would you like? {||}")]
public SandwichOptions? Sandwich;
// </patterns1>


// <patterns2> 
[Prompt("What kind of {&} would you like? {||}", ChoiceFormat="{1}")]
public SandwichOptions? Sandwich;
// </patterns2>