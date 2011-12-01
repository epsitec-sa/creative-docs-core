Ext.define('Epsitec.Cresus.Core.Static.Enum',
  {
    extend : 'Ext.container.Container',
    alias : 'widget.epsitec.enum',
    
    /* Config */
    layout : 'column',
    
    /* Constructor */
    constructor : function (options)
    {
      options.columnWidth = 1;
      
      var combo = Ext.create('Epsitec.Cresus.Core.Static.EnumComboBox', options);
      
      var button = Ext.create('Ext.Button',
          {
            text : '>',
            renderTo : Ext.getBody(),
            handler : function ()
            {
              Ext.Msg.alert('Cannot edit this enumeration', 'You cannot directly edit this enumeration. You will need to save the current changes, click the header menu to edit the corresponding enumeration, and come back to this entity to edit it.');
            },
            margin : '19 0 0 5'
          }
        );
      
      this.items = this.items || new Array();
      this.items.push(combo);
      this.items.push(button);
      
      this.callParent();
      
      return this;
    },
  }
);
 