Ext.define('Epsitec.Cresus.Core.Static.CollectionSummaryTile',
  {
    extend : 'Epsitec.Cresus.Core.Static.SummaryTile',
    alias : 'widget.collectionsummarytile',
    
    /* Properties */
    propertyAccessorId : null,
    entityType : null,
    hideRemoveButton : false,
    hideAddButton : false,
    
    /* Constructor */
    constructor : function (o)
    {
      var options = o || {};
      this.addPlusMinusButtons(options);
      
      this.callParent(new Array(options));
      return this;
    },
    
    /* Additional methods */ 
    addPlusMinusButtons : function (options)
    {
      options.tools = options.tools || new Array();
      
      if (!options.hideAddButton)
      {
        options.tools.push(
          {
            type : 'plus',
            tooltip : 'Add a new item',
            handler : this.addEntity,
            scope : this
          }
        );
      }
      
      if (!options.hideRemoveButton)
      {
        options.tools.push(
          {
            type : 'minus',
            tooltip : 'Remove this item',
            handler : this.deleteEntity,
            scope : this
          }
        );
      }
    },
    
    showEntityColumnRefreshAndSelect : function (subViewMode, subViewId, entityId)
    {
      var columnId = this.ownerCt.columnId;
      var columnManager = this.ownerCt.columnManager;
      
      var callbackQueue = Epsitec.Cresus.Core.Static.CallbackQueue.create
      (
        function ()
        {
          columnManager.selectEntity(columnId, entityId);
        },
        this
      );
      
      this.showEntityColumnAndRefresh(subViewMode, subViewId, entityId, callbackQueue);
    },
    
    removePanel : function ()
    {
      var columnManager = this.ownerCt.columnManager;
      
      // If this panel is currently selected, we must remove all the columns to the
      // right of this one.
      var columnId = this.ownerCt.columnId;
      var selectedEntityId = columnManager.getSelectedEntity(columnId);
      
      if (selectedEntityId == this.entityId)
      {
        columnManager.removeColumnsFromIndex(columnId + 1);
      }
      
      // Now we refresh the current column in order to update the UI with any modification that
      // the deletion might have done to summaries.
      this.refreshEntity(true);
    },
    
    addEntity : function ()
    {
      this.setLoading();
      
      Ext.Ajax.request(
        {
          url : 'proxy/collection/create',
          method : 'POST',
          params :
          {
            parentEntityId : this.ownerCt.entityId,
            entityType : this.entityType,
            propertyAccessorId : this.propertyAccessorId
          },
          success : function (response, options)
          {
            this.setLoading(false);
            
            try
            {
              var json = Ext.decode(response.responseText);
            }
            catch (err)
            {
              options.failure.apply(arguments);
              return;
            }
            
            var newEntityId = json.content;
            
            this.showEntityColumnRefreshAndSelect(this.subViewMode, this.subViewId, newEntityId);
          },
          failure : function ()
          {
            this.setLoading(false);
            Epsitec.Cresus.Core.Static.ErrorHandler.handleError(response);
          },
          scope : this
        }
      );
    },
    
    deleteEntity : function ()
    {
      this.setLoading();
      
      Ext.Ajax.request(
        {
          url : 'proxy/collection/delete',
          method : 'POST',
          params :
          {
            parentEntityId : this.ownerCt.entityId,
            deletedEntityId : this.entityId,
            propertyAccessorId : this.propertyAccessorId
          },
          success : function (response, options)
          {
            this.setLoading(false);
            
            this.removePanel();
          },
          failure : function ()
          {
            this.setLoading(false);
            Epsitec.Cresus.Core.Static.ErrorHandler.handleError(response);
          },
          scope : this
        }
      );
    },
  }
);
 