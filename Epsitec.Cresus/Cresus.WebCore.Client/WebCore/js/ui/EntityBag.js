Ext.define('Bag', {
        extend: 'Ext.data.Model',
        idProperty: 'id',
        fields: [{
            name: 'summary'
        }, {
            name: 'entityType'
        }, {
            name: 'data'
        }]
    });

Ext.require([
  'Epsitec.cresus.webcore.ui.DropZone'
],
function() {
  Ext.define('Epsitec.cresus.webcore.ui.EntityBag', {
    extend: 'Ext.Window',
    alternateClassName: ['Epsitec.EntityBag'],

    /* Properties */
    bagStore: null,
    dropZones: null,
    /* Constructor */

    constructor: function(menu) {
      var config;

      this.initStores();
      this.dropZones = [];
      this.removeFromBagDropZone = Ext.create('Epsitec.DropZone', 'removezoneid','Retirer de l\'arche', this.removeEntityFromBag, this);
      this.registerDropZone(this.removeFromBagDropZone);

      this.showEntityDropZone = Ext.create('Epsitec.DropZone', 'showentityid','Voir dans la base', this.showEntity, this);
      this.registerDropZone(this.showEntityDropZone);

      config = {
        headerPosition: 'left',
        title: 'Arche',
        cls: 'entitybag-window',
        iconCls: 'epsitec-aider-images-general-ark-icon16',
        autoHeight: true,
        draggable: true,
        resizable: true,
        closable: false,
        autoScroll: true,
        layout: {
            type: 'column'
        },
        dockedItems: [{
          xtype: 'toolbar',
          dock: 'top',
          items: this.removeFromBagDropZone
        }],
        items: [this.createEntityView()],
        listeners: {
          beforerender: this.setSizeAndPosition,
          score: this
        } 
      };

      menu.on("resize", this.resizeEntityBagHandler, this);
      this.callParent([config]);


      return this;
    },

    /* Methods */
    registerDropZone: function (dropzone) {
      this.dropZones[dropzone.dropZoneId] = dropzone;
    },

    showRegistredDropZone: function (){
      for(d in this.dropZones)
      {
        this.dropZones[d].show();
      }       
    },

    hideRegistredDropZone: function (){
      for(d in this.dropZones)
      {
        this.dropZones[d].hide();
      }  
    },

    resizeEntityBagHandler: function () {
        this.setSizeAndPosition();   
    },

    setSizeAndPosition: function() {
      var viewport = Epsitec.Cresus.Core.app.viewport, 
          menu = Epsitec.Cresus.Core.app.menu;
      if(Ext.isDefined(viewport))
      {
        var newHeight = ((this.bagStore.count() * 250));
        this.width = 280;
        if(newHeight < (viewport.height - 250))
        {
          this.height = newHeight;
        }
        this.x = viewport.width - this.width;
        this.y = menu.el.lastBox.height;
        if(this.isVisible())
        {
          this.setPosition(this.x,this.y);
        }
      } 
    },

    initStores: function(){
      this.bagStore = Ext.create('Ext.data.Store', {
          model: 'Bag',
          data: [],
      });
    },

    showEntity: function(entity)
    {
      var path = {};
      path.entityId = entity.id;

      alert(entity.id);
    },

    addEntityToBag: function(entityId) {
      var hub = Epsitec.Cresus.Core.app.hubs.getHubByName('entitybag');

      hub.AddToMyBag(entityId);
    },

    addEntityToClientBag: function(entity) {
      var index = this.bagStore.indexOfId(entity.id);
      if(index===-1)
      {
        this.bagStore.add(entity);
      }
      else
      {
        this.bagStore.removeAt(index);
        this.bagStore.insert(index,entity);
      }
      
      if(!this.isVisible())
      {
        this.show();
      }
      this.setSizeAndPosition();
    },

    removeEntityFromBag: function(entity) {
      var record = this.bagStore.getById(entity.id);
      var hub = Epsitec.Cresus.Core.app.hubs.getHubByName('entitybag');

      hub.RemoveFromMyBag(entity.id);
    },

    removeEntityFromClientBag: function(entity) {
      var record = this.bagStore.getById(entity.id);
      
      this.bagStore.remove(record);

      if(this.bagStore.count()==0)
      {
        this.hide();
      }

      this.setSizeAndPosition();
    },

    createToolbar: function() {
      return Ext.create('Ext.Toolbar', {
        dock: 'top',
        items: this.createButtons()
      });
    },

    createEntityView: function() {
      return Ext.create('Ext.view.View', {
        flex : 100,
        cls: 'entitybag-view',
        tpl: '<tpl for=".">' +
                '<div class="entitybag-source">' +
                    '<div class="entitybag-label">{entityType}</div>{summary}' +
                '</div><br/>' +
             '</tpl>',
        itemSelector: 'div.entitybag-source',
        overItemCls: 'entitybag-over',
        selectedItemClass: 'entitybag-selected',
        singleSelect: true,
        entityBag: this,
        store: this.bagStore,
        listeners: {
            render: this.initializeEntityDragZone,
            destroy: this.unregEntityDragZone,
            scope: this
        }
    });      
    },

    unregEntityDragZone: function (v) {
      if(v.dragZone)
      {
        v.dragZone.unreg();
      }
    },

    initializeEntityDragZone: function (v) {
        v.dragZone = Ext.create('Ext.dd.DragZone', v.getEl(), {

//      On receipt of a mousedown event, see if it is within a draggable element.
//      Return a drag data object if so. The data object can contain arbitrary application
//      data, but it should also contain a DOM element in the ddel property to provide
//      a proxy to drag.
          getDragData: function(e) {
              var sourceEl = e.getTarget(v.itemSelector, 10), d;

              if (sourceEl) {
                  d = sourceEl.cloneNode(true);
                  d.id = Ext.id();
                  return (v.dragData = {
                      sourceEl: sourceEl,
                      repairXY: Ext.fly(sourceEl).getXY(),
                      ddel: d,
                      entityData: v.getRecord(sourceEl).data
                  });
              }
          },

//      Provide coordinates for the proxy to slide back to on failed drag.
//      This is the original XY coordinates of the draggable element.
          getRepairXY: function() {
              return this.dragData.repairXY;
          },

          onBeforeDrag: function () {
            v.entityBag.showRegistredDropZone();
          },

          afterInvalidDrop: function () {
            v.entityBag.hideRegistredDropZone();
          },

          afterDragDrop: function () {
            v.entityBag.hideRegistredDropZone();
          }
      });
    },

    statics: {
       getUrl: function(prefix, viewMode, viewId, entityId) {
        var url = prefix + '/' + viewMode + '/' + viewId + '/' + entityId + '/list';
        return url;
      }
    }
  });
});
