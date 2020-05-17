## For [Create a bot using adaptive, component, waterfall, and custom dialogs](/articles/v4sdk/bot-builder-mixed-dialogs.md)

```nomnoml
#font: Segoe UI
#fontSize: 9
#lineWidth: 1
#arrowSize: 1
#bendSize:0.3
#edges: rounded
#padding: 8
#spacing: 16
#fill: #def; #acf
#ranker: longest-path

[<class> <<component>> root |
 [waterfall]->[<<slot filling>> fullname]
 [waterfall]->[AdaptiveDialog]
 [AdaptiveDialog]->[<<slot filling>> address]
 [<class> <<slot filling>> fullname |
  first
  last
 ]
 [AdaptiveDialog |
  age
  shoe size
  fullname
  address |
  [<trigger> OnBeginDialog |
   [<action> NumberInput userage]->[<action> NumberInput shoesize]
   [NumberInput shoesize]->[<action> BeginDialog address]
  ]
 ]
 [<class> <<slot filling>> address |
  street
  city
  zip
 ]
]
```
