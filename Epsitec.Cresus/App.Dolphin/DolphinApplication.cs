ÿþ/ / 	 C o p y r i g h t   ©   2 0 0 3 - 2 0 0 8 ,   E P S I T E C   S A ,   C H - 1 0 9 2   B E L M O N T ,   S w i t z e r l a n d  
 / / 	 A u t h o r :   D a n i e l   R O U X ,   M a i n t a i n e r :   D a n i e l   R O U X  
  
 u s i n g   S y s t e m . C o l l e c t i o n s . G e n e r i c ;  
 u s i n g   S y s t e m . X m l ;  
 u s i n g   S y s t e m . X m l . S e r i a l i z a t i o n ;  
 u s i n g   E p s i t e c . C o m m o n . W i d g e t s ;  
 u s i n g   E p s i t e c . C o m m o n . S u p p o r t ;  
 u s i n g   E p s i t e c . C o m m o n . T y p e s ;  
 u s i n g   E p s i t e c . C o m m o n . D r a w i n g ;  
  
 n a m e s p a c e   E p s i t e c . A p p . D o l p h i n  
 {  
 	 / / /   < s u m m a r y >  
 	 / / /   F e n ê t r e   p r i n c i p a l e   d e   l ' a p p l i c a t i o n .  
 	 / / /   < / s u m m a r y >  
 	 p u b l i c   c l a s s   D o l p h i n A p p l i c a t i o n   :   A p p l i c a t i o n  
 	 {  
 	 	 s t a t i c   D o l p h i n A p p l i c a t i o n ( )  
 	 	 {  
 	 	 	 I m a g e P r o v i d e r . D e f a u l t . E n a b l e L o n g L i f e C a c h e   =   t r u e ;  
 	 	 	 I m a g e P r o v i d e r . D e f a u l t . P r e f i l l M a n i f e s t I c o n C a c h e ( ) ;  
 	 	 }  
  
 	 	 p u b l i c   D o l p h i n A p p l i c a t i o n ( )   :   t h i s ( n e w   R e s o u r c e M a n a g e r P o o l ( " A p p . D o l p h i n " ) ,   n u l l )  
 	 	 {  
 	 	 	 t h i s . r e s o u r c e M a n a g e r P o o l . D e f a u l t P r e f i x   =   " f i l e " ;  
 	 	 	 t h i s . r e s o u r c e M a n a g e r P o o l . S e t u p D e f a u l t R o o t P a t h s ( ) ;  
 	 	 }  
  
 	 	 p u b l i c   D o l p h i n A p p l i c a t i o n ( R e s o u r c e M a n a g e r P o o l   p o o l ,   s t r i n g [ ]   a r g s )  
 	 	 {  
 	 	 	 t h i s . r e s o u r c e M a n a g e r P o o l   =   p o o l ;  
  
 	 	 	 t h i s . m e m o r y   =   n e w   C o m p o n e n t s . M e m o r y ( t h i s ) ;  
 	 	 	 t h i s . p r o c e s s o r   =   n e w   C o m p o n e n t s . T i n y P r o c e s s o r ( t h i s . m e m o r y ) ;  
 	 	 	 t h i s . a s s e m b l e r   =   n e w   A s s e m b l e r ( t h i s . p r o c e s s o r ,   t h i s . m e m o r y ) ;  
 	 	 	 t h i s . b r e a k A d d r e s s   =   M i s c . u n d e f i n e d ;  
 	 	 	 t h i s . m e m o r y . R o m I n i t i a l i s e ( t h i s . p r o c e s s o r ) ;  
 	 	 	 t h i s . i p s   =   1 0 0 0 0 ;  
 	 	 	 t h i s . p a n e l M o d e   =   " B u s " ;  
 	 	 	 t h i s . f i r s t O p e n S a v e D i a l o g   =   t r u e ;  
  
 	 	 	 t h i s . S t a r t C h e c k ( ) ;  
 	 	 	  
 	 	 	 t r y  
 	 	 	 {  
 	 	 	 	 t h i s . C o p y S a m p l e F i l e s   ( ) ;  
 	 	 	 }  
 	 	 	 c a t c h  
 	 	 	 {  
 	 	 	 }  
  
 	 	 	 i f   ( a r g s   ! =   n u l l )  
 	 	 	 {  
 	 	 	 	 f o r e a c h   ( s t r i n g   a r g   i n   a r g s )  
 	 	 	 	 {  
 	 	 	 	 	 i f   ( a r g . E n d s W i t h ( " . d o l p h i n " ) )     / /   p r o g r a m m e . d o l p h i n   s u r   l a   l i g n e   d e   c o m m a n d e   ?  
 	 	 	 	 	 {  
 	 	 	 	 	 	 t h i s . f i l e n a m e   =   a r g ;     / /   i l   f a u d r a   l ' o u v r i r  
 	 	 	 	 	 }  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
 	 	 p u b l i c   v o i d   S h o w ( W i n d o w   p a r e n t W i n d o w )  
 	 	 {  
 	 	 	 / / 	 C r é e   e t   m o n t r e   l a   f e n ê t r e   d e   l ' é d i t e u r .  
 	 	 	 i f   (   t h i s . W i n d o w   = =   n u l l   )  
 	 	 	 {  
 	 	 	 	 W i n d o w   w i n d o w   =   n e w   W i n d o w ( ) ;  
 	 	 	 	 t h i s . W i n d o w   =   w i n d o w ;  
  
 	 	 	 	 w i n d o w . R o o t . W i n d o w S t y l e s   =   W i n d o w S t y l e s . D e f a u l t D o c u m e n t W i n d o w ;  
 	 	 	 	 w i n d o w . I c o n   =   B i t m a p . F r o m M a n i f e s t R e s o u r c e ( " E p s i t e c . A p p . D o l p h i n . I m a g e s . A p p l i c a t i o n . i c o n " ,   t y p e o f ( D o l p h i n A p p l i c a t i o n ) . A s s e m b l y ) ;  
  
 	 	 	 	 P o i n t   p a r e n t C e n t e r ;  
 	 	 	 	 R e c t a n g l e   w i n d o w B o u n d s ;  
  
 	 	 	 	 i f   ( p a r e n t W i n d o w   = =   n u l l )  
 	 	 	 	 {  
 	 	 	 	 	 R e c t a n g l e   a r e a   =   S c r e e n I n f o . A l l S c r e e n s [ 0 ] . W o r k i n g A r e a ;  
 	 	 	 	 	 p a r e n t C e n t e r   =   a r e a . C e n t e r ;  
 	 	 	 	 }  
 	 	 	 	 e l s e  
 	 	 	 	 {  
 	 	 	 	 	 p a r e n t C e n t e r   =   p a r e n t W i n d o w . W i n d o w B o u n d s . C e n t e r ;  
 	 	 	 	 }  
  
 	 	 	 	 d o u b l e   w   =   D o l p h i n A p p l i c a t i o n . M a i n W i d t h   +   D o l p h i n A p p l i c a t i o n . M a i n M a r g i n * 2 ;  
 	 	 	 	 d o u b l e   h   =   D o l p h i n A p p l i c a t i o n . M a i n H e i g h t   +   D o l p h i n A p p l i c a t i o n . M a i n M a r g i n * 2 ;  
  
 	 	 	 	 w i n d o w B o u n d s   =   n e w   R e c t a n g l e ( p a r e n t C e n t e r . X - w / 2 ,   p a r e n t C e n t e r . Y - h / 2 ,   w ,   h ) ;  
 	 	 	 	 w i n d o w B o u n d s   =   S c r e e n I n f o . F i t I n t o W o r k i n g A r e a ( w i n d o w B o u n d s ) ;  
  
 	 	 	 	 w i n d o w . W i n d o w B o u n d s   =   w i n d o w B o u n d s ;  
 	 	 	 	 w i n d o w . C l i e n t S i z e   =   w i n d o w B o u n d s . S i z e ;  
 	 	 	 	 w i n d o w . T e x t   =   T e x t L a y o u t . C o n v e r t T o S i m p l e T e x t ( R e s . S t r i n g s . W i n d o w . T i t l e ) ;  
 	 	 	 	 w i n d o w . N a m e   =   " A p p l i c a t i o n " ;     / /   u t i l i s é   p o u r   g é n é r e r   " Q u i t A p p l i c a t i o n "   !  
 	 	 	 	 w i n d o w . P r e v e n t A u t o C l o s e   =   t r u e ;  
 	 	 	 	 w i n d o w . M a k e M i n i m i z a b l e F i x e d S i z e W i n d o w ( ) ;     / /   t o u t   l e   l a y o u t   e s t   b a s é   s u r   u n e   f e n ê t r e   f i x e   !  
 	 	 	 	  
 	 	 	 	 t h i s . C r e a t e L a y o u t ( ) ;  
  
 	 	 	 	 i f   ( ! s t r i n g . I s N u l l O r E m p t y ( t h i s . f i l e n a m e ) )     / /   p r o g r a m m e   s u r   l a   l i g n e   d e   c o m m a n d e   à   o u v r i r   ?  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . O p e n ( ) ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 t h i s . W i n d o w . S h o w ( ) ;  
 	 	 	 t h i s . E n d C h e c k ( ) ;  
 	 	 }  
  
 	 	 i n t e r n a l   v o i d   H i d e ( )  
 	 	 {  
 	 	 	 t h i s . W i n d o w . H i d e ( ) ;  
 	 	 }  
  
 	 	 p u b l i c   o v e r r i d e   s t r i n g   S h o r t W i n d o w T i t l e  
 	 	 {  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 r e t u r n   T e x t L a y o u t . C o n v e r t T o S i m p l e T e x t ( R e s . S t r i n g s . W i n d o w . T i t l e ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p u b l i c   n e w   R e s o u r c e M a n a g e r P o o l   R e s o u r c e M a n a g e r P o o l  
 	 	 {  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 r e t u r n   t h i s . r e s o u r c e M a n a g e r P o o l ;  
 	 	 	 }  
 	 	 }  
 	 	  
 	 	 p u b l i c   C o m m a n d S t a t e   G e t C o m m a n d S t a t e ( s t r i n g   c o m m a n d )  
 	 	 {  
 	 	 	 C o m m a n d C o n t e x t   c o n t e x t   =   t h i s . C o m m a n d C o n t e x t ;  
 	 	 	 C o m m a n d S t a t e   s t a t e   =   c o n t e x t . G e t C o m m a n d S t a t e   ( C o m m a n d . G e t   ( c o m m a n d ) ) ;  
  
 	 	 	 r e t u r n   s t a t e ;  
 	 	 }  
  
  
 	 	 [ C o m m a n d ( A p p l i c a t i o n C o m m a n d s . I d . Q u i t ) ]  
 	 	 [ C o m m a n d ( " Q u i t A p p l i c a t i o n " ) ]  
 	 	 v o i d   C o m m a n d Q u i t A p p l i c a t i o n ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 e . E x e c u t e d   =   t r u e ;  
  
 	 	 	 i f   ( t h i s . Q u i t ( ) )  
 	 	 	 {  
 	 	 	 	 W i n d o w . Q u i t ( ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   o v e r r i d e   v o i d   E x e c u t e Q u i t ( C o m m a n d D i s p a t c h e r   d i s p a t c h e r ,   C o m m a n d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 / / 	 E v i t e   q u e   c e t t e   c o m m a n d e   n e   s o i t   e x é c u t é e   p a r   W i d g e t s . A p p l i c a t i o n ,  
 	 	 	 / / 	 c a r   c e l a   p r o v o q u e r a i t   l a   f i n   d u   p r o g r a m m e ,   q u e l l e   q u e   s o i t   l a  
 	 	 	 / / 	 r é p o n s e   d o n n é e   p a r   l ' u t i l i s a t e u r   a u   d i a l o g u e   a f f i c h é   p a r   D o c u m e n t E d i t o r .  
 	 	 }  
  
  
 	 	 p r o t e c t e d   v o i d   C r e a t e L a y o u t ( )  
 	 	 {  
 	 	 	 / / 	 C r é e   t o u s   l e s   w i d g e t s   d e   l ' a p p l i c a t i o n .  
 	 	 	 t h i s . m a i n P a n e l   =   n e w   M y W i d g e t s . M a i n P a n e l ( t h i s . W i n d o w . R o o t ) ;  
 	 	 	 t h i s . m a i n P a n e l . D o l p h i n A p p l i c a t i o n   =   t h i s ;  
 	 	 	 t h i s . m a i n P a n e l . B r i g h t n e s s   =   0 . 7 ;  
 	 	 	 t h i s . m a i n P a n e l . D r a w F u l l F r a m e   =   t r u e ;  
 	 	 	 t h i s . m a i n P a n e l . D r a w S c r e w   =   t r u e ;  
 	 	 	 t h i s . m a i n P a n e l . M i n S i z e   =   n e w   S i z e ( D o l p h i n A p p l i c a t i o n . M a i n W i d t h ,   D o l p h i n A p p l i c a t i o n . M a i n H e i g h t ) ;  
 	 	 	 t h i s . m a i n P a n e l . M a x S i z e   =   n e w   S i z e ( D o l p h i n A p p l i c a t i o n . M a i n W i d t h ,   D o l p h i n A p p l i c a t i o n . M a i n H e i g h t ) ;  
 	 	 	 t h i s . m a i n P a n e l . P r e f e r r e d S i z e   =   n e w   S i z e ( D o l p h i n A p p l i c a t i o n . M a i n W i d t h ,   D o l p h i n A p p l i c a t i o n . M a i n H e i g h t ) ;  
 	 	 	 t h i s . m a i n P a n e l . M a r g i n s   =   n e w   M a r g i n s ( D o l p h i n A p p l i c a t i o n . M a i n M a r g i n ,   D o l p h i n A p p l i c a t i o n . M a i n M a r g i n ,   D o l p h i n A p p l i c a t i o n . M a i n M a r g i n ,   D o l p h i n A p p l i c a t i o n . M a i n M a r g i n ) ;  
 	 	 	 t h i s . m a i n P a n e l . P a d d i n g   =   n e w   M a r g i n s ( 1 4 ,   1 4 ,   1 4 ,   0 ) ;  
 	 	 	 t h i s . m a i n P a n e l . D o c k   =   D o c k S t y l e . F i l l ;  
  
 	 	 	 t h i s . p a n e l T i t l e   =   n e w   M y W i d g e t s . P a n e l ( t h i s . m a i n P a n e l ) ;  
 	 	 	 t h i s . p a n e l T i t l e . B r i g h t n e s s   =   0 . 9 ;  
 	 	 	 t h i s . p a n e l T i t l e . D r a w F u l l F r a m e   =   t r u e ;  
 	 	 	 t h i s . p a n e l T i t l e . P r e f e r r e d H e i g h t   =   4 0 ;  
 	 	 	 t h i s . p a n e l T i t l e . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   0 ,   0 ,   1 0 ) ;  
 	 	 	 t h i s . p a n e l T i t l e . D o c k   =   D o c k S t y l e . T o p ;  
  
 	 	 	 S t a t i c T e x t   e p s i t e c   =   n e w   S t a t i c T e x t ( t h i s . m a i n P a n e l ) ;  
 	 	 	 e p s i t e c . T e x t   =   R e s . S t r i n g s . W i n d o w . C o p y r i g h t ;  
 	 	 	 e p s i t e c . C o n t e n t A l i g n m e n t   =   C o n t e n t A l i g n m e n t . M i d d l e R i g h t ;  
 	 	 	 e p s i t e c . P r e f e r r e d H e i g h t   =   1 3 ;  
 	 	 	 e p s i t e c . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   1 0 ,   0 ,   1 ) ;  
 	 	 	 e p s i t e c . D o c k   =   D o c k S t y l e . B o t t o m ;  
  
 	 	 	 t h i s . b u t t o n N e w   =   n e w   I c o n B u t t o n ( t h i s . p a n e l T i t l e ) ;  
 	 	 	 t h i s . b u t t o n N e w . A u t o F o c u s   =   f a l s e ;  
 	 	 	 t h i s . b u t t o n N e w . I c o n N a m e   =   M i s c . I c o n ( " N e w " ) ;  
 	 	 	 t h i s . b u t t o n N e w . M a r g i n s   =   n e w   M a r g i n s ( 1 0 ,   0 ,   8 ,   8 ) ;  
 	 	 	 t h i s . b u t t o n N e w . D o c k   =   D o c k S t y l e . L e f t ;  
 	 	 	 t h i s . b u t t o n N e w . C l i c k e d   + =   t h i s . H a n d l e B u t t o n N e w C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . b u t t o n N e w ,   R e s . S t r i n g s . C o m m a n d . N e w . T o o l t i p ) ;  
  
 	 	 	 t h i s . b u t t o n O p e n   =   n e w   I c o n B u t t o n ( t h i s . p a n e l T i t l e ) ;  
 	 	 	 t h i s . b u t t o n O p e n . A u t o F o c u s   =   f a l s e ;  
 	 	 	 t h i s . b u t t o n O p e n . I c o n N a m e   =   M i s c . I c o n ( " O p e n " ) ;  
 	 	 	 t h i s . b u t t o n O p e n . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   0 ,   8 ,   8 ) ;  
 	 	 	 t h i s . b u t t o n O p e n . D o c k   =   D o c k S t y l e . L e f t ;  
 	 	 	 t h i s . b u t t o n O p e n . C l i c k e d   + =   t h i s . H a n d l e B u t t o n O p e n C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . b u t t o n O p e n ,   R e s . S t r i n g s . C o m m a n d . O p e n . T o o l t i p ) ;  
  
 	 	 	 t h i s . b u t t o n S a v e   =   n e w   I c o n B u t t o n ( t h i s . p a n e l T i t l e ) ;  
 	 	 	 t h i s . b u t t o n S a v e . A u t o F o c u s   =   f a l s e ;  
 	 	 	 t h i s . b u t t o n S a v e . I c o n N a m e   =   M i s c . I c o n ( " S a v e " ) ;  
 	 	 	 t h i s . b u t t o n S a v e . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   0 ,   8 ,   8 ) ;  
 	 	 	 t h i s . b u t t o n S a v e . D o c k   =   D o c k S t y l e . L e f t ;  
 	 	 	 t h i s . b u t t o n S a v e . C l i c k e d   + =   t h i s . H a n d l e B u t t o n S a v e C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . b u t t o n S a v e ,   R e s . S t r i n g s . C o m m a n d . S a v e . T o o l t i p ) ;  
  
 	 	 	 S t a t i c T e x t   t i t l e   =   n e w   S t a t i c T e x t ( t h i s . p a n e l T i t l e ) ;  
 	 	 	 t i t l e . T e x t   =   M i s c . B o l d ( M i s c . F o n t S i z e ( D o l p h i n A p p l i c a t i o n . A p p l i c a t i o n T i t l e ,   2 0 0 ) ) ;  
 	 	 	 t i t l e . C o n t e n t A l i g n m e n t   =   C o n t e n t A l i g n m e n t . M i d d l e C e n t e r ;  
 	 	 	 t i t l e . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   0 ,   0 ,   0 ) ;  
 	 	 	 t i t l e . D o c k   =   D o c k S t y l e . F i l l ;  
  
 	 	 	 t h i s . b u t t o n A b o u t   =   n e w   I c o n B u t t o n ( t h i s . p a n e l T i t l e ) ;  
 	 	 	 t h i s . b u t t o n A b o u t . A u t o F o c u s   =   f a l s e ;  
 	 	 	 t h i s . b u t t o n A b o u t . I c o n N a m e   =   M i s c . I c o n ( " A b o u t " ) ;  
 	 	 	 t h i s . b u t t o n A b o u t . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   1 0 ,   8 ,   8 ) ;  
 	 	 	 t h i s . b u t t o n A b o u t . D o c k   =   D o c k S t y l e . R i g h t ;  
 	 	 	 t h i s . b u t t o n A b o u t . C l i c k e d   + =   t h i s . H a n d l e B u t t o n A b o u t C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . b u t t o n A b o u t ,   R e s . S t r i n g s . C o m m a n d . A b o u t . T o o l t i p ) ;  
  
 	 	 	 t h i s . b u t t o n L o o k   =   n e w   I c o n B u t t o n ( t h i s . p a n e l T i t l e ) ;  
 	 	 	 t h i s . b u t t o n L o o k . A u t o F o c u s   =   f a l s e ;  
 	 	 	 t h i s . b u t t o n L o o k . I c o n N a m e   =   M i s c . I c o n ( " L o o k " ) ;  
 	 	 	 t h i s . b u t t o n L o o k . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   0 ,   8 ,   8 ) ;  
 	 	 	 t h i s . b u t t o n L o o k . D o c k   =   D o c k S t y l e . R i g h t ;  
 	 	 	 t h i s . b u t t o n L o o k . C l i c k e d   + =   t h i s . H a n d l e B u t t o n L o o k C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . b u t t o n L o o k ,   R e s . S t r i n g s . C o m m a n d . L o o k . T o o l t i p ) ;  
  
 # i f   f a l s e  
 	 	 	 S t a t i c T e x t   v e r s i o n   =   n e w   S t a t i c T e x t ( t h i s . p a n e l T i t l e ) ;  
 	 	 	 v e r s i o n . T e x t   =   s t r i n g . C o n c a t ( " < f o n t   s i z e = \ " 8 0 % \ " > " ,   M i s c . G e t V e r s i o n ( ) ,   " < / f o n t > " ) ;  
 	 	 	 v e r s i o n . C o n t e n t A l i g n m e n t   =   C o n t e n t A l i g n m e n t . M i d d l e R i g h t ;  
 	 	 	 v e r s i o n . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   5 ,   0 ,   0 ) ;  
 	 	 	 v e r s i o n . D o c k   =   D o c k S t y l e . R i g h t ;  
 # e n d i f  
  
 	 	 	 M y W i d g e t s . P a n e l   a l l   =   n e w   M y W i d g e t s . P a n e l ( t h i s . m a i n P a n e l ) ;  
 	 	 	 a l l . D o c k   =   D o c k S t y l e . F i l l ;  
  
 	 	 	 / / 	 C r é e   l e s   d e u x   g r a n d e s   p a r t i e s   g a u c h e / d r o i t e .  
 	 	 	 t h i s . l e f t P a n e l   =   n e w   M y W i d g e t s . P a n e l ( a l l ) ;  
 	 	 	 t h i s . l e f t P a n e l . B r i g h t n e s s   =   0 . 9 ;  
 	 	 	 t h i s . l e f t P a n e l . D r a w F u l l F r a m e   =   t r u e ;  
 	 	 	 t h i s . l e f t P a n e l . P r e f e r r e d W i d t h   =   5 1 0 ;  
 	 	 	 t h i s . l e f t P a n e l . P a d d i n g   =   n e w   M a r g i n s ( 0 ,   0 ,   0 ,   1 0 ) ;  
 	 	 	 t h i s . l e f t P a n e l . D o c k   =   D o c k S t y l e . L e f t ;  
  
 	 	 	 t h i s . r i g h t P a n e l   =   n e w   M y W i d g e t s . P a n e l ( a l l ) ;  
 	 	 	 t h i s . r i g h t P a n e l . M a r g i n s   =   n e w   M a r g i n s ( 1 0 ,   0 ,   0 ,   0 ) ;  
 	 	 	 t h i s . r i g h t P a n e l . D o c k   =   D o c k S t y l e . F i l l ;  
  
 	 	 	 / / 	 C r é e   l e s   3   p a r t i e s   d e   g a u c h e .  
 	 	 	 t h i s . l e f t H e a d e r   =   n e w   M y W i d g e t s . P a n e l ( t h i s . l e f t P a n e l ) ;  
 	 	 	 t h i s . l e f t H e a d e r . P r e f e r r e d H e i g h t   =   1 ;     / /   s e r a   a g r a n d i  
 	 	 	 t h i s . l e f t H e a d e r . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   0 ,   5 ,   0 ) ;  
 	 	 	 t h i s . l e f t H e a d e r . D o c k   =   D o c k S t y l e . T o p ;  
  
 	 	 	 t h i s . t o p L e f t S e p   =   n e w   M y W i d g e t s . L i n e ( t h i s . l e f t P a n e l ) ;  
 	 	 	 t h i s . t o p L e f t S e p . P r e f e r r e d H e i g h t   =   1 ;  
 	 	 	 t h i s . t o p L e f t S e p . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   0 ,   5 ,   3 ) ;  
 	 	 	 t h i s . t o p L e f t S e p . D o c k   =   D o c k S t y l e . T o p ;  
  
 	 	 	 t h i s . l e f t C l o c k   =   n e w   M y W i d g e t s . P a n e l ( t h i s . l e f t P a n e l ) ;  
 	 	 	 t h i s . l e f t C l o c k . P r e f e r r e d W i d t h   =   5 0 ;  
 	 	 	 t h i s . l e f t C l o c k . M a r g i n s   =   n e w   M a r g i n s ( 1 0 ,   0 ,   0 ,   0 ) ;  
 	 	 	 t h i s . l e f t C l o c k . D o c k   =   D o c k S t y l e . L e f t ;  
  
 	 	 	 t h i s . l e f t P a n e l B u s   =   n e w   M y W i d g e t s . P a n e l ( t h i s . l e f t P a n e l ) ;  
 	 	 	 t h i s . l e f t P a n e l B u s . D o c k   =   D o c k S t y l e . F i l l ;  
  
 	 	 	 t h i s . l e f t P a n e l D e t a i l   =   n e w   M y W i d g e t s . P a n e l ( t h i s . l e f t P a n e l ) ;  
 	 	 	 t h i s . l e f t P a n e l D e t a i l . D o c k   =   D o c k S t y l e . F i l l ;  
 	 	 	 t h i s . l e f t P a n e l D e t a i l . V i s i b i l i t y   =   f a l s e ;  
  
 	 	 	 t h i s . l e f t P a n e l C o d e   =   n e w   M y W i d g e t s . P a n e l ( t h i s . l e f t P a n e l ) ;  
 	 	 	 t h i s . l e f t P a n e l C o d e . D o c k   =   D o c k S t y l e . F i l l ;  
 	 	 	 t h i s . l e f t P a n e l C o d e . V i s i b i l i t y   =   f a l s e ;  
  
 	 	 	 t h i s . l e f t P a n e l C a l m   =   n e w   M y W i d g e t s . P a n e l ( t h i s . l e f t P a n e l ) ;  
 	 	 	 t h i s . l e f t P a n e l C a l m . D o c k   =   D o c k S t y l e . F i l l ;  
 	 	 	 t h i s . l e f t P a n e l C a l m . V i s i b i l i t y   =   f a l s e ;  
  
 	 	 	 t h i s . l e f t P a n e l Q u i c k   =   n e w   M y W i d g e t s . P a n e l ( t h i s . l e f t P a n e l ) ;  
 	 	 	 t h i s . l e f t P a n e l Q u i c k . D o c k   =   D o c k S t y l e . F i l l ;  
 	 	 	 t h i s . l e f t P a n e l Q u i c k . V i s i b i l i t y   =   f a l s e ;  
  
 	 	 	 / / 	 C r é e   l e s   2   p a r t i e s   d e   d r o i t e .  
 	 	 	 t h i s . h e l p P a n e l   =   n e w   M y W i d g e t s . P a n e l ( t h i s . r i g h t P a n e l ) ;  
 	 	 	 t h i s . h e l p P a n e l . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   0 ,   0 ,   1 0 ) ;  
 	 	 	 t h i s . h e l p P a n e l . D o c k   =   D o c k S t y l e . F i l l ;  
  
 	 	 	 M y W i d g e t s . P a n e l   k d P a n e l   =   n e w   M y W i d g e t s . P a n e l ( t h i s . r i g h t P a n e l ) ;  
 	 	 	 k d P a n e l . B r i g h t n e s s   =   0 . 9 ;  
 	 	 	 k d P a n e l . D r a w F u l l F r a m e   =   t r u e ;  
 	 	 	 k d P a n e l . D r a w S c r e w   =   t r u e ;  
 	 	 	 k d P a n e l . P r e f e r r e d H e i g h t   =   1 0 0 ;     / /   m i n i m u m   q u i   s e r a   é t e n d u  
 	 	 	 k d P a n e l . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   0 ,   0 ,   0 ) ;  
 	 	 	 k d P a n e l . P a d d i n g   =   n e w   M a r g i n s ( 1 2 ,   0 ,   1 0 ,   2 ) ;  
 	 	 	 k d P a n e l . D o c k   =   D o c k S t y l e . B o t t o m ;  
  
 	 	 	 / / 	 C r é e   l e   c o n t e n u   d e s   d i f f é r e n t e s   p a r t i e s .  
 	 	 	 t h i s . C r e a t e O p t i o n s ( t h i s . l e f t H e a d e r ) ;  
 	 	 	 t h i s . C r e a t e C l o c k C o n t r o l ( t h i s . l e f t C l o c k ) ;  
 	 	 	 t h i s . C r e a t e B u s P a n e l ( t h i s . l e f t P a n e l B u s ) ;  
 	 	 	 t h i s . C r e a t e D e t a i l P a n e l ( t h i s . l e f t P a n e l D e t a i l ) ;  
 	 	 	 t h i s . C r e a t e C o d e P a n e l ( t h i s . l e f t P a n e l C o d e ) ;  
 	 	 	 t h i s . C r e a t e C a l m P a n e l ( t h i s . l e f t P a n e l C a l m ) ;  
 	 	 	 t h i s . C r e a t e Q u i c k P a n e l ( t h i s . l e f t P a n e l Q u i c k ) ;  
 	 	 	 t h i s . C r e a t e H e l p ( t h i s . h e l p P a n e l ) ;  
 	 	 	 t h i s . C r e a t e K e y b o a r d D i s p l a y ( k d P a n e l ) ;  
  
 	 	 	 t h i s . P r o c e s s o r F e e d b a c k ( ) ;  
 	 	 	 t h i s . U p d a t e S a v e ( ) ;  
 	 	 	 t h i s . U p d a t e P a n e l M o d e ( ) ;  
 	 	 	 t h i s . U p d a t e M e m o r y B a n k ( ) ;  
 	 	 }  
  
 	 	 p u b l i c   b o o l   Q u i t ( )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   a v a n t   d e   q u i t t e r   l ' a p p l i c a t i o n .  
 	 	 	 / / 	 R e t o u r n e   t r u e   s ' i l   e s t   p o s s i b l e   d e   q u i t t e r .  
 	 	 	 r e t u r n   t h i s . A u t o S a v e ( ) ;  
 	 	 }  
  
 	 	 p u b l i c   M y W i d g e t s . M e m o r y A c c e s s o r   M e m o r y A c c e s s o r  
 	 	 {  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 r e t u r n   t h i s . m e m o r y A c c e s s o r ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p u b l i c   M y W i d g e t s . C o d e A c c e s s o r   C o d e A c c e s s o r  
 	 	 {  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 r e t u r n   t h i s . c o d e A c c e s s o r ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p u b l i c   L i s t < M y W i d g e t s . D i g i t >   D i s p l a y D i g i t s  
 	 	 {  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 r e t u r n   t h i s . d i s p l a y D i g i t s ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p u b l i c   M y W i d g e t s . D i s p l a y   D i s p l a y B i t m a p  
 	 	 {  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 r e t u r n   t h i s . d i s p l a y B i t m a p ;  
 	 	 	 }  
 	 	 }  
  
  
 	 	 p r o t e c t e d   v o i d   C r e a t e O p t i o n s ( M y W i d g e t s . P a n e l   p a r e n t )  
 	 	 {  
 	 	 	 / / 	 C r é e   l a   p a r t i e   s u p é r i e u r e   d e   p a n n e a u   d e   g a u c h e .  
 	 	 	 t h i s . b u t t o n M o d e B u s   =   n e w   M y W i d g e t s . P u s h B u t t o n ( p a r e n t ) ;  
 	 	 	 t h i s . b u t t o n M o d e B u s . T e x t   =   R e s . S t r i n g s . T a b P a g e . B u s . B u t t o n ;  
 	 	 	 t h i s . b u t t o n M o d e B u s . N a m e   =   " B u s " ;  
 	 	 	 t h i s . b u t t o n M o d e B u s . P r e f e r r e d S i z e   =   n e w   S i z e ( 7 8 ,   2 4 ) ;  
 	 	 	 t h i s . b u t t o n M o d e B u s . M a r g i n s   =   n e w   M a r g i n s ( 6 0 + 1 0 + 1 ,   0 ,   0 ,   0 ) ;  
 	 	 	 t h i s . b u t t o n M o d e B u s . D o c k   =   D o c k S t y l e . L e f t ;  
 	 	 	 t h i s . b u t t o n M o d e B u s . C l i c k e d   + =   t h i s . H a n d l e B u t t o n M o d e C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . b u t t o n M o d e B u s ,   R e s . S t r i n g s . T a b P a g e . B u s . T o o l t i p ) ;  
  
 	 	 	 t h i s . b u t t o n M o d e D e t a i l   =   n e w   M y W i d g e t s . P u s h B u t t o n ( p a r e n t ) ;  
 	 	 	 t h i s . b u t t o n M o d e D e t a i l . T e x t   =   R e s . S t r i n g s . T a b P a g e . D e t a i l . B u t t o n ;  
 	 	 	 t h i s . b u t t o n M o d e D e t a i l . N a m e   =   " D e t a i l " ;  
 	 	 	 t h i s . b u t t o n M o d e D e t a i l . P r e f e r r e d S i z e   =   n e w   S i z e ( 7 8 ,   2 4 ) ;  
 	 	 	 t h i s . b u t t o n M o d e D e t a i l . M a r g i n s   =   n e w   M a r g i n s ( 2 ,   0 ,   0 ,   0 ) ;  
 	 	 	 t h i s . b u t t o n M o d e D e t a i l . D o c k   =   D o c k S t y l e . L e f t ;  
 	 	 	 t h i s . b u t t o n M o d e D e t a i l . C l i c k e d   + =   t h i s . H a n d l e B u t t o n M o d e C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . b u t t o n M o d e D e t a i l ,   R e s . S t r i n g s . T a b P a g e . D e t a i l . T o o l t i p ) ;  
  
 	 	 	 t h i s . b u t t o n M o d e C o d e   =   n e w   M y W i d g e t s . P u s h B u t t o n ( p a r e n t ) ;  
 	 	 	 t h i s . b u t t o n M o d e C o d e . T e x t   =   R e s . S t r i n g s . T a b P a g e . C o d e . B u t t o n ;  
 	 	 	 t h i s . b u t t o n M o d e C o d e . N a m e   =   " C o d e " ;  
 	 	 	 t h i s . b u t t o n M o d e C o d e . P r e f e r r e d S i z e   =   n e w   S i z e ( 7 8 ,   2 4 ) ;  
 	 	 	 t h i s . b u t t o n M o d e C o d e . M a r g i n s   =   n e w   M a r g i n s ( 2 ,   0 ,   0 ,   0 ) ;  
 	 	 	 t h i s . b u t t o n M o d e C o d e . D o c k   =   D o c k S t y l e . L e f t ;  
 	 	 	 t h i s . b u t t o n M o d e C o d e . C l i c k e d   + =   t h i s . H a n d l e B u t t o n M o d e C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . b u t t o n M o d e C o d e ,   R e s . S t r i n g s . T a b P a g e . C o d e . T o o l t i p ) ;  
  
 	 	 	 t h i s . b u t t o n M o d e C a l m   =   n e w   M y W i d g e t s . P u s h B u t t o n ( p a r e n t ) ;  
 	 	 	 t h i s . b u t t o n M o d e C a l m . T e x t   =   R e s . S t r i n g s . T a b P a g e . C a l m . B u t t o n ;  
 	 	 	 t h i s . b u t t o n M o d e C a l m . N a m e   =   " C a l m " ;  
 	 	 	 t h i s . b u t t o n M o d e C a l m . P r e f e r r e d S i z e   =   n e w   S i z e ( 7 8 ,   2 4 ) ;  
 	 	 	 t h i s . b u t t o n M o d e C a l m . M a r g i n s   =   n e w   M a r g i n s ( 2 ,   0 ,   0 ,   0 ) ;  
 	 	 	 t h i s . b u t t o n M o d e C a l m . D o c k   =   D o c k S t y l e . L e f t ;  
 	 	 	 t h i s . b u t t o n M o d e C a l m . C l i c k e d   + =   t h i s . H a n d l e B u t t o n M o d e C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . b u t t o n M o d e C a l m ,   R e s . S t r i n g s . T a b P a g e . C a l m . T o o l t i p ) ;  
  
 	 	 	 t h i s . b u t t o n M o d e Q u i c k   =   n e w   M y W i d g e t s . P u s h B u t t o n ( p a r e n t ) ;  
 	 	 	 t h i s . b u t t o n M o d e Q u i c k . T e x t   =   R e s . S t r i n g s . T a b P a g e . Q u i c k . B u t t o n ;  
 	 	 	 t h i s . b u t t o n M o d e Q u i c k . N a m e   =   " Q u i c k " ;  
 	 	 	 t h i s . b u t t o n M o d e Q u i c k . P r e f e r r e d S i z e   =   n e w   S i z e ( 7 8 ,   2 4 ) ;  
 	 	 	 t h i s . b u t t o n M o d e Q u i c k . M a r g i n s   =   n e w   M a r g i n s ( 2 ,   0 ,   0 ,   0 ) ;  
 	 	 	 t h i s . b u t t o n M o d e Q u i c k . D o c k   =   D o c k S t y l e . L e f t ;  
 	 	 	 t h i s . b u t t o n M o d e Q u i c k . C l i c k e d   + =   t h i s . H a n d l e B u t t o n M o d e C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . b u t t o n M o d e Q u i c k ,   R e s . S t r i n g s . T a b P a g e . Q u i c k . T o o l t i p ) ;  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   C r e a t e B u s P a n e l ( M y W i d g e t s . P a n e l   p a r e n t )  
 	 	 {  
 	 	 	 / / 	 C r é e   l e   p a n n e a u   d e   g a u c h e   c o m p l e t   a v e c   l e s   b u s .  
 	 	 	 M y W i d g e t s . P a n e l   t o p ,   b o t t o m ;  
 	 	 	 t h i s . C r e a t e B i t s P a n e l ( p a r e n t ,   o u t   t o p ,   o u t   b o t t o m ,   R e s . S t r i n g s . B u s . T i t l e . D a t a ) ;  
  
 	 	 	 t h i s . d a t a D i g i t s   =   n e w   L i s t < M y W i d g e t s . D i g i t > ( ) ;  
 	 	 	 f o r   ( i n t   i = 0 ;   i < C o m p o n e n t s . M e m o r y . T o t a l D a t a / 4 ;   i + + )  
 	 	 	 {  
 	 	 	 	 t h i s . C r e a t e B i t D i g i t ( t o p ,   i ,   t h i s . d a t a D i g i t s ) ;  
 	 	 	 }  
  
 	 	 	 t h i s . d a t a L e d s   =   n e w   L i s t < M y W i d g e t s . L e d > ( ) ;  
 	 	 	 t h i s . d a t a S w i t c h s   =   n e w   L i s t < M y W i d g e t s . S w i t c h > ( ) ;  
 	 	 	 f o r   ( i n t   i = 0 ;   i < C o m p o n e n t s . M e m o r y . T o t a l D a t a ;   i + + )  
 	 	 	 {  
 	 	 	 	 t h i s . C r e a t e B i t B u t t o n ( b o t t o m ,   i ,   C o m p o n e n t s . M e m o r y . T o t a l D a t a ,   t h i s . d a t a L e d s ,   t h i s . d a t a S w i t c h s ) ;  
 	 	 	 	 t h i s . d a t a S w i t c h s [ i ] . C l i c k e d   + =   t h i s . H a n d l e D a t a S w i t c h C l i c k e d ;  
 	 	 	 }  
  
 	 	 	 / / 	 P a n n e a u   d e s   a d r e s s e s .  
 	 	 	 t h i s . C r e a t e B i t s P a n e l ( p a r e n t ,   o u t   t o p ,   o u t   b o t t o m ,   R e s . S t r i n g s . B u s . T i t l e . A d d r e s s ) ;  
  
 	 	 	 t h i s . a d d r e s s D i g i t s   =   n e w   L i s t < M y W i d g e t s . D i g i t > ( ) ;  
 	 	 	 f o r   ( i n t   i = 0 ;   i < C o m p o n e n t s . M e m o r y . T o t a l A d d r e s s / 4 ;   i + + )  
 	 	 	 {  
 	 	 	 	 t h i s . C r e a t e B i t D i g i t ( t o p ,   i ,   t h i s . a d d r e s s D i g i t s ) ;  
 	 	 	 }  
 	 	 	  
 	 	 	 t h i s . a d d r e s s L e d s   =   n e w   L i s t < M y W i d g e t s . L e d > ( ) ;  
 	 	 	 t h i s . a d d r e s s S w i t c h s   =   n e w   L i s t < M y W i d g e t s . S w i t c h > ( ) ;  
 	 	 	 f o r   ( i n t   i = 0 ;   i < C o m p o n e n t s . M e m o r y . T o t a l A d d r e s s ;   i + + )  
 	 	 	 {  
 	 	 	 	 t h i s . C r e a t e B i t B u t t o n ( b o t t o m ,   i ,   C o m p o n e n t s . M e m o r y . T o t a l A d d r e s s ,   t h i s . a d d r e s s L e d s ,   t h i s . a d d r e s s S w i t c h s ) ;  
 	 	 	 	 t h i s . a d d r e s s S w i t c h s [ i ] . C l i c k e d   + =   t h i s . H a n d l e A d d r e s s S w i t c h C l i c k e d ;  
 	 	 	 }  
  
 	 	 	 t h i s . A d d r e s s B i t s   =   0 ;  
 	 	 	 t h i s . D a t a B i t s   =   0 ;  
 	 	 	 t h i s . U p d a t e B u t t o n s ( ) ;  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   C r e a t e C o d e P a n e l ( M y W i d g e t s . P a n e l   p a r e n t )  
 	 	 {  
 	 	 	 / / 	 C r é e   l e   p a n n e a u   d e   g a u c h e   p o u r   l e   c o d e .  
 	 	 	 M y W i d g e t s . P a n e l   h e a d e r ;  
 	 	 	 M y W i d g e t s . P a n e l   c o d e P a n e l   =   t h i s . C r e a t e P a n e l W i t h T i t l e ( p a r e n t ,   R e s . S t r i n g s . C o d e . T i t l e . D a t a ,   o u t   h e a d e r ) ;  
 	 	 	 c o d e P a n e l . P r e f e r r e d H e i g h t   =   4 7 + 1 9 * 1 7 + 1 ;     / /   p l a c e   p o u r   1 7   i n s t r u c t i o n s  
 	 	 	 c o d e P a n e l . D o c k   =   D o c k S t y l e . B o t t o m ;  
  
 	 	 	 t h i s . c o d e B u t t o n P C   =   n e w   M y W i d g e t s . P u s h B u t t o n ( h e a d e r ) ;  
 	 	 	 t h i s . c o d e B u t t o n P C . T e x t   =   R e s . S t r i n g s . C o d e . P C . B u t t o n ;  
 	 	 	 t h i s . c o d e B u t t o n P C . P r e f e r r e d S i z e   =   n e w   S i z e ( 2 2 ,   2 2 ) ;  
 	 	 	 t h i s . c o d e B u t t o n P C . M a r g i n s   =   n e w   M a r g i n s ( 1 0 + 1 7 ,   8 ,   0 ,   3 ) ;  
 	 	 	 t h i s . c o d e B u t t o n P C . D o c k   =   D o c k S t y l e . L e f t ;  
 	 	 	 t h i s . c o d e B u t t o n P C . C l i c k e d   + =   t h i s . H a n d l e C o d e B u t t o n P C C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . c o d e B u t t o n P C ,   R e s . S t r i n g s . C o d e . P C . T o o l t i p ) ;  
  
 	 	 	 t h i s . c o d e B u t t o n M   =   n e w   M y W i d g e t s . P u s h B u t t o n ( h e a d e r ) ;  
 	 	 	 t h i s . c o d e B u t t o n M . T e x t   =   R e s . S t r i n g s . C o d e . R A M . B u t t o n ;  
 	 	 	 t h i s . c o d e B u t t o n M . N a m e   =   " M " ;  
 	 	 	 t h i s . c o d e B u t t o n M . P r e f e r r e d S i z e   =   n e w   S i z e ( 3 6 ,   2 2 ) ;  
 	 	 	 t h i s . c o d e B u t t o n M . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   2 ,   0 ,   3 ) ;  
 	 	 	 t h i s . c o d e B u t t o n M . D o c k   =   D o c k S t y l e . L e f t ;  
 	 	 	 t h i s . c o d e B u t t o n M . C l i c k e d   + =   t h i s . H a n d l e C o d e B u t t o n C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . c o d e B u t t o n M ,   R e s . S t r i n g s . C o d e . R A M . T o o l t i p ) ;  
  
 	 	 	 t h i s . c o d e B u t t o n R   =   n e w   M y W i d g e t s . P u s h B u t t o n ( h e a d e r ) ;  
 	 	 	 t h i s . c o d e B u t t o n R . T e x t   =   R e s . S t r i n g s . C o d e . R O M . B u t t o n ;  
 	 	 	 t h i s . c o d e B u t t o n R . N a m e   =   " R " ;  
 	 	 	 t h i s . c o d e B u t t o n R . P r e f e r r e d S i z e   =   n e w   S i z e ( 3 6 ,   2 2 ) ;  
 	 	 	 t h i s . c o d e B u t t o n R . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   2 ,   0 ,   3 ) ;  
 	 	 	 t h i s . c o d e B u t t o n R . D o c k   =   D o c k S t y l e . L e f t ;  
 	 	 	 t h i s . c o d e B u t t o n R . C l i c k e d   + =   t h i s . H a n d l e C o d e B u t t o n C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . c o d e B u t t o n R ,   R e s . S t r i n g s . C o d e . R O M . T o o l t i p ) ;  
  
 	 	 	 t h i s . c o d e B u t t o n S u b   =   n e w   M y W i d g e t s . P u s h B u t t o n ( h e a d e r ) ;  
 	 	 	 t h i s . c o d e B u t t o n S u b . T e x t   =   " "" ;  
 	 	 	 t h i s . c o d e B u t t o n S u b . N a m e   =   " S U B " ;  
 	 	 	 t h i s . c o d e B u t t o n S u b . E n a b l e   =   f a l s e ;  
 	 	 	 t h i s . c o d e B u t t o n S u b . A u t o F o c u s   =   f a l s e ;  
 	 	 	 t h i s . c o d e B u t t o n S u b . P r e f e r r e d S i z e   =   n e w   S i z e ( 2 2 ,   2 2 ) ;  
 	 	 	 t h i s . c o d e B u t t o n S u b . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   1 5 ,   0 ,   3 ) ;  
 	 	 	 t h i s . c o d e B u t t o n S u b . D o c k   =   D o c k S t y l e . R i g h t ;  
 	 	 	 t h i s . c o d e B u t t o n S u b . C l i c k e d   + =   t h i s . H a n d l e C o d e S u b B u t t o n C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . c o d e B u t t o n S u b ,   R e s . S t r i n g s . C o d e . S u b . T o o l t i p ) ;  
  
 	 	 	 t h i s . c o d e B u t t o n A d d   =   n e w   M y W i d g e t s . P u s h B u t t o n ( h e a d e r ) ;  
 	 	 	 t h i s . c o d e B u t t o n A d d . T e x t   =   " + " ;  
 	 	 	 t h i s . c o d e B u t t o n A d d . N a m e   =   " A D D " ;  
 	 	 	 t h i s . c o d e B u t t o n A d d . E n a b l e   =   f a l s e ;  
 	 	 	 t h i s . c o d e B u t t o n A d d . A u t o F o c u s   =   f a l s e ;  
 	 	 	 t h i s . c o d e B u t t o n A d d . P r e f e r r e d S i z e   =   n e w   S i z e ( 2 2 ,   2 2 ) ;  
 	 	 	 t h i s . c o d e B u t t o n A d d . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   2 ,   0 ,   3 ) ;  
 	 	 	 t h i s . c o d e B u t t o n A d d . D o c k   =   D o c k S t y l e . R i g h t ;  
 	 	 	 t h i s . c o d e B u t t o n A d d . C l i c k e d   + =   t h i s . H a n d l e C o d e A d d B u t t o n C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . c o d e B u t t o n A d d ,   R e s . S t r i n g s . C o d e . A d d . T o o l t i p ) ;  
  
 	 	 	 t h i s . c o d e A c c e s s o r   =   n e w   M y W i d g e t s . C o d e A c c e s s o r ( c o d e P a n e l ) ;  
 	 	 	 t h i s . c o d e A c c e s s o r . P r o c e s s o r   =   t h i s . p r o c e s s o r ;  
 	 	 	 t h i s . c o d e A c c e s s o r . M e m o r y   =   t h i s . m e m o r y ;  
 	 	 	 t h i s . c o d e A c c e s s o r . B a n k   =   " M " ;  
 	 	 	 t h i s . c o d e A c c e s s o r . M a r g i n s   =   n e w   M a r g i n s ( 1 0 ,   1 0 ,   0 ,   0 ) ;  
 	 	 	 t h i s . c o d e A c c e s s o r . D o c k   =   D o c k S t y l e . F i l l ;  
 	 	 	 t h i s . c o d e A c c e s s o r . I n s t r u c t i o n S e l e c t e d   + =   n e w   E v e n t H a n d l e r ( t h i s . H a n d l e C o d e A c c e s s o r I n s t r u c t i o n S e l e c t e d ) ;  
 	 	 	 t h i s . c o d e A c c e s s o r . B a n k C h a n g e d   + =   n e w   E v e n t H a n d l e r ( t h i s . H a n d l e C o d e A c c e s s o r B a n k C h a n g e d ) ;  
  
 	 	 	 / / 	 P a r t i e   p o u r   l e   p r o c e s s e u r .  
 	 	 	 M y W i d g e t s . P a n e l   p r o c e s s o r P a n e l   =   t h i s . C r e a t e P a n e l W i t h T i t l e ( p a r e n t ,   R e s . S t r i n g s . C o d e . T i t l e . R e g i s t e r ,   o u t   h e a d e r ) ;  
 	 	 	 p r o c e s s o r P a n e l . P r e f e r r e d H e i g h t   =   1 0 ;     / /   m i n u s c u l e   ( s e r a   é t e n d u )  
 	 	 	 p r o c e s s o r P a n e l . D o c k   =   D o c k S t y l e . B o t t o m ;  
  
 	 	 	 t h i s . c o d e R e g i s t e r s   =   n e w   L i s t < T e x t F i e l d > ( ) ;  
 	 	 	 b o o l   f i r s t   =   t r u e ;  
 	 	 	 f o r e a c h   ( s t r i n g   n a m e   i n   t h i s . p r o c e s s o r . R e g i s t e r N a m e s )  
 	 	 	 {  
 	 	 	 	 S t a t i c T e x t   l a b e l   =   n e w   S t a t i c T e x t ( p r o c e s s o r P a n e l ) ;  
 	 	 	 	 l a b e l . T e x t   =   n a m e ;  
 	 	 	 	 l a b e l . P r e f e r r e d W i d t h   =   2 0 ;  
 	 	 	 	 l a b e l . C o n t e n t A l i g n m e n t   =   C o n t e n t A l i g n m e n t . M i d d l e R i g h t ;  
 	 	 	 	 l a b e l . M a r g i n s   =   n e w   M a r g i n s ( f i r s t ? 1 0 : 0 ,   4 ,   0 ,   0 ) ;  
 	 	 	 	 l a b e l . D o c k   =   D o c k S t y l e . L e f t ;  
  
 	 	 	 	 i n t   b i t C o u n t   =   t h i s . p r o c e s s o r . G e t R e g i s t e r S i z e ( n a m e ) ;  
  
 	 	 	 	 T e x t F i e l d   f i e l d   =   n e w   T e x t F i e l d ( p r o c e s s o r P a n e l ) ;  
 	 	 	 	 f i e l d . N a m e   =   n a m e ;  
 	 	 	 	 f i e l d . P r e f e r r e d W i d t h   =   M y W i d g e t s . T e x t F i e l d H e x a . G e t H e x a W i d t h ( b i t C o u n t ) ;  
 	 	 	 	 f i e l d . M a x L e n g t h   =   ( b i t C o u n t + 3 ) / 4 ;  
 	 	 	 	 f i e l d . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   2 ,   0 ,   0 ) ;  
 	 	 	 	 f i e l d . D o c k   =   D o c k S t y l e . L e f t ;  
 	 	 	 	 f i e l d . T e x t C h a n g e d   + =   n e w   E v e n t H a n d l e r ( t h i s . H a n d l e P r o c e s s o r R e g i s t e r C h a n g e d ) ;  
  
 	 	 	 	 t h i s . c o d e R e g i s t e r s . A d d ( f i e l d ) ;  
 	 	 	 	 f i r s t   =   f a l s e ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   C r e a t e C a l m P a n e l ( M y W i d g e t s . P a n e l   p a r e n t )  
 	 	 {  
 	 	 	 / / 	 C r é e   l e   p a n n e a u   d e   g a u c h e   p o u r   l ' a s s e m b l e u r   C A L M .  
 	 	 	 M y W i d g e t s . P a n e l   h e a d e r ;  
 	 	 	 t h i s . c a l m P a n e l   =   t h i s . C r e a t e P a n e l W i t h T i t l e ( p a r e n t ,   R e s . S t r i n g s . C a l m . T i t l e . M a i n ,   o u t   h e a d e r ) ;  
 	 	 	 t h i s . c a l m P a n e l . D r a w S c r e w   =   f a l s e ;  
 	 	 	 t h i s . c a l m P a n e l . P a d d i n g   =   n e w   M a r g i n s ( 0 ,   0 ,   4 ,   5 ) ;  
 	 	 	 t h i s . c a l m P a n e l . P r e f e r r e d H e i g h t   =   D o l p h i n A p p l i c a t i o n . P a n e l H e i g h t ;  
 	 	 	 t h i s . c a l m P a n e l . D o c k   =   D o c k S t y l e . B o t t o m ;  
  
 	 	 	 t h i s . c a l m B u t t o n O p e n   =   n e w   I c o n B u t t o n ( h e a d e r ) ;  
 	 	 	 t h i s . c a l m B u t t o n O p e n . A u t o F o c u s   =   f a l s e ;  
 	 	 	 t h i s . c a l m B u t t o n O p e n . I c o n N a m e   =   M i s c . I c o n ( " O p e n C a l m " ) ;  
 	 	 	 t h i s . c a l m B u t t o n O p e n . M a r g i n s   =   n e w   M a r g i n s ( 5 ,   0 ,   0 ,   1 ) ;  
 	 	 	 t h i s . c a l m B u t t o n O p e n . D o c k   =   D o c k S t y l e . L e f t ;  
 	 	 	 t h i s . c a l m B u t t o n O p e n . C l i c k e d   + =   t h i s . H a n d l e C a l m O p e n C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . c a l m B u t t o n O p e n ,   R e s . S t r i n g s . C a l m . O p e n . T o o l t i p ) ;  
  
 	 	 	 t h i s . c a l m B u t t o n S a v e   =   n e w   I c o n B u t t o n ( h e a d e r ) ;  
 	 	 	 t h i s . c a l m B u t t o n S a v e . A u t o F o c u s   =   f a l s e ;  
 	 	 	 t h i s . c a l m B u t t o n S a v e . I c o n N a m e   =   M i s c . I c o n ( " S a v e C a l m " ) ;  
 	 	 	 t h i s . c a l m B u t t o n S a v e . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   0 ,   0 ,   1 ) ;  
 	 	 	 t h i s . c a l m B u t t o n S a v e . D o c k   =   D o c k S t y l e . L e f t ;  
 	 	 	 t h i s . c a l m B u t t o n S a v e . C l i c k e d   + =   t h i s . H a n d l e C a l m S a v e C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . c a l m B u t t o n S a v e ,   R e s . S t r i n g s . C a l m . S a v e . T o o l t i p ) ;  
  
 	 	 	 t h i s . c a l m B u t t o n S h o w   =   n e w   I c o n B u t t o n ( h e a d e r ) ;  
 	 	 	 t h i s . c a l m B u t t o n S h o w . B u t t o n S t y l e   =   B u t t o n S t y l e . A c t i v a b l e I c o n ;  
 	 	 	 t h i s . c a l m B u t t o n S h o w . A u t o F o c u s   =   f a l s e ;  
 	 	 	 t h i s . c a l m B u t t o n S h o w . I c o n N a m e   =   M i s c . I c o n ( " T e x t S h o w C o n t r o l C h a r a c t e r s " ) ;  
 	 	 	 t h i s . c a l m B u t t o n S h o w . M a r g i n s   =   n e w   M a r g i n s ( 5 ,   0 ,   0 ,   1 ) ;  
 	 	 	 t h i s . c a l m B u t t o n S h o w . D o c k   =   D o c k S t y l e . L e f t ;  
 	 	 	 t h i s . c a l m B u t t o n S h o w . C l i c k e d   + =   t h i s . H a n d l e C a l m S h o w C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . c a l m B u t t o n S h o w ,   R e s . S t r i n g s . C a l m . S h o w . T o o l t i p ) ;  
  
 	 	 	 t h i s . c a l m B u t t o n B i g   =   n e w   I c o n B u t t o n ( h e a d e r ) ;  
 	 	 	 t h i s . c a l m B u t t o n B i g . B u t t o n S t y l e   =   B u t t o n S t y l e . A c t i v a b l e I c o n ;  
 	 	 	 t h i s . c a l m B u t t o n B i g . A u t o F o c u s   =   f a l s e ;  
 	 	 	 t h i s . c a l m B u t t o n B i g . I c o n N a m e   =   M i s c . I c o n ( " F o n t B i g " ) ;  
 	 	 	 t h i s . c a l m B u t t o n B i g . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   0 ,   0 ,   1 ) ;  
 	 	 	 t h i s . c a l m B u t t o n B i g . D o c k   =   D o c k S t y l e . L e f t ;  
 	 	 	 t h i s . c a l m B u t t o n B i g . C l i c k e d   + =   t h i s . H a n d l e C a l m B i g C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . c a l m B u t t o n B i g ,   R e s . S t r i n g s . C a l m . B i g . T o o l t i p ) ;  
  
 	 	 	 t h i s . c a l m B u t t o n F u l l   =   n e w   I c o n B u t t o n ( h e a d e r ) ;  
 	 	 	 t h i s . c a l m B u t t o n F u l l . B u t t o n S t y l e   =   B u t t o n S t y l e . A c t i v a b l e I c o n ;  
 	 	 	 t h i s . c a l m B u t t o n F u l l . A u t o F o c u s   =   f a l s e ;  
 	 	 	 t h i s . c a l m B u t t o n F u l l . I c o n N a m e   =   M i s c . I c o n ( " F u l l S c r e e n " ) ;  
 	 	 	 t h i s . c a l m B u t t o n F u l l . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   0 ,   0 ,   1 ) ;  
 	 	 	 t h i s . c a l m B u t t o n F u l l . D o c k   =   D o c k S t y l e . L e f t ;  
 	 	 	 t h i s . c a l m B u t t o n F u l l . C l i c k e d   + =   t h i s . H a n d l e C a l m F u l l C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . c a l m B u t t o n F u l l ,   R e s . S t r i n g s . C a l m . F u l l . T o o l t i p ) ;  
  
 	 	 	 t h i s . c a l m B u t t o n A s s   =   n e w   M y W i d g e t s . P u s h B u t t o n ( h e a d e r ) ;  
 	 	 	 t h i s . c a l m B u t t o n A s s . T e x t   =   R e s . S t r i n g s . C a l m . A s s . B u t t o n ;  
 	 	 	 t h i s . c a l m B u t t o n A s s . A u t o F o c u s   =   f a l s e ;  
 	 	 	 t h i s . c a l m B u t t o n A s s . P r e f e r r e d S i z e   =   n e w   S i z e ( 8 0 ,   2 2 ) ;  
 	 	 	 t h i s . c a l m B u t t o n A s s . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   5 ,   0 ,   3 ) ;  
 	 	 	 t h i s . c a l m B u t t o n A s s . D o c k   =   D o c k S t y l e . R i g h t ;  
 	 	 	 t h i s . c a l m B u t t o n A s s . C l i c k e d   + =   t h i s . H a n d l e C a l m A s s e m b l e r C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . c a l m B u t t o n A s s ,   R e s . S t r i n g s . C a l m . A s s . T o o l t i p ) ;  
  
 	 	 	 t h i s . c a l m B u t t o n E r r   =   n e w   M y W i d g e t s . P u s h B u t t o n ( h e a d e r ) ;  
 	 	 	 t h i s . c a l m B u t t o n E r r . T e x t   =   R e s . S t r i n g s . C a l m . E r r . B u t t o n ;  
 	 	 	 t h i s . c a l m B u t t o n E r r . A u t o F o c u s   =   f a l s e ;  
 	 	 	 t h i s . c a l m B u t t o n E r r . P r e f e r r e d S i z e   =   n e w   S i z e ( 3 6 ,   2 2 ) ;  
 	 	 	 t h i s . c a l m B u t t o n E r r . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   1 ,   0 ,   3 ) ;  
 	 	 	 t h i s . c a l m B u t t o n E r r . D o c k   =   D o c k S t y l e . R i g h t ;  
 	 	 	 t h i s . c a l m B u t t o n E r r . C l i c k e d   + =   t h i s . H a n d l e C a l m E r r o r C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . c a l m B u t t o n E r r ,   R e s . S t r i n g s . C a l m . E r r . T o o l t i p ) ;  
  
 	 	 	 t h i s . c a l m E d i t o r   =   n e w   T e x t F i e l d M u l t i ( t h i s . c a l m P a n e l ) ;  
 	 	 	 t h i s . c a l m E d i t o r . M a x L e n g t h   =   1 0 0 0 0 0 ;  
 	 	 	 / / ? t h i s . c a l m E d i t o r . T e x t L a y o u t . D e f a u l t F o n t   =   F o n t . G e t F o n t ( " C o u r i e r   N e w " ,   " R e g u l a r " ) ;  
 	 	 	 t h i s . c a l m E d i t o r . T e x t N a v i g a t o r . A l l o w T a b I n s e r t i o n   =   t r u e ;  
 	 	 	 t h i s . c a l m E d i t o r . M a r g i n s   =   n e w   M a r g i n s ( 5 ,   5 ,   0 ,   0 ) ;  
 	 	 	 t h i s . c a l m E d i t o r . D o c k   =   D o c k S t y l e . F i l l ;  
 	 	 	 t h i s . c a l m E d i t o r . T e x t C h a n g e d   + =   n e w   E v e n t H a n d l e r ( t h i s . H a n d l e C a l m E d i t o r T e x t C h a n g e d ) ;  
  
 	 	 	 t h i s . U p d a t e C a l m E d i t o r ( ) ;  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   C r e a t e Q u i c k P a n e l ( M y W i d g e t s . P a n e l   p a r e n t )  
 	 	 {  
 	 	 	 / / 	 C r é e   l e   p a n n e a u   d e   g a u c h e   v i d e ,   p o u r   l e   m o d e   r a p i d e .  
 	 	 	 M y W i d g e t s . P a n e l   p a n e l   =   t h i s . C r e a t e P a n e l W i t h T i t l e ( p a r e n t ,   R e s . S t r i n g s . Q u i c k . T i t l e . M a i n ) ;  
 	 	 	 p a n e l . P r e f e r r e d H e i g h t   =   D o l p h i n A p p l i c a t i o n . P a n e l H e i g h t ;  
 	 	 	 p a n e l . D o c k   =   D o c k S t y l e . B o t t o m ;  
  
 	 	 	 S t a t i c T e x t   l a b e l   =   n e w   S t a t i c T e x t ( p a n e l ) ;  
 	 	 	 l a b e l . T e x t   =   R e s . S t r i n g s . Q u i c k . L a b e l . D e f a u l t ;  
 	 	 	 l a b e l . C o n t e n t A l i g n m e n t   =   C o n t e n t A l i g n m e n t . M i d d l e C e n t e r ;  
 	 	 	 l a b e l . D o c k   =   D o c k S t y l e . F i l l ;  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   C r e a t e D e t a i l P a n e l ( M y W i d g e t s . P a n e l   p a r e n t )  
 	 	 {  
 	 	 	 / / 	 C r é e   l e   p a n n e a u   d e   g a u c h e   d é t a i l l é   c o m p l e t .  
 	 	 	 M y W i d g e t s . P a n e l   h e a d e r ;  
 	 	 	 M y W i d g e t s . P a n e l   m e m o r y P a n e l   =   t h i s . C r e a t e P a n e l W i t h T i t l e ( p a r e n t ,   R e s . S t r i n g s . D e t a i l . T i t l e . M e m o r y ,   o u t   h e a d e r ) ;  
 	 	 	 m e m o r y P a n e l . P r e f e r r e d H e i g h t   =   4 7 + 2 1 * 1 0 ;     / /   p l a c e   p o u r   1 0   a d r e s s e s  
 	 	 	 m e m o r y P a n e l . D o c k   =   D o c k S t y l e . B o t t o m ;  
  
 	 	 	 t h i s . m e m o r y B u t t o n P C   =   n e w   M y W i d g e t s . P u s h B u t t o n ( h e a d e r ) ;  
 	 	 	 t h i s . m e m o r y B u t t o n P C . T e x t   =   R e s . S t r i n g s . D e t a i l . P C . B u t t o n ;  
 	 	 	 t h i s . m e m o r y B u t t o n P C . P r e f e r r e d S i z e   =   n e w   S i z e ( 2 2 ,   2 2 ) ;  
 	 	 	 t h i s . m e m o r y B u t t o n P C . M a r g i n s   =   n e w   M a r g i n s ( 1 0 + 1 7 ,   8 ,   0 ,   3 ) ;  
 	 	 	 t h i s . m e m o r y B u t t o n P C . D o c k   =   D o c k S t y l e . L e f t ;  
 	 	 	 t h i s . m e m o r y B u t t o n P C . C l i c k e d   + =   t h i s . H a n d l e M e m o r y B u t t o n P C C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . m e m o r y B u t t o n P C ,   R e s . S t r i n g s . D e t a i l . P C . T o o l t i p ) ;  
  
 	 	 	 t h i s . m e m o r y B u t t o n M   =   n e w   M y W i d g e t s . P u s h B u t t o n ( h e a d e r ) ;  
 	 	 	 t h i s . m e m o r y B u t t o n M . T e x t   =   R e s . S t r i n g s . D e t a i l . R A M . B u t t o n ;  
 	 	 	 t h i s . m e m o r y B u t t o n M . N a m e   =   " M " ;  
 	 	 	 t h i s . m e m o r y B u t t o n M . P r e f e r r e d S i z e   =   n e w   S i z e ( 3 6 ,   2 2 ) ;  
 	 	 	 t h i s . m e m o r y B u t t o n M . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   2 ,   0 ,   3 ) ;  
 	 	 	 t h i s . m e m o r y B u t t o n M . D o c k   =   D o c k S t y l e . L e f t ;  
 	 	 	 t h i s . m e m o r y B u t t o n M . C l i c k e d   + =   t h i s . H a n d l e M e m o r y B u t t o n C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . m e m o r y B u t t o n M ,   R e s . S t r i n g s . D e t a i l . R A M . T o o l t i p ) ;  
  
 	 	 	 t h i s . m e m o r y B u t t o n R   =   n e w   M y W i d g e t s . P u s h B u t t o n ( h e a d e r ) ;  
 	 	 	 t h i s . m e m o r y B u t t o n R . T e x t   =   R e s . S t r i n g s . D e t a i l . R O M . B u t t o n ;  
 	 	 	 t h i s . m e m o r y B u t t o n R . N a m e   =   " R " ;  
 	 	 	 t h i s . m e m o r y B u t t o n R . P r e f e r r e d S i z e   =   n e w   S i z e ( 3 6 ,   2 2 ) ;  
 	 	 	 t h i s . m e m o r y B u t t o n R . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   2 ,   0 ,   3 ) ;  
 	 	 	 t h i s . m e m o r y B u t t o n R . D o c k   =   D o c k S t y l e . L e f t ;  
 	 	 	 t h i s . m e m o r y B u t t o n R . C l i c k e d   + =   t h i s . H a n d l e M e m o r y B u t t o n C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . m e m o r y B u t t o n R ,   R e s . S t r i n g s . D e t a i l . R O M . T o o l t i p ) ;  
  
 	 	 	 t h i s . m e m o r y B u t t o n P   =   n e w   M y W i d g e t s . P u s h B u t t o n ( h e a d e r ) ;  
 	 	 	 t h i s . m e m o r y B u t t o n P . T e x t   =   R e s . S t r i n g s . D e t a i l . P e r i p h . B u t t o n ;  
 	 	 	 t h i s . m e m o r y B u t t o n P . N a m e   =   " P " ;  
 	 	 	 t h i s . m e m o r y B u t t o n P . P r e f e r r e d S i z e   =   n e w   S i z e ( 3 6 ,   2 2 ) ;  
 	 	 	 t h i s . m e m o r y B u t t o n P . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   2 ,   0 ,   3 ) ;  
 	 	 	 t h i s . m e m o r y B u t t o n P . D o c k   =   D o c k S t y l e . L e f t ;  
 	 	 	 t h i s . m e m o r y B u t t o n P . C l i c k e d   + =   t h i s . H a n d l e M e m o r y B u t t o n C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . m e m o r y B u t t o n P ,   R e s . S t r i n g s . D e t a i l . P e r i p h . T o o l t i p ) ;  
  
 	 	 	 t h i s . m e m o r y B u t t o n D   =   n e w   M y W i d g e t s . P u s h B u t t o n ( h e a d e r ) ;  
 	 	 	 t h i s . m e m o r y B u t t o n D . T e x t   =   R e s . S t r i n g s . D e t a i l . D i s p l a y . B u t t o n ;  
 	 	 	 t h i s . m e m o r y B u t t o n D . N a m e   =   " D " ;  
 	 	 	 t h i s . m e m o r y B u t t o n D . P r e f e r r e d S i z e   =   n e w   S i z e ( 3 6 ,   2 2 ) ;  
 	 	 	 t h i s . m e m o r y B u t t o n D . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   2 ,   0 ,   3 ) ;  
 	 	 	 t h i s . m e m o r y B u t t o n D . D o c k   =   D o c k S t y l e . L e f t ;  
 	 	 	 t h i s . m e m o r y B u t t o n D . C l i c k e d   + =   t h i s . H a n d l e M e m o r y B u t t o n C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . m e m o r y B u t t o n D ,   R e s . S t r i n g s . D e t a i l . D i s p l a y . T o o l t i p ) ;  
  
 	 	 	 t h i s . m e m o r y A c c e s s o r   =   n e w   M y W i d g e t s . M e m o r y A c c e s s o r ( m e m o r y P a n e l ) ;  
 	 	 	 t h i s . m e m o r y A c c e s s o r . M e m o r y   =   t h i s . m e m o r y ;  
 	 	 	 t h i s . m e m o r y A c c e s s o r . B a n k   =   " M " ;  
 	 	 	 t h i s . m e m o r y A c c e s s o r . M a r g i n s   =   n e w   M a r g i n s ( 1 0 ,   1 0 ,   0 ,   0 ) ;  
 	 	 	 t h i s . m e m o r y A c c e s s o r . D o c k   =   D o c k S t y l e . F i l l ;  
  
 	 	 	 / / 	 P a r t i e   p o u r   l e   p r o c e s s e u r .  
 	 	 	 M y W i d g e t s . P a n e l   p r o c e s s o r P a n e l   =   t h i s . C r e a t e P a n e l W i t h T i t l e ( p a r e n t ,   R e s . S t r i n g s . D e t a i l . T i t l e . R e g i s t e r ,   o u t   h e a d e r ) ;  
 	 	 	 p r o c e s s o r P a n e l . P r e f e r r e d H e i g h t   =   1 0 ;     / /   m i n u s c u l e   ( s e r a   é t e n d u )  
 	 	 	 p r o c e s s o r P a n e l . D o c k   =   D o c k S t y l e . B o t t o m ;  
  
 	 	 	 t h i s . b u t t o n R e s e t   =   n e w   M y W i d g e t s . P u s h B u t t o n ( h e a d e r ) ;  
 	 	 	 t h i s . b u t t o n R e s e t . T e x t   =   R e s . S t r i n g s . D e t a i l . R e s e t . B u t t o n ;  
 	 	 	 t h i s . b u t t o n R e s e t . P r e f e r r e d S i z e   =   n e w   S i z e ( 4 0 ,   2 2 ) ;  
 	 	 	 t h i s . b u t t o n R e s e t . M a r g i n s   =   n e w   M a r g i n s ( 1 0 + 1 7 ,   2 ,   0 ,   3 ) ;  
 	 	 	 t h i s . b u t t o n R e s e t . D o c k   =   D o c k S t y l e . L e f t ;  
 	 	 	 t h i s . b u t t o n R e s e t . C l i c k e d   + =   t h i s . H a n d l e B u t t o n R e s e t C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . b u t t o n R e s e t ,   R e s . S t r i n g s . D e t a i l . R e s e t . T o o l t i p ) ;  
  
 	 	 	 t h i s . r e g i s t e r F i e l d s   =   n e w   L i s t < M y W i d g e t s . T e x t F i e l d H e x a > ( ) ;  
 	 	 	 i n t   i n d e x   =   1 0 0 ;  
 	 	 	 f o r e a c h   ( s t r i n g   n a m e   i n   t h i s . p r o c e s s o r . R e g i s t e r N a m e s )  
 	 	 	 {  
 	 	 	 	 M y W i d g e t s . T e x t F i e l d H e x a   f i e l d   =   t h i s . C r e a t e P r o c e s s o r R e g i s t e r ( p r o c e s s o r P a n e l ,   n a m e ) ;  
 	 	 	 	 f i e l d . N a m e   =   n a m e ;  
 	 	 	 	 f i e l d . B i t N a m e s   =   t h i s . p r o c e s s o r . G e t R e g i s t e r B i t N a m e s ( n a m e ) ;  
 	 	 	 	 f i e l d . S e t T a b I n d e x ( i n d e x + + ) ;  
 	 	 	 	 f i e l d . M a r g i n s   =   n e w   M a r g i n s ( 1 0 + 1 7 ,   0 ,   0 ,   1 ) ;     / /   l a i s s e   l a   l a r g e u r   d ' u n   S c r o l l e r  
 	 	 	 	 f i e l d . D o c k   =   D o c k S t y l e . T o p ;  
 	 	 	 	 f i e l d . H e x a V a l u e C h a n g e d   + =   n e w   E v e n t H a n d l e r ( t h i s . H a n d l e P r o c e s s o r H e x a V a l u e C h a n g e d ) ;  
  
 	 	 	 	 t h i s . r e g i s t e r F i e l d s . A d d ( f i e l d ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   M y W i d g e t s . T e x t F i e l d H e x a   C r e a t e P r o c e s s o r R e g i s t e r ( M y W i d g e t s . P a n e l   p a r e n t ,   s t r i n g   n a m e )  
 	 	 {  
 	 	 	 / / 	 C r é e   l e   w i d g e t   c o m p l e x e   p o u r   r e p r é s e n t e r   u n   r e g i s t r e   d u   p r o c e s s e u r .  
 	 	 	 M y W i d g e t s . T e x t F i e l d H e x a   f i e l d   =   n e w   M y W i d g e t s . T e x t F i e l d H e x a ( p a r e n t ) ;  
  
 	 	 	 f i e l d . B i t C o u n t   =   t h i s . p r o c e s s o r . G e t R e g i s t e r S i z e ( n a m e ) ;  
 	 	 	 f i e l d . L a b e l   =   n a m e ;  
 	 	 	 f i e l d . P r e f e r r e d H e i g h t   =   2 0 ;  
 	 	 	 f i e l d . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   0 ,   0 ,   1 ) ;  
  
 	 	 	 r e t u r n   f i e l d ;  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   C r e a t e C l o c k C o n t r o l ( M y W i d g e t s . P a n e l   p a r e n t )  
 	 	 {  
 	 	 	 / / 	 C r é e   l e s   w i d g e t s   p o u r   l e   c o n t r ô l e   d e   l ' h o r l o g e   d u   p r o c e s s e u r   ( b o u t o n   R / S ,   e t c . ) .  
 	 	 	 t h i s . b u t t o n R u n   =   n e w   M y W i d g e t s . P u s h B u t t o n ( p a r e n t ) ;  
 	 	 	 / / ? t h i s . b u t t o n R u n . T e x t   =   " < f o n t   s i z e = \ " 2 0 0 % \ " > < b > R / S < / b > < / f o n t > " ;  
 	 	 	 / / ? t h i s . b u t t o n R u n . T e x t   =   " < f o n t   s i z e = \ " 1 5 0 % \ " > < b > R U N < / b > < / f o n t > " ;  
 	 	 	 t h i s . b u t t o n R u n . P r e f e r r e d S i z e   =   n e w   S i z e ( 5 0 ,   5 0 ) ;  
 	 	 	 t h i s . b u t t o n R u n . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   0 ,   1 0 ,   0 ) ;  
 	 	 	 t h i s . b u t t o n R u n . D o c k   =   D o c k S t y l e . T o p ;  
 	 	 	 t h i s . b u t t o n R u n . C l i c k e d   + =   t h i s . H a n d l e B u t t o n R u n C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . b u t t o n R u n ,   R e s . S t r i n g s . C o m m a n d . R u n . T o o l t i p ) ;  
  
 	 	 	 t h i s . b u t t o n C l o c k 6   =   n e w   M y W i d g e t s . P u s h B u t t o n ( p a r e n t ) ;  
 	 	 	 t h i s . b u t t o n C l o c k 6 . I n d e x   =   1 0 0 0 0 0 0 ;  
 	 	 	 t h i s . b u t t o n C l o c k 6 . T e x t   =   R e s . S t r i n g s . C o m m a n d . C l o c k 6 . B u t t o n ;  
 	 	 	 t h i s . b u t t o n C l o c k 6 . P r e f e r r e d S i z e   =   n e w   S i z e ( 5 0 ,   2 0 ) ;  
 	 	 	 t h i s . b u t t o n C l o c k 6 . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   0 ,   8 ,   0 ) ;  
 	 	 	 t h i s . b u t t o n C l o c k 6 . D o c k   =   D o c k S t y l e . T o p ;  
 	 	 	 t h i s . b u t t o n C l o c k 6 . C l i c k e d   + =   t h i s . H a n d l e B u t t o n C l o c k C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . b u t t o n C l o c k 6 ,   R e s . S t r i n g s . C o m m a n d . C l o c k 6 . T o o l t i p ) ;  
  
 	 	 	 t h i s . b u t t o n C l o c k 5   =   n e w   M y W i d g e t s . P u s h B u t t o n ( p a r e n t ) ;  
 	 	 	 t h i s . b u t t o n C l o c k 5 . I n d e x   =   1 0 0 0 0 0 ;  
 	 	 	 t h i s . b u t t o n C l o c k 5 . T e x t   =   R e s . S t r i n g s . C o m m a n d . C l o c k 5 . B u t t o n ;  
 	 	 	 t h i s . b u t t o n C l o c k 5 . P r e f e r r e d S i z e   =   n e w   S i z e ( 5 0 ,   2 0 ) ;  
 	 	 	 t h i s . b u t t o n C l o c k 5 . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   0 ,   1 ,   0 ) ;  
 	 	 	 t h i s . b u t t o n C l o c k 5 . D o c k   =   D o c k S t y l e . T o p ;  
 	 	 	 t h i s . b u t t o n C l o c k 5 . C l i c k e d   + =   t h i s . H a n d l e B u t t o n C l o c k C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . b u t t o n C l o c k 5 ,   R e s . S t r i n g s . C o m m a n d . C l o c k 5 . T o o l t i p ) ;  
  
 	 	 	 t h i s . b u t t o n C l o c k 4   =   n e w   M y W i d g e t s . P u s h B u t t o n ( p a r e n t ) ;  
 	 	 	 t h i s . b u t t o n C l o c k 4 . I n d e x   =   1 0 0 0 0 ;  
 	 	 	 t h i s . b u t t o n C l o c k 4 . T e x t   =   R e s . S t r i n g s . C o m m a n d . C l o c k 4 . B u t t o n ;  
 	 	 	 t h i s . b u t t o n C l o c k 4 . P r e f e r r e d S i z e   =   n e w   S i z e ( 5 0 ,   2 0 ) ;  
 	 	 	 t h i s . b u t t o n C l o c k 4 . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   0 ,   1 ,   0 ) ;  
 	 	 	 t h i s . b u t t o n C l o c k 4 . D o c k   =   D o c k S t y l e . T o p ;  
 	 	 	 t h i s . b u t t o n C l o c k 4 . C l i c k e d   + =   t h i s . H a n d l e B u t t o n C l o c k C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . b u t t o n C l o c k 4 ,   R e s . S t r i n g s . C o m m a n d . C l o c k 4 . T o o l t i p ) ;  
  
 	 	 	 t h i s . b u t t o n C l o c k 3   =   n e w   M y W i d g e t s . P u s h B u t t o n ( p a r e n t ) ;  
 	 	 	 t h i s . b u t t o n C l o c k 3 . I n d e x   =   1 0 0 0 ;  
 	 	 	 t h i s . b u t t o n C l o c k 3 . T e x t   =   R e s . S t r i n g s . C o m m a n d . C l o c k 3 . B u t t o n ;  
 	 	 	 t h i s . b u t t o n C l o c k 3 . P r e f e r r e d S i z e   =   n e w   S i z e ( 5 0 ,   2 0 ) ;  
 	 	 	 t h i s . b u t t o n C l o c k 3 . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   0 ,   1 ,   0 ) ;  
 	 	 	 t h i s . b u t t o n C l o c k 3 . D o c k   =   D o c k S t y l e . T o p ;  
 	 	 	 t h i s . b u t t o n C l o c k 3 . C l i c k e d   + =   t h i s . H a n d l e B u t t o n C l o c k C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . b u t t o n C l o c k 3 ,   R e s . S t r i n g s . C o m m a n d . C l o c k 3 . T o o l t i p ) ;  
  
 	 	 	 t h i s . b u t t o n C l o c k 2   =   n e w   M y W i d g e t s . P u s h B u t t o n ( p a r e n t ) ;  
 	 	 	 t h i s . b u t t o n C l o c k 2 . I n d e x   =   1 0 0 ;  
 	 	 	 t h i s . b u t t o n C l o c k 2 . T e x t   =   R e s . S t r i n g s . C o m m a n d . C l o c k 2 . B u t t o n ;  
 	 	 	 t h i s . b u t t o n C l o c k 2 . P r e f e r r e d S i z e   =   n e w   S i z e ( 5 0 ,   2 0 ) ;  
 	 	 	 t h i s . b u t t o n C l o c k 2 . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   0 ,   1 ,   0 ) ;  
 	 	 	 t h i s . b u t t o n C l o c k 2 . D o c k   =   D o c k S t y l e . T o p ;  
 	 	 	 t h i s . b u t t o n C l o c k 2 . C l i c k e d   + =   t h i s . H a n d l e B u t t o n C l o c k C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . b u t t o n C l o c k 2 ,   R e s . S t r i n g s . C o m m a n d . C l o c k 2 . T o o l t i p ) ;  
  
 	 	 	 t h i s . b u t t o n C l o c k 1   =   n e w   M y W i d g e t s . P u s h B u t t o n ( p a r e n t ) ;  
 	 	 	 t h i s . b u t t o n C l o c k 1 . I n d e x   =   1 0 ;  
 	 	 	 t h i s . b u t t o n C l o c k 1 . T e x t   =   R e s . S t r i n g s . C o m m a n d . C l o c k 1 . B u t t o n ;  
 	 	 	 t h i s . b u t t o n C l o c k 1 . P r e f e r r e d S i z e   =   n e w   S i z e ( 5 0 ,   2 0 ) ;  
 	 	 	 t h i s . b u t t o n C l o c k 1 . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   0 ,   1 ,   0 ) ;  
 	 	 	 t h i s . b u t t o n C l o c k 1 . D o c k   =   D o c k S t y l e . T o p ;  
 	 	 	 t h i s . b u t t o n C l o c k 1 . C l i c k e d   + =   t h i s . H a n d l e B u t t o n C l o c k C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . b u t t o n C l o c k 1 ,   R e s . S t r i n g s . C o m m a n d . C l o c k 1 . T o o l t i p ) ;  
  
 	 	 	 t h i s . b u t t o n C l o c k 0   =   n e w   M y W i d g e t s . P u s h B u t t o n ( p a r e n t ) ;  
 	 	 	 t h i s . b u t t o n C l o c k 0 . I n d e x   =   1 ;  
 	 	 	 t h i s . b u t t o n C l o c k 0 . T e x t   =   R e s . S t r i n g s . C o m m a n d . C l o c k 0 . B u t t o n ;  
 	 	 	 t h i s . b u t t o n C l o c k 0 . P r e f e r r e d S i z e   =   n e w   S i z e ( 5 0 ,   2 0 ) ;  
 	 	 	 t h i s . b u t t o n C l o c k 0 . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   0 ,   1 ,   0 ) ;  
 	 	 	 t h i s . b u t t o n C l o c k 0 . D o c k   =   D o c k S t y l e . T o p ;  
 	 	 	 t h i s . b u t t o n C l o c k 0 . C l i c k e d   + =   t h i s . H a n d l e B u t t o n C l o c k C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . b u t t o n C l o c k 0 ,   R e s . S t r i n g s . C o m m a n d . C l o c k 0 . T o o l t i p ) ;  
  
 	 	 	 M y W i d g e t s . P a n e l   s t e p L a b e l s   =   t h i s . C r e a t e S w i t c h H o r i z o n a l L a b e l s ( p a r e n t ,   R e s . S t r i n g s . C o m m a n d . S w i t c h S t e p . N o ,   R e s . S t r i n g s . C o m m a n d . S w i t c h S t e p . Y e s ) ;  
 	 	 	 s t e p L a b e l s . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   0 ,   5 ,   0 ) ;  
 	 	 	 s t e p L a b e l s . D o c k   =   D o c k S t y l e . T o p ;  
  
 	 	 	 t h i s . s w i t c h S t e p   =   n e w   M y W i d g e t s . S w i t c h ( p a r e n t ) ;  
 	 	 	 t h i s . s w i t c h S t e p . P r e f e r r e d S i z e   =   n e w   S i z e ( 5 0 ,   2 0 ) ;  
 	 	 	 t h i s . s w i t c h S t e p . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   0 ,   0 ,   5 ) ;  
 	 	 	 t h i s . s w i t c h S t e p . D o c k   =   D o c k S t y l e . T o p ;  
 	 	 	 t h i s . s w i t c h S t e p . C l i c k e d   + =   t h i s . H a n d l e S w i t c h S t e p C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . s w i t c h S t e p ,   R e s . S t r i n g s . C o m m a n d . S w i t c h S t e p . T o o l t i p ) ;  
  
 	 	 	 t h i s . b u t t o n S t e p   =   n e w   M y W i d g e t s . P u s h B u t t o n ( p a r e n t ) ;  
 	 	 	 t h i s . b u t t o n S t e p . T e x t   =   M i s c . B o l d ( M i s c . F o n t S i z e ( R e s . S t r i n g s . C o m m a n d . S t e p . B u t t o n ,   2 0 0 ) ) ;  
 	 	 	 t h i s . b u t t o n S t e p . P r e f e r r e d S i z e   =   n e w   S i z e ( 5 0 ,   5 0 ) ;  
 	 	 	 t h i s . b u t t o n S t e p . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   0 ,   0 ,   0 ) ;  
 	 	 	 t h i s . b u t t o n S t e p . D o c k   =   D o c k S t y l e . T o p ;  
 	 	 	 t h i s . b u t t o n S t e p . C l i c k e d   + =   t h i s . H a n d l e B u t t o n S t e p C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . b u t t o n S t e p ,   R e s . S t r i n g s . C o m m a n d . S t e p . T o o l t i p ) ;  
  
 	 	 	 M y W i d g e t s . P a n e l   i n t o L a b e l s   =   t h i s . C r e a t e S w i t c h H o r i z o n a l L a b e l s ( p a r e n t ,   R e s . S t r i n g s . C o m m a n d . S w i t c h I n t o . N o ,   R e s . S t r i n g s . C o m m a n d . S w i t c h I n t o . Y e s ) ;  
 	 	 	 i n t o L a b e l s . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   0 ,   5 ,   0 ) ;  
 	 	 	 i n t o L a b e l s . D o c k   =   D o c k S t y l e . T o p ;  
  
 	 	 	 t h i s . s w i t c h I n t o   =   n e w   M y W i d g e t s . S w i t c h ( p a r e n t ) ;  
 	 	 	 t h i s . s w i t c h I n t o . P r e f e r r e d S i z e   =   n e w   S i z e ( 5 0 ,   2 0 ) ;  
 	 	 	 t h i s . s w i t c h I n t o . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   0 ,   0 ,   5 ) ;  
 	 	 	 t h i s . s w i t c h I n t o . D o c k   =   D o c k S t y l e . T o p ;  
 	 	 	 t h i s . s w i t c h I n t o . C l i c k e d   + =   t h i s . H a n d l e S w i t c h I n t o C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . s w i t c h I n t o ,   R e s . S t r i n g s . C o m m a n d . S w i t c h I n t o . T o o l t i p ) ;  
  
 	 	 	 / / 	 P a r t i e   i n f é r i e u r e   g a u c h e   p o u r   l e   c o n t r ô l e   d e s   b u s .  
 	 	 	 t h i s . c l o c k B u s P a n e l   =   n e w   M y W i d g e t s . P a n e l ( p a r e n t ) ;  
 	 	 	 t h i s . c l o c k B u s P a n e l . P r e f e r r e d W i d t h   =   4 0 ;  
 	 	 	 t h i s . c l o c k B u s P a n e l . D o c k   =   D o c k S t y l e . B o t t o m ;  
  
 	 	 	 t h i s . b u t t o n M e m o r y R e a d   =   n e w   M y W i d g e t s . P u s h B u t t o n ( t h i s . c l o c k B u s P a n e l ) ;  
 	 	 	 t h i s . b u t t o n M e m o r y R e a d . T e x t   =   M i s c . B o l d ( M i s c . F o n t S i z e ( R e s . S t r i n g s . C o m m a n d . R e a d . B u t t o n ,   2 0 0 ) ) ;  
 	 	 	 t h i s . b u t t o n M e m o r y R e a d . P r e f e r r e d S i z e   =   n e w   S i z e ( 5 0 ,   5 0 ) ;  
 	 	 	 t h i s . b u t t o n M e m o r y R e a d . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   0 ,   0 ,   2 ) ;  
 	 	 	 t h i s . b u t t o n M e m o r y R e a d . D o c k   =   D o c k S t y l e . T o p ;  
 	 	 	 t h i s . b u t t o n M e m o r y R e a d . P r e s s e d   + =   t h i s . H a n d l e B u t t o n M e m o r y P r e s s e d ;  
 	 	 	 t h i s . b u t t o n M e m o r y R e a d . R e l e a s e d   + =   t h i s . H a n d l e B u t t o n M e m o r y R e l e a s e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . b u t t o n M e m o r y R e a d ,   R e s . S t r i n g s . C o m m a n d . R e a d . T o o l t i p ) ;  
  
 	 	 	 t h i s . b u t t o n M e m o r y W r i t e   =   n e w   M y W i d g e t s . P u s h B u t t o n ( t h i s . c l o c k B u s P a n e l ) ;  
 	 	 	 t h i s . b u t t o n M e m o r y W r i t e . T e x t   =   M i s c . B o l d ( M i s c . F o n t S i z e ( R e s . S t r i n g s . C o m m a n d . W r i t e . B u t t o n ,   2 0 0 ) ) ;  
 	 	 	 t h i s . b u t t o n M e m o r y W r i t e . P r e f e r r e d S i z e   =   n e w   S i z e ( 5 0 ,   5 0 ) ;  
 	 	 	 t h i s . b u t t o n M e m o r y W r i t e . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   0 ,   0 ,   0 ) ;  
 	 	 	 t h i s . b u t t o n M e m o r y W r i t e . D o c k   =   D o c k S t y l e . T o p ;  
 	 	 	 t h i s . b u t t o n M e m o r y W r i t e . P r e s s e d   + =   t h i s . H a n d l e B u t t o n M e m o r y P r e s s e d ;  
 	 	 	 t h i s . b u t t o n M e m o r y W r i t e . R e l e a s e d   + =   t h i s . H a n d l e B u t t o n M e m o r y R e l e a s e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . b u t t o n M e m o r y W r i t e ,   R e s . S t r i n g s . C o m m a n d . W r i t e . T o o l t i p ) ;  
  
 	 	 	 t h i s . U p d a t e C l o c k B u t t o n s ( ) ;  
 	 	 }  
  
 	 	 p r o t e c t e d   M y W i d g e t s . L e d   C r e a t e L a b e l e d L e d ( M y W i d g e t s . P a n e l   p a r e n t ,   s t r i n g   t e x t )  
 	 	 {  
 	 	 	 / / 	 C r é e   u n e   l e d   d a n s   u n   g r o s   p a n n e a u   a v e c   u n   t e x t e   e x p l i c a t i f .  
 	 	 	 M y W i d g e t s . P a n e l   p a n e l   =   n e w   M y W i d g e t s . P a n e l ( p a r e n t ) ;  
  
 	 	 	 p a n e l . B r i g h t n e s s   =   0 . 8 ;  
 	 	 	 p a n e l . D r a w F u l l F r a m e   =   t r u e ;  
 	 	 	 p a n e l . P r e f e r r e d W i d t h   =   p a r e n t . P r e f e r r e d W i d t h ;  
 	 	 	 p a n e l . P r e f e r r e d H e i g h t   =   6 0 ;  
 	 	 	 p a n e l . P a d d i n g   =   n e w   M a r g i n s ( 0 ,   0 ,   1 0 ,   5 ) ;  
 	 	 	 p a n e l . D o c k   =   D o c k S t y l e . T o p ;  
  
 	 	 	 M y W i d g e t s . L e d   l e d   =   n e w   M y W i d g e t s . L e d ( p a n e l ) ;  
 	 	 	 l e d . P r e f e r r e d W i d t h   =   p a r e n t . P r e f e r r e d W i d t h ;  
 	 	 	 l e d . D o c k   =   D o c k S t y l e . F i l l ;  
  
 	 	 	 S t a t i c T e x t   l a b e l   =   n e w   S t a t i c T e x t ( p a n e l ) ;  
 	 	 	 l a b e l . T e x t   =   t e x t ;  
 	 	 	 l a b e l . C o n t e n t A l i g n m e n t   =   C o n t e n t A l i g n m e n t . M i d d l e C e n t e r ;  
 	 	 	 l a b e l . P r e f e r r e d W i d t h   =   p a r e n t . P r e f e r r e d W i d t h ;  
 	 	 	 l a b e l . P r e f e r r e d H e i g h t   =   1 6 ;  
 	 	 	 l a b e l . D o c k   =   D o c k S t y l e . B o t t o m ;  
  
 	 	 	 r e t u r n   l e d ;  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   C r e a t e B i t s P a n e l ( M y W i d g e t s . P a n e l   p a r e n t ,   o u t   M y W i d g e t s . P a n e l   t o p ,   o u t   M y W i d g e t s . P a n e l   b o t t o m ,   s t r i n g   t i t l e )  
 	 	 {  
 	 	 	 / / 	 C r é e   u n   p a n n e a u   r e c e v a n t   d e s   b o u t o n s   ( l e d   +   s w i t c h )   p o u r   d e s   b i t s .  
 	 	 	 M y W i d g e t s . P a n e l   p a n e l   =   t h i s . C r e a t e P a n e l W i t h T i t l e ( p a r e n t ,   t i t l e ) ;  
 	 	 	 p a n e l . D o c k   =   D o c k S t y l e . B o t t o m ;  
 	 	 	  
 	 	 	 t o p   =   n e w   M y W i d g e t s . P a n e l ( p a n e l ) ;  
 	 	 	 t o p . P r e f e r r e d H e i g h t   =   5 0 ;  
 	 	 	 t o p . P a d d i n g   =   n e w   M a r g i n s ( 0 ,   2 0 ,   0 ,   5 ) ;  
 	 	 	 t o p . D o c k   =   D o c k S t y l e . T o p ;  
 	 	 	  
 	 	 	 b o t t o m   =   n e w   M y W i d g e t s . P a n e l ( p a n e l ) ;  
 	 	 	 b o t t o m . D o c k   =   D o c k S t y l e . F i l l ;  
  
 	 	 	 t h i s . C r e a t e S w i t c h V e r t i c a l L a b e l s ( b o t t o m ,   " < b > 0 < / b > " ,   " < b > 1 < / b > " ) ;  
 	 	 }  
  
 	 	 p r o t e c t e d   M y W i d g e t s . P a n e l   C r e a t e P a n e l W i t h T i t l e ( M y W i d g e t s . P a n e l   p a r e n t ,   s t r i n g   t i t l e )  
 	 	 {  
 	 	 	 M y W i d g e t s . P a n e l   h e a d e r ;  
 	 	 	 r e t u r n   t h i s . C r e a t e P a n e l W i t h T i t l e ( p a r e n t ,   t i t l e ,   o u t   h e a d e r ) ;  
 	 	 }  
  
 	 	 p r o t e c t e d   M y W i d g e t s . P a n e l   C r e a t e P a n e l W i t h T i t l e ( M y W i d g e t s . P a n e l   p a r e n t ,   s t r i n g   t i t l e ,   o u t   M y W i d g e t s . P a n e l   h e a d e r )  
 	 	 {  
 	 	 	 / / 	 C r é e   u n   p a n n e a u   a v e c   u n   t i t r e   e n   h a u t .  
 	 	 	 M y W i d g e t s . P a n e l   p a n e l   =   n e w   M y W i d g e t s . P a n e l ( p a r e n t ) ;  
 	 	 	 p a n e l . M i n W i d t h   =   D o l p h i n A p p l i c a t i o n . P a n e l W i d t h ;  
 	 	 	 p a n e l . M a x W i d t h   =   D o l p h i n A p p l i c a t i o n . P a n e l W i d t h ;  
 	 	 	 p a n e l . B r i g h t n e s s   =   0 . 8 ;  
 	 	 	 p a n e l . D r a w F u l l F r a m e   =   t r u e ;  
 	 	 	 p a n e l . D r a w S c r e w   =   t r u e ;  
 	 	 	 p a n e l . P r e f e r r e d H e i g h t   =   1 9 5 ;  
 	 	 	 p a n e l . P a d d i n g   =   n e w   M a r g i n s ( 0 ,   0 ,   4 ,   1 2 ) ;  
 	 	 	 p a n e l . M a r g i n s   =   n e w   M a r g i n s ( 1 0 ,   1 0 ,   1 0 ,   0 ) ;  
  
 	 	 	 h e a d e r   =   n e w   M y W i d g e t s . P a n e l ( p a n e l ) ;  
 	 	 	 h e a d e r . P r e f e r r e d H e i g h t   =   2 5 ;  
 	 	 	 h e a d e r . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   0 ,   0 ,   0 ) ;  
 	 	 	 h e a d e r . D o c k   =   D o c k S t y l e . T o p ;  
  
 	 	 	 S t a t i c T e x t   l a b e l   =   n e w   S t a t i c T e x t ( h e a d e r ) ;  
 	 	 	 l a b e l . T e x t   =   M i s c . B o l d ( M i s c . F o n t S i z e ( t i t l e ,   1 5 0 ) ) ;  
 	 	 	 l a b e l . C o n t e n t A l i g n m e n t   =   C o n t e n t A l i g n m e n t . T o p C e n t e r ;  
 	 	 	 l a b e l . P r e f e r r e d H e i g h t   =   2 5 ;  
 	 	 	 l a b e l . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   0 ,   0 ,   0 ) ;  
 	 	 	 l a b e l . D o c k   =   D o c k S t y l e . F i l l ;  
  
 	 	 	 M y W i d g e t s . L i n e   s e p   =   n e w   M y W i d g e t s . L i n e ( p a n e l ) ;  
 	 	 	 s e p . P r e f e r r e d H e i g h t   =   1 ;  
 	 	 	 s e p . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   0 ,   0 ,   5 ) ;  
 	 	 	 s e p . D o c k   =   D o c k S t y l e . T o p ;  
  
 	 	 	 r e t u r n   p a n e l ;  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   C r e a t e B i t D i g i t ( M y W i d g e t s . P a n e l   p a r e n t ,   i n t   r a n k ,   L i s t < M y W i d g e t s . D i g i t >   d i g i t s )  
 	 	 {  
 	 	 	 / / 	 C r é e   u n   d i g i t   p o u r   u n   g r o u p e   d e   4   b i t s .  
 	 	 	 M y W i d g e t s . D i g i t   d i g i t   =   n e w   M y W i d g e t s . D i g i t ( p a r e n t ) ;  
 	 	 	 d i g i t . P r e f e r r e d W i d t h   =   3 0 ;  
 	 	 	 d i g i t . M a r g i n s   =   n e w   M a r g i n s ( 3 7 + 1 8 ,   3 7 ,   0 ,   0 ) ;  
 	 	 	 d i g i t . D o c k   =   D o c k S t y l e . R i g h t ;  
  
 	 	 	 d i g i t s . A d d ( d i g i t ) ;  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   C r e a t e B i t B u t t o n ( M y W i d g e t s . P a n e l   p a r e n t ,   i n t   r a n k ,   i n t   t o t a l ,   L i s t < M y W i d g e t s . L e d >   l e d s ,   L i s t < M y W i d g e t s . S w i t c h >   s w i t c h s )  
 	 	 {  
 	 	 	 / / 	 C r é e   u n   b o u t o n   ( l e d   +   s w i t c h )   p o u r   u n   b i t .  
 	 	 	 M y W i d g e t s . P a n e l   g r o u p   =   n e w   M y W i d g e t s . P a n e l ( p a r e n t ) ;  
 	 	 	 g r o u p . P r e f e r r e d S i z e   =   n e w   S i z e ( 2 4 ,   2 4 ) ;  
 	 	 	 g r o u p . M a r g i n s   =   n e w   M a r g i n s ( ( ( r a n k + 1 ) % 4   = =   0 )   ?   2 + 1 8 / 2 - 1 : 2 ,   0 ,   0 ,   0 ) ;  
 	 	 	 g r o u p . D o c k   =   D o c k S t y l e . R i g h t ;  
  
 	 	 	 S t a t i c T e x t   l a b e l   =   n e w   S t a t i c T e x t ( g r o u p ) ;  
 	 	 	 l a b e l . T e x t   =   r a n k . T o S t r i n g ( ) ;  
 	 	 	 l a b e l . C o n t e n t A l i g n m e n t   =   C o n t e n t A l i g n m e n t . M i d d l e C e n t e r ;  
 	 	 	 l a b e l . P r e f e r r e d W i d t h   =   2 4 ;  
 	 	 	 l a b e l . P r e f e r r e d H e i g h t   =   2 0 ;  
 	 	 	 l a b e l . D o c k   =   D o c k S t y l e . T o p ;  
  
 	 	 	 M y W i d g e t s . L e d   s t a t e   =   n e w   M y W i d g e t s . L e d ( g r o u p ) ;  
 	 	 	 s t a t e . I n d e x   =   r a n k ;  
 	 	 	 s t a t e . P r e f e r r e d W i d t h   =   2 4 ;  
 	 	 	 s t a t e . P r e f e r r e d H e i g h t   =   2 4 ;  
 	 	 	 s t a t e . D o c k   =   D o c k S t y l e . T o p ;  
  
 	 	 	 M y W i d g e t s . S w i t c h   b u t t o n   =   n e w   M y W i d g e t s . S w i t c h ( g r o u p ) ;  
 	 	 	 b u t t o n . P r e f e r r e d W i d t h   =   2 4 ;  
 	 	 	 b u t t o n . M a r g i n s   =   n e w   M a r g i n s ( 2 ,   2 ,   5 ,   0 ) ;  
 	 	 	 b u t t o n . D o c k   =   D o c k S t y l e . F i l l ;  
  
 	 	 	 i f   ( ( r a n k + 1 ) % 4   = =   0   & &   r a n k   <   t o t a l - 1 )  
 	 	 	 {  
 	 	 	 	 M y W i d g e t s . L i n e   s e p   =   n e w   M y W i d g e t s . L i n e ( p a r e n t ) ;  
 	 	 	 	 s e p . P r e f e r r e d W i d t h   =   1 ;  
 	 	 	 	 s e p . M a r g i n s   =   n e w   M a r g i n s ( 1 8 / 2 ,   0 ,   0 ,   0 ) ;  
 	 	 	 	 s e p . D o c k   =   D o c k S t y l e . R i g h t ;  
 	 	 	 }  
  
 	 	 	 l e d s . A d d ( s t a t e ) ;  
 	 	 	 s w i t c h s . A d d ( b u t t o n ) ;  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   C r e a t e S w i t c h V e r t i c a l L a b e l s ( M y W i d g e t s . P a n e l   p a r e n t ,   s t r i n g   n o ,   s t r i n g   y e s )  
 	 	 {  
 	 	 	 M y W i d g e t s . P a n e l   l a b e l s   =   n e w   M y W i d g e t s . P a n e l ( p a r e n t ) ;  
 	 	 	 l a b e l s . P r e f e r r e d W i d t h   =   2 0 ;  
 	 	 	 l a b e l s . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   1 0 ,   0 ,   0 ) ;  
 	 	 	 l a b e l s . D o c k   =   D o c k S t y l e . R i g h t ;  
  
 	 	 	 S t a t i c T e x t   l a b e l ;  
  
 	 	 	 l a b e l   =   n e w   S t a t i c T e x t ( l a b e l s ) ;  
 	 	 	 l a b e l . T e x t   =   n o ;  
 	 	 	 l a b e l . C o n t e n t A l i g n m e n t   =   C o n t e n t A l i g n m e n t . M i d d l e C e n t e r ;  
 	 	 	 l a b e l . P r e f e r r e d W i d t h   =   2 0 ;  
 	 	 	 l a b e l . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   0 ,   5 ,   5 ) ;  
 	 	 	 l a b e l . D o c k   =   D o c k S t y l e . B o t t o m ;  
  
 	 	 	 l a b e l   =   n e w   S t a t i c T e x t ( l a b e l s ) ;  
 	 	 	 l a b e l . T e x t   =   y e s ;  
 	 	 	 l a b e l . C o n t e n t A l i g n m e n t   =   C o n t e n t A l i g n m e n t . M i d d l e C e n t e r ;  
 	 	 	 l a b e l . P r e f e r r e d W i d t h   =   2 0 ;  
 	 	 	 l a b e l . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   0 ,   5 ,   5 ) ;  
 	 	 	 l a b e l . D o c k   =   D o c k S t y l e . B o t t o m ;  
 	 	 }  
  
 	 	 p r o t e c t e d   M y W i d g e t s . P a n e l   C r e a t e S w i t c h H o r i z o n a l L a b e l s ( M y W i d g e t s . P a n e l   p a r e n t ,   s t r i n g   n o ,   s t r i n g   y e s )  
 	 	 {  
 	 	 	 M y W i d g e t s . P a n e l   l a b e l s   =   n e w   M y W i d g e t s . P a n e l ( p a r e n t ) ;  
 	 	 	 l a b e l s . P r e f e r r e d H e i g h t   =   2 0 ;  
 	 	 	 l a b e l s . P r e f e r r e d W i d t h   =   p a r e n t . P r e f e r r e d W i d t h ;  
  
 	 	 	 S t a t i c T e x t   l a b e l ;  
  
 	 	 	 l a b e l   =   n e w   S t a t i c T e x t ( l a b e l s ) ;  
 	 	 	 l a b e l . T e x t   =   M i s c . F o n t S i z e ( n o ,   8 0 ) ;  
 	 	 	 l a b e l . C o n t e n t A l i g n m e n t   =   C o n t e n t A l i g n m e n t . M i d d l e L e f t ;  
 	 	 	 l a b e l . P r e f e r r e d H e i g h t   =   2 0 ;  
 	 	 	 l a b e l . P r e f e r r e d W i d t h   =   p a r e n t . P r e f e r r e d W i d t h / 2 ;  
 	 	 	 l a b e l . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   0 ,   0 ,   0 ) ;  
 	 	 	 l a b e l . D o c k   =   D o c k S t y l e . L e f t ;  
  
 	 	 	 l a b e l   =   n e w   S t a t i c T e x t ( l a b e l s ) ;  
 	 	 	 l a b e l . T e x t   =   M i s c . F o n t S i z e ( y e s ,   8 0 ) ;  
 	 	 	 l a b e l . C o n t e n t A l i g n m e n t   =   C o n t e n t A l i g n m e n t . M i d d l e R i g h t ;  
 	 	 	 l a b e l . P r e f e r r e d H e i g h t   =   2 0 ;  
 	 	 	 l a b e l . P r e f e r r e d W i d t h   =   p a r e n t . P r e f e r r e d W i d t h / 2 ;  
 	 	 	 l a b e l . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   0 ,   0 ,   0 ) ;  
 	 	 	 l a b e l . D o c k   =   D o c k S t y l e . R i g h t ;  
  
 	 	 	 r e t u r n   l a b e l s ;  
 	 	 }  
  
  
 	 	 p r o t e c t e d   v o i d   C r e a t e H e l p ( M y W i d g e t s . P a n e l   p a r e n t )  
 	 	 {  
 	 	 	 / / 	 C r é e   l e   p a n n e a u   p o u r   l ' a i d e .  
 	 	 	 t h i s . b o o k   =   n e w   T a b B o o k ( p a r e n t ) ;  
 	 	 	 t h i s . b o o k . A r r o w s   =   T a b B o o k A r r o w s . S t r e t c h ;  
 	 	 	 t h i s . b o o k . D o c k   =   D o c k S t y l e . F i l l ;  
  
 	 	 	 / / 	 C r é e   l ' o n g l e t   p o u r   l e s   c o m m e n t a i r e s   s u r   l e   p r o g r a m m e .  
 	 	 	 t h i s . p a g e P r o g r a m   =   n e w   T a b P a g e ( ) ;  
 	 	 	 t h i s . p a g e P r o g r a m . T a b T i t l e   =   R e s . S t r i n g s . T a b P a g e . R i g h t . P r o g r a m . B u t t o n ;  
  
 	 	 	 H T o o l B a r   t o o l b a r   =   n e w   H T o o l B a r ( t h i s . p a g e P r o g r a m ) ;  
 	 	 	 t o o l b a r . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   0 ,   0 ,   - 1 ) ;  
 	 	 	 t o o l b a r . D o c k   =   D o c k S t y l e . T o p ;  
  
 	 	 	 I c o n B u t t o n   b u t t o n ;  
  
 	 	 	 b u t t o n   =   n e w   I c o n B u t t o n ( ) ;  
 	 	 	 b u t t o n . N a m e   =   " F o n t B o l d " ;  
 	 	 	 b u t t o n . I c o n N a m e   =   M i s c . I c o n ( " F o n t B o l d " ) ;  
 	 	 	 b u t t o n . B u t t o n S t y l e   =   B u t t o n S t y l e . A c t i v a b l e I c o n ;  
 	 	 	 b u t t o n . C l i c k e d   + =   t h i s . H a n d l e B u t t o n S t y l e C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( b u t t o n ,   R e s . S t r i n g s . C o m m a n d . B o l d . T o o l t i p ) ;  
 	 	 	 t o o l b a r . I t e m s . A d d ( b u t t o n ) ;  
  
 	 	 	 b u t t o n   =   n e w   I c o n B u t t o n ( ) ;  
 	 	 	 b u t t o n . N a m e   =   " F o n t I t a l i c " ;  
 	 	 	 b u t t o n . I c o n N a m e   =   M i s c . I c o n ( " F o n t I t a l i c " ) ;  
 	 	 	 b u t t o n . B u t t o n S t y l e   =   B u t t o n S t y l e . A c t i v a b l e I c o n ;  
 	 	 	 b u t t o n . C l i c k e d   + =   t h i s . H a n d l e B u t t o n S t y l e C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( b u t t o n ,   R e s . S t r i n g s . C o m m a n d . I t a l i c . T o o l t i p ) ;  
 	 	 	 t o o l b a r . I t e m s . A d d ( b u t t o n ) ;  
  
 	 	 	 b u t t o n   =   n e w   I c o n B u t t o n ( ) ;  
 	 	 	 b u t t o n . N a m e   =   " F o n t U n d e r l i n e " ;  
 	 	 	 b u t t o n . I c o n N a m e   =   M i s c . I c o n ( " F o n t U n d e r l i n e " ) ;  
 	 	 	 b u t t o n . B u t t o n S t y l e   =   B u t t o n S t y l e . A c t i v a b l e I c o n ;  
 	 	 	 b u t t o n . C l i c k e d   + =   t h i s . H a n d l e B u t t o n S t y l e C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( b u t t o n ,   R e s . S t r i n g s . C o m m a n d . U n d e r l i n e d . T o o l t i p ) ;  
 	 	 	 t o o l b a r . I t e m s . A d d ( b u t t o n ) ;  
  
 	 	 	 t h i s . f i e l d P r o g r a m R e m   =   n e w   T e x t F i e l d M u l t i ( t h i s . p a g e P r o g r a m ) ;  
 	 	 	 t h i s . f i e l d P r o g r a m R e m . T e x t   =   D o l p h i n A p p l i c a t i o n . P r o g r a m E m p t y R e m ;  
 	 	 	 t h i s . f i e l d P r o g r a m R e m . D o c k   =   D o c k S t y l e . F i l l ;  
 	 	 	 t h i s . f i e l d P r o g r a m R e m . T e x t C h a n g e d   + =   n e w   E v e n t H a n d l e r ( t h i s . H a n d l e F i e l d P r o g r a m R e m T e x t C h a n g e d ) ;  
  
 	 	 	 t h i s . b o o k . I t e m s . A d d ( t h i s . p a g e P r o g r a m ) ;  
  
 	 	 	 / / 	 C r é e   l e s   o n g l e t s   d ' a i d e   s u r   l e   p r o c e s s e u r .  
 	 	 	 L i s t < s t r i n g >   c h a p t e r s   =   t h i s . p r o c e s s o r . H e l p C h a p t e r s ;  
 	 	 	 f o r e a c h   ( s t r i n g   c h a p t e r   i n   c h a p t e r s )  
 	 	 	 {  
 	 	 	 	 T a b P a g e   p a g e   =   n e w   T a b P a g e ( ) ;  
 	 	 	 	 p a g e . T a b T i t l e   =   c h a p t e r ;  
  
 	 	 	 	 s t r i n g   t e x t   =   t h i s . p r o c e s s o r . H e l p C h a p t e r ( c h a p t e r ) ;  
  
 	 	 	 	 T e x t F i e l d M u l t i   f i e l d   =   n e w   T e x t F i e l d M u l t i ( p a g e ) ;  
 	 	 	 	 f i e l d . I s R e a d O n l y   =   t r u e ;  
 	 	 	 	 f i e l d . M a x L e n g t h   =   t e x t . L e n g t h + 1 0 ;  
 	 	 	 	 f i e l d . T e x t   =   t e x t ;  
 	 	 	 	 f i e l d . D o c k   =   D o c k S t y l e . F i l l ;  
  
 	 	 	 	 t h i s . b o o k . I t e m s . A d d ( p a g e ) ;  
 	 	 	 }  
  
 	 	 	 t h i s . b o o k . A c t i v e P a g e   =   t h i s . p a g e P r o g r a m ;  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   C r e a t e K e y b o a r d D i s p l a y ( M y W i d g e t s . P a n e l   p a r e n t )  
 	 	 {  
 	 	 	 / / 	 C r é e   l e   c l a v i e r   e t   l ' a f f i c h a g e   s i m u l é ,   d a n s   l a   p a r t i e   d e   d r o i t e .  
 	 	 	 L i s t < M y W i d g e t s . P a n e l >   l i n e s   =   n e w   L i s t < M y W i d g e t s . P a n e l > ( ) ;  
 	 	 	 f o r   ( i n t   y = 0 ;   y < 2 ;   y + + )  
 	 	 	 {  
 	 	 	 	 M y W i d g e t s . P a n e l   k e y b o a r d   =   n e w   M y W i d g e t s . P a n e l ( p a r e n t ) ;  
 	 	 	 	 k e y b o a r d . P r e f e r r e d H e i g h t   =   5 0 ;  
 	 	 	 	 k e y b o a r d . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   0 ,   0 ,   ( y = = 0 )   ?   1 0 : 2 ) ;  
 	 	 	 	 k e y b o a r d . D o c k   =   D o c k S t y l e . B o t t o m ;  
  
 	 	 	 	 l i n e s . A d d ( k e y b o a r d ) ;  
 	 	 	 }  
  
 	 	 	 M y W i d g e t s . P a n e l   d i s p l a y   =   n e w   M y W i d g e t s . P a n e l ( p a r e n t ) ;  
 	 	 	 d i s p l a y . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   0 ,   5 ,   1 0 ) ;  
 	 	 	 d i s p l a y . D o c k   =   D o c k S t y l e . B o t t o m ;  
  
 	 	 	 / / 	 C r é e   l e s   d i g i t s   d e   l ' a f f i c h a g e .  
 	 	 	 t h i s . d i s p l a y D i g i t s   =   n e w   L i s t < M y W i d g e t s . D i g i t > ( ) ;  
 	 	 	 f o r   ( i n t   i = 0 ;   i < 4 ;   i + + )  
 	 	 	 {  
 	 	 	 	 M y W i d g e t s . D i g i t   d i g i t   =   n e w   M y W i d g e t s . D i g i t ( d i s p l a y ) ;  
 	 	 	 	 d i g i t . D o c k   =   D o c k S t y l e . L e f t ;  
  
 	 	 	 	 t h i s . d i s p l a y D i g i t s . A d d ( d i g i t ) ;  
 	 	 	 }  
  
 	 	 	 M y W i d g e t s . P a n e l   k e y   =   n e w   M y W i d g e t s . P a n e l ( d i s p l a y ) ;  
 	 	 	 k e y . P r e f e r r e d W i d t h   =   3 4 ;  
 	 	 	 k e y . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   1 2 ,   0 ,   0 ) ;  
 	 	 	 k e y . D o c k   =   D o c k S t y l e . R i g h t ;  
  
 	 	 	 t h i s . d i s p l a y B u t t o n K e y   =   n e w   M y W i d g e t s . P u s h B u t t o n ( k e y ) ;  
 	 	 	 t h i s . d i s p l a y B u t t o n K e y . P r e f e r r e d S i z e   =   n e w   S i z e ( 3 4 ,   2 4 ) ;  
 	 	 	 t h i s . d i s p l a y B u t t o n K e y . D o c k   =   D o c k S t y l e . B o t t o m ;  
 	 	 	 t h i s . d i s p l a y B u t t o n K e y . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   0 ,   2 ,   0 ) ;  
 	 	 	 t h i s . d i s p l a y B u t t o n K e y . C l i c k e d   + =   t h i s . H a n d l e D i s p l a y B u t t o n K e y C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . d i s p l a y B u t t o n K e y ,   R e s . S t r i n g s . C o m m a n d . K e y b o a r d . T o o l t i p ) ;  
  
 	 	 	 M y W i d g e t s . P a n e l   m o d e   =   n e w   M y W i d g e t s . P a n e l ( d i s p l a y ) ;  
 	 	 	 m o d e . P r e f e r r e d W i d t h   =   5 4 ;  
 	 	 	 m o d e . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   6 ,   0 ,   0 ) ;  
 	 	 	 m o d e . D o c k   =   D o c k S t y l e . R i g h t ;  
  
 	 	 	 t h i s . d i s p l a y B u t t o n M o d e   =   n e w   M y W i d g e t s . P u s h B u t t o n ( m o d e ) ;  
 	 	 	 t h i s . d i s p l a y B u t t o n M o d e . T e x t   =   R e s . S t r i n g s . C o m m a n d . D i s p l a y . A c t i v e . B u t t o n ;  
 	 	 	 t h i s . d i s p l a y B u t t o n M o d e . P r e f e r r e d S i z e   =   n e w   S i z e ( 5 4 ,   2 4 ) ;  
 	 	 	 t h i s . d i s p l a y B u t t o n M o d e . D o c k   =   D o c k S t y l e . B o t t o m ;  
 	 	 	 t h i s . d i s p l a y B u t t o n M o d e . C l i c k e d   + =   t h i s . H a n d l e D i s p l a y B u t t o n M o d e C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . d i s p l a y B u t t o n M o d e ,   R e s . S t r i n g s . C o m m a n d . D i s p l a y . A c t i v e . T o o l t i p ) ;  
  
 	 	 	 M y W i d g e t s . P a n e l   t e c h n o   =   n e w   M y W i d g e t s . P a n e l ( d i s p l a y ) ;  
 	 	 	 t e c h n o . P r e f e r r e d W i d t h   =   3 4 ;  
 	 	 	 t e c h n o . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   1 ,   0 ,   0 ) ;  
 	 	 	 t e c h n o . D o c k   =   D o c k S t y l e . R i g h t ;  
  
 	 	 	 t h i s . d i s p l a y B u t t o n T e c h n o   =   n e w   M y W i d g e t s . P u s h B u t t o n ( t e c h n o ) ;  
 	 	 	 t h i s . d i s p l a y B u t t o n T e c h n o . P r e f e r r e d S i z e   =   n e w   S i z e ( 3 4 ,   2 4 ) ;  
 	 	 	 t h i s . d i s p l a y B u t t o n T e c h n o . D o c k   =   D o c k S t y l e . B o t t o m ;  
 	 	 	 t h i s . d i s p l a y B u t t o n T e c h n o . C l i c k e d   + =   t h i s . H a n d l e D i s p l a y B u t t o n T e c h n o C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . d i s p l a y B u t t o n T e c h n o ,   R e s . S t r i n g s . C o m m a n d . D i s p l a y . T e c h n o . T o o l t i p ) ;  
  
 	 	 	 M y W i d g e t s . P a n e l   c l s   =   n e w   M y W i d g e t s . P a n e l ( d i s p l a y ) ;  
 	 	 	 c l s . P r e f e r r e d W i d t h   =   3 4 ;  
 	 	 	 c l s . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   1 ,   0 ,   0 ) ;  
 	 	 	 c l s . D o c k   =   D o c k S t y l e . R i g h t ;  
  
 	 	 	 t h i s . d i s p l a y B u t t o n C l s   =   n e w   M y W i d g e t s . P u s h B u t t o n ( c l s ) ;  
 	 	 	 t h i s . d i s p l a y B u t t o n C l s . T e x t   =   R e s . S t r i n g s . C o m m a n d . D i s p l a y . C l e a r . B u t t o n ;  
 	 	 	 t h i s . d i s p l a y B u t t o n C l s . P r e f e r r e d S i z e   =   n e w   S i z e ( 3 4 ,   2 4 ) ;  
 	 	 	 t h i s . d i s p l a y B u t t o n C l s . D o c k   =   D o c k S t y l e . B o t t o m ;  
 	 	 	 t h i s . d i s p l a y B u t t o n C l s . C l i c k e d   + =   t h i s . H a n d l e D i s p l a y B u t t o n C l s C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . d i s p l a y B u t t o n C l s ,   R e s . S t r i n g s . C o m m a n d . D i s p l a y . C l e a r . T o o l t i p ) ;  
  
 	 	 	 / / 	 C r é e   l ' a f f i c h a g e   b i t m a p .  
 	 	 	 M y W i d g e t s . P a n e l   b i t m a p   =   n e w   M y W i d g e t s . P a n e l ( p a r e n t ) ;  
 	 	 	 b i t m a p . P r e f e r r e d H e i g h t   =   0 ;  
 	 	 	 b i t m a p . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   1 2 ,   0 ,   0 ) ;  
 	 	 	 b i t m a p . D o c k   =   D o c k S t y l e . B o t t o m ;  
  
 	 	 	 t h i s . d i s p l a y B i t m a p   =   n e w   M y W i d g e t s . D i s p l a y ( b i t m a p ) ;  
 	 	 	 t h i s . d i s p l a y B i t m a p . S e t M e m o r y ( t h i s . m e m o r y ,   C o m p o n e n t s . M e m o r y . D i s p l a y B a s e ,   C o m p o n e n t s . M e m o r y . D i s p l a y D x ,   C o m p o n e n t s . M e m o r y . D i s p l a y D y ) ;  
 	 	 	 t h i s . d i s p l a y B i t m a p . P r e f e r r e d S i z e   =   n e w   S i z e ( 2 5 8 ,   2 0 2 ) ;  
 	 	 	 t h i s . d i s p l a y B i t m a p . D o c k   =   D o c k S t y l e . B o t t o m ;  
  
 	 	 	 / / 	 C r é e   l e s   t o u c h e s   d u   c l a v i e r .  
 	 	 	 t h i s . k e y b o a r d B u t t o n s   =   n e w   L i s t < M y W i d g e t s . P u s h B u t t o n > ( ) ;  
 	 	 	 M y W i d g e t s . P u s h B u t t o n   b u t t o n ;  
 	 	 	 i n t   t = 0 ;  
 	 	 	 f o r   ( i n t   y = 0 ;   y < 2 ;   y + + )  
 	 	 	 {  
 	 	 	 	 f o r   ( i n t   x = 0 ;   x < 5 ;   x + + )  
 	 	 	 	 {  
 	 	 	 	 	 i n t   i n d e x   =   D o l p h i n A p p l i c a t i o n . K e y b o a r d I n d e x [ t + + ] ;  
 	 	 	 	 	 b u t t o n   =   t h i s . C r e a t e K e y b o a r d B u t t o n ( i n d e x ,   l i n e s [ y ] ) ;  
 	 	 	 	 	 t h i s . k e y b o a r d B u t t o n s . A d d ( b u t t o n ) ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 b u t t o n   =   t h i s . C r e a t e K e y b o a r d B u t t o n ( 1 0 0 ,   l i n e s [ 0 ] ) ;  
 	 	 	 b u t t o n . M a r g i n s   =   n e w   M a r g i n s ( ( 5 0 + 2 ) * 1 ,   2 ,   0 ,   0 ) ;  
 	 	 	 t h i s . k e y b o a r d B u t t o n s . A d d ( b u t t o n ) ;  
  
 	 	 	 b u t t o n   =   t h i s . C r e a t e K e y b o a r d B u t t o n ( 1 0 1 ,   l i n e s [ 0 ] ) ;  
 	 	 	 t h i s . k e y b o a r d B u t t o n s . A d d ( b u t t o n ) ;  
  
 	 	 	 b u t t o n   =   t h i s . C r e a t e K e y b o a r d B u t t o n ( 1 0 2 ,   l i n e s [ 0 ] ) ;  
 	 	 	 t h i s . k e y b o a r d B u t t o n s . A d d ( b u t t o n ) ;  
  
 	 	 	 b u t t o n   =   t h i s . C r e a t e K e y b o a r d B u t t o n ( 1 0 3 ,   l i n e s [ 1 ] ) ;  
 	 	 	 b u t t o n . M a r g i n s   =   n e w   M a r g i n s ( ( 5 0 + 2 ) * 2 ,   2 ,   0 ,   0 ) ;  
 	 	 	 t h i s . k e y b o a r d B u t t o n s . A d d ( b u t t o n ) ;  
  
 	 	 	 t h i s . U p d a t e D i s p l a y M o d e ( ) ;  
 	 	 	 t h i s . U p d a t e K e y b o a r d ( ) ;  
 	 	 }  
  
 	 	 p r o t e c t e d   M y W i d g e t s . P u s h B u t t o n   C r e a t e K e y b o a r d B u t t o n ( i n t   i n d e x ,   M y W i d g e t s . P a n e l   p a r e n t )  
 	 	 {  
 	 	 	 M y W i d g e t s . P u s h B u t t o n   b u t t o n   =   n e w   M y W i d g e t s . P u s h B u t t o n ( p a r e n t ) ;  
 	 	 	 b u t t o n . I n d e x   =   i n d e x ;  
 	 	 	 b u t t o n . P r e f e r r e d W i d t h   =   5 0 ;  
 	 	 	 b u t t o n . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   2 ,   0 ,   0 ) ;  
 	 	 	 b u t t o n . D o c k   =   D o c k S t y l e . L e f t ;  
 	 	 	 b u t t o n . P r e s s e d   + =   t h i s . H a n d l e K e y b o a r d B u t t o n P r e s s e d ;  
 	 	 	 b u t t o n . R e l e a s e d   + =   t h i s . H a n d l e K e y b o a r d B u t t o n R e l e a s e d ;  
  
 	 	 	 i f   ( i n d e x   = =   0 x 0 8 )  
 	 	 	 {  
 	 	 	 	 b u t t o n . T e x t   =   " < b > S h i f t < / b > " ;  
 	 	 	 }  
 	 	 	 e l s e   i f   ( i n d e x   = =   0 x 1 0 )  
 	 	 	 {  
 	 	 	 	 b u t t o n . T e x t   =   " < b > C t r l < / b > " ;  
 	 	 	 }  
 	 	 	 e l s e   i f   ( i n d e x   = =   1 0 0 )  
 	 	 	 {  
 	 	 	 	 b u t t o n . T e x t   =   M i s c . I m a g e ( " A r r o w L e f t " ) ;  
 	 	 	 }  
 	 	 	 e l s e   i f   ( i n d e x   = =   1 0 1 )  
 	 	 	 {  
 	 	 	 	 b u t t o n . T e x t   =   M i s c . I m a g e ( " A r r o w D o w n " ) ;  
 	 	 	 }  
 	 	 	 e l s e   i f   ( i n d e x   = =   1 0 2 )  
 	 	 	 {  
 	 	 	 	 b u t t o n . T e x t   =   M i s c . I m a g e ( " A r r o w R i g h t " ) ;  
 	 	 	 }  
 	 	 	 e l s e   i f   ( i n d e x   = =   1 0 3 )  
 	 	 	 {  
 	 	 	 	 b u t t o n . T e x t   =   M i s c . I m a g e ( " A r r o w U p " ) ;  
 	 	 	 }  
  
 	 	 	 r e t u r n   b u t t o n ;  
 	 	 }  
  
 	 	 p r o t e c t e d   s t a t i c   i n t [ ]   K e y b o a r d I n d e x   =  
 	 	 {  
 	 	 	 0 x 0 8 ,   0 x 0 0 ,   0 x 0 1 ,   0 x 0 2 ,   0 x 0 3 ,     / /   S h i f t ,   0 . . 3  
 	 	 	 0 x 1 0 ,   0 x 0 4 ,   0 x 0 5 ,   0 x 0 6 ,   0 x 0 7 ,     / /   C t r l ,     4 . . 7  
 	 	 } ;  
  
 	 	 p r o t e c t e d   M y W i d g e t s . P u s h B u t t o n   S e a r c h K e y ( i n t   i n d e x )  
 	 	 {  
 	 	 	 / / 	 C h e r c h e   u n e   t o u c h e   d u   c l a v i e r   é m u l é .  
 	 	 	 f o r e a c h   ( M y W i d g e t s . P u s h B u t t o n   b u t t o n   i n   t h i s . k e y b o a r d B u t t o n s )  
 	 	 	 {  
 	 	 	 	 i f   ( b u t t o n . I n d e x   = =   i n d e x )  
 	 	 	 	 {  
 	 	 	 	 	 r e t u r n   b u t t o n ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 r e t u r n   n u l l ;  
 	 	 }  
  
 	 	 p u b l i c   b o o l   I s R u n n i n g  
 	 	 {  
 	 	 	 / / 	 R e t o u r n e   t r u e   s i   u n   p r o g r a m m e   e s t   e n   c o u r s   d ' e x é c u t i o n .  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 r e t u r n   t h i s . b u t t o n R u n . A c t i v e S t a t e   = =   A c t i v e S t a t e . Y e s ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p u b l i c   v o i d   P r o c e s s R u n n i n g M e s s a g e ( M e s s a g e   m e s s a g e )  
 	 	 {  
 	 	 	 / / 	 G è r e   l e s   t o u c h e s   p e n d a n t   q u ' u n   p r o g r a m m e   e s t   e n   c o u r s   d ' e x é c u t i o n .  
 	 	 	 i f   ( m e s s a g e . M e s s a g e T y p e   = =   M e s s a g e T y p e . K e y D o w n )  
 	 	 	 {  
 	 	 	 	 i f   ( m e s s a g e . K e y C o d e   = =   K e y C o d e . A r r o w U p )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . R u n n i n g K e y ( 0 x 1 0 ,   t r u e ) ;     / /   c t r l   p r e s s e d  
 	 	 	 	 	 t h i s . R u n n i n g K e y ( 1 0 3 ,   t r u e ) ;     / /   u p   p r e s s e d  
 	 	 	 	 }  
  
 	 	 	 	 i f   ( m e s s a g e . K e y C o d e   = =   K e y C o d e . A r r o w D o w n )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . R u n n i n g K e y ( 0 x 0 8 ,   t r u e ) ;     / /   s h i f t   p r e s s e d  
 	 	 	 	 	 t h i s . R u n n i n g K e y ( 1 0 1 ,   t r u e ) ;     / /   d o w n   p r e s s e d  
 	 	 	 	 }  
  
 	 	 	 	 i f   ( m e s s a g e . K e y C o d e   = =   K e y C o d e . A r r o w L e f t )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . R u n n i n g K e y ( 1 0 0 ,   t r u e ) ;     / /   l e f t   p r e s s e d  
 	 	 	 	 }  
  
 	 	 	 	 i f   ( m e s s a g e . K e y C o d e   = =   K e y C o d e . A r r o w R i g h t )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . R u n n i n g K e y ( 1 0 2 ,   t r u e ) ;     / /   r i g h t   p r e s s e d  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 i f   ( m e s s a g e . M e s s a g e T y p e   = =   M e s s a g e T y p e . K e y U p )  
 	 	 	 {  
 	 	 	 	 i f   ( m e s s a g e . K e y C o d e   = =   K e y C o d e . A r r o w U p )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . R u n n i n g K e y ( 0 x 1 0 ,   f a l s e ) ;     / /   c t r l   r e l e a s e d  
 	 	 	 	 	 t h i s . R u n n i n g K e y ( 1 0 3 ,   f a l s e ) ;     / /   u p   r e l e a s e d  
 	 	 	 	 }  
  
 	 	 	 	 i f   ( m e s s a g e . K e y C o d e   = =   K e y C o d e . A r r o w D o w n )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . R u n n i n g K e y ( 0 x 0 8 ,   f a l s e ) ;     / /   s h i f t   r e l e a s e d  
 	 	 	 	 	 t h i s . R u n n i n g K e y ( 1 0 1 ,   f a l s e ) ;     / /   d o w n   r e l e a s e d  
 	 	 	 	 }  
  
 	 	 	 	 i f   ( m e s s a g e . K e y C o d e   = =   K e y C o d e . A r r o w L e f t )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . R u n n i n g K e y ( 1 0 0 ,   f a l s e ) ;     / /   l e f t   r e l e a s e d  
 	 	 	 	 }  
  
 	 	 	 	 i f   ( m e s s a g e . K e y C o d e   = =   K e y C o d e . A r r o w R i g h t )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . R u n n i n g K e y ( 1 0 2 ,   f a l s e ) ;     / /   r i g h t   r e l e a s e d  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   R u n n i n g K e y ( i n t   i n d e x ,   b o o l   p r e s s e d )  
 	 	 {  
 	 	 	 / / 	 G è r e   u n e   t o u c h e   p e n d a n t   q u ' u n   p r o g r a m m e   e s t   e n   c o u r s   d ' e x é c u t i o n .  
 	 	 	 M y W i d g e t s . P u s h B u t t o n   b u t t o n   =   t h i s . S e a r c h K e y ( i n d e x ) ;  
 	 	 	 A c t i v e S t a t e   s t a t e   =   p r e s s e d   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
  
 	 	 	 i f   ( b u t t o n . A c t i v e S t a t e   ! =   s t a t e )  
 	 	 	 {  
 	 	 	 	 b u t t o n . A c t i v e S t a t e   =   s t a t e ;  
 	 	 	 	 t h i s . U p d a t e K e y b o a r d ( ) ;  
 	 	 	 	 t h i s . K e y b o a r d C h a n g e d ( b u t t o n ,   p r e s s e d ) ;  
 	 	 	 }  
 	 	 }  
  
  
 	 	 p r o t e c t e d   v o i d   U p d a t e B u t t o n s ( )  
 	 	 {  
 	 	 	 / / 	 M e t   à   j o u r   l e   m o d e   e n a b l e / d i s a b l e   d e   t o u s   l e s   b o u t o n s .  
 	 	 	 t h i s . b u t t o n S t e p . E n a b l e   =   ( t h i s . b u t t o n R u n . A c t i v e S t a t e   = =   A c t i v e S t a t e . Y e s )   & &   ( t h i s . s w i t c h S t e p . A c t i v e S t a t e   = =   A c t i v e S t a t e . Y e s ) ;  
  
 	 	 	 b o o l   r u n   =   ( t h i s . b u t t o n R u n . A c t i v e S t a t e   = =   A c t i v e S t a t e . Y e s ) ;  
  
 	 	 	 i f   ( r u n )  
 	 	 	 {  
 	 	 	 	 t h i s . b u t t o n R u n . T e x t   =   M i s c . B o l d ( M i s c . F o n t S i z e ( R e s . S t r i n g s . C o m m a n d . S t o p . B u t t o n ,   1 4 0 ) ) ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . b u t t o n R u n . T e x t   =   M i s c . B o l d ( M i s c . F o n t S i z e ( R e s . S t r i n g s . C o m m a n d . R u n . B u t t o n ,   1 4 0 ) ) ;  
 	 	 	 }  
  
 	 	 	 t h i s . b u t t o n M e m o r y R e a d . E n a b l e   =   ! r u n ;  
 	 	 	 t h i s . b u t t o n M e m o r y W r i t e . E n a b l e   =   ! r u n ;  
  
 	 	 	 f o r e a c h   ( M y W i d g e t s . S w i t c h   b u t t o n   i n   t h i s . a d d r e s s S w i t c h s )  
 	 	 	 {  
 	 	 	 	 b u t t o n . E n a b l e   =   ! r u n ;  
 	 	 	 }  
  
 	 	 	 f o r e a c h   ( M y W i d g e t s . S w i t c h   b u t t o n   i n   t h i s . d a t a S w i t c h s )  
 	 	 	 {  
 	 	 	 	 b u t t o n . E n a b l e   =   ! r u n ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   U p d a t e C l o c k B u t t o n s ( )  
 	 	 {  
 	 	 	 / / 	 M e t   à   j o u r   l e s   b o u t o n s   p o u r   l a   f r é q u e n c e   d e   l ' h o r l o g e .  
 	 	 	 t h i s . b u t t o n C l o c k 6 . A c t i v e S t a t e   =   ( t h i s . i p s   = =   1 0 0 0 0 0 0 )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 t h i s . b u t t o n C l o c k 5 . A c t i v e S t a t e   =   ( t h i s . i p s   = =     1 0 0 0 0 0 )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 t h i s . b u t t o n C l o c k 4 . A c t i v e S t a t e   =   ( t h i s . i p s   = =       1 0 0 0 0 )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 t h i s . b u t t o n C l o c k 3 . A c t i v e S t a t e   =   ( t h i s . i p s   = =         1 0 0 0 )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 t h i s . b u t t o n C l o c k 2 . A c t i v e S t a t e   =   ( t h i s . i p s   = =           1 0 0 )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 t h i s . b u t t o n C l o c k 1 . A c t i v e S t a t e   =   ( t h i s . i p s   = =             1 0 )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 t h i s . b u t t o n C l o c k 0 . A c t i v e S t a t e   =   ( t h i s . i p s   = =               1 )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   U p d a t e S a v e ( )  
 	 	 {  
 	 	 	 / / 	 M e t   à   j o u r   l e   b o u t o n   d ' e n r e g i s t r e m e n t .  
 	 	 	 t h i s . b u t t o n S a v e . E n a b l e   =   t h i s . d i r t y ;  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   U p d a t e F i l e n a m e ( )  
 	 	 {  
 	 	 	 / / 	 M e t   à   j o u r   l e   n o m   d u   p r o g r a m m e   o u v e r t .  
 	 	 	 i f   ( s t r i n g . I s N u l l O r E m p t y ( t h i s . f i l e n a m e ) )  
 	 	 	 {  
 	 	 	 	 t h i s . W i n d o w . T e x t   =   T e x t L a y o u t . C o n v e r t T o S i m p l e T e x t ( R e s . S t r i n g s . W i n d o w . T i t l e ) ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . W i n d o w . T e x t   =   s t r i n g . C o n c a t ( T e x t L a y o u t . C o n v e r t T o S i m p l e T e x t ( R e s . S t r i n g s . W i n d o w . T i t l e ) ,   "   -   " ,   S y s t e m . I O . P a t h . G e t F i l e N a m e W i t h o u t E x t e n s i o n ( t h i s . f i l e n a m e ) ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   U p d a t e M e m o r y B a n k ( )  
 	 	 {  
 	 	 	 / / 	 M e t   à   j o u r   l a   b a n q u e   m é m o i r e   u t i l i s é e .  
 	 	 	 t h i s . m e m o r y B u t t o n P C . A c t i v e S t a t e   =   ( t h i s . m e m o r y A c c e s s o r . F o l l o w P C )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 t h i s . m e m o r y B u t t o n M . A c t i v e S t a t e   =   ( t h i s . m e m o r y A c c e s s o r . B a n k   = =   " M " )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 t h i s . m e m o r y B u t t o n R . A c t i v e S t a t e   =   ( t h i s . m e m o r y A c c e s s o r . B a n k   = =   " R " )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 t h i s . m e m o r y B u t t o n P . A c t i v e S t a t e   =   ( t h i s . m e m o r y A c c e s s o r . B a n k   = =   " P " )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 t h i s . m e m o r y B u t t o n D . A c t i v e S t a t e   =   ( t h i s . m e m o r y A c c e s s o r . B a n k   = =   " D " )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
  
 	 	 	 t h i s . c o d e B u t t o n P C . A c t i v e S t a t e   =   ( t h i s . c o d e A c c e s s o r . F o l l o w P C )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 t h i s . c o d e B u t t o n M . A c t i v e S t a t e   =   ( t h i s . c o d e A c c e s s o r . B a n k   = =   " M " )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 t h i s . c o d e B u t t o n R . A c t i v e S t a t e   =   ( t h i s . c o d e A c c e s s o r . B a n k   = =   " R " )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   U p d a t e P a n e l M o d e ( )  
 	 	 {  
 	 	 	 / / 	 M e t   à   j o u r   l e   p a n n e a u   c h o i s i .  
 	 	 	 t h i s . b u t t o n M o d e B u s . A c t i v e S t a t e         =   ( t h i s . p a n e l M o d e   = =   " B u s "       )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 t h i s . b u t t o n M o d e D e t a i l . A c t i v e S t a t e   =   ( t h i s . p a n e l M o d e   = =   " D e t a i l " )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 t h i s . b u t t o n M o d e C o d e . A c t i v e S t a t e       =   ( t h i s . p a n e l M o d e   = =   " C o d e "     )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 t h i s . b u t t o n M o d e C a l m . A c t i v e S t a t e       =   ( t h i s . p a n e l M o d e   = =   " C a l m "     )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 t h i s . b u t t o n M o d e Q u i c k . A c t i v e S t a t e     =   ( t h i s . p a n e l M o d e   = =   " Q u i c k "   )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
  
 	 	 	 t h i s . l e f t P a n e l B u s . V i s i b i l i t y         =   ( t h i s . p a n e l M o d e   = =   " B u s " ) ;  
 	 	 	 t h i s . c l o c k B u s P a n e l . V i s i b i l i t y       =   ( t h i s . p a n e l M o d e   = =   " B u s " ) ;  
 	 	 	 t h i s . l e f t P a n e l D e t a i l . V i s i b i l i t y   =   ( t h i s . p a n e l M o d e   = =   " D e t a i l " ) ;  
 	 	 	 t h i s . l e f t P a n e l C o d e . V i s i b i l i t y       =   ( t h i s . p a n e l M o d e   = =   " C o d e " ) ;  
 	 	 	 t h i s . l e f t P a n e l C a l m . V i s i b i l i t y       =   ( t h i s . p a n e l M o d e   = =   " C a l m " ) ;  
 	 	 	 t h i s . l e f t P a n e l Q u i c k . V i s i b i l i t y     =   ( t h i s . p a n e l M o d e   = =   " Q u i c k " ) ;  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   U p d a t e D i s p l a y M o d e ( )  
 	 	 {  
 	 	 	 / / 	 M e t   à   j o u r   e n   f o n c t i o n   d u   m o d e   [ D I S P L A Y ] .  
 	 	 	 b o o l   b i t m a p   =   ( t h i s . d i s p l a y B u t t o n M o d e . A c t i v e S t a t e   = =   A c t i v e S t a t e . Y e s ) ;  
 	 	 	 b o o l   l c d   =   ( t h i s . d i s p l a y B i t m a p . T e c h n o l o g y   = =   M y W i d g e t s . D i s p l a y . T y p e . L C D ) ;  
  
 	 	 	 f o r e a c h   ( M y W i d g e t s . D i g i t   d i g i t   i n   t h i s . d i s p l a y D i g i t s )  
 	 	 	 {  
 	 	 	 	 d i g i t . P r e f e r r e d S i z e   =   b i t m a p   ?   n e w   S i z e ( 2 0 ,   3 0 )   :   n e w   S i z e ( 4 0 ,   6 0 ) ;  
 	 	 	 }  
  
 	 	 	 t h i s . d i s p l a y B i t m a p . V i s i b i l i t y   =   b i t m a p ;  
 	 	 	 t h i s . d i s p l a y B i t m a p . T e c h n o l o g y   =   l c d   ?   M y W i d g e t s . D i s p l a y . T y p e . L C D   :   M y W i d g e t s . D i s p l a y . T y p e . C R T ;  
  
 	 	 	 t h i s . d i s p l a y B u t t o n C l s . V i s i b i l i t y   =   b i t m a p ;  
 	 	 	 t h i s . d i s p l a y B u t t o n T e c h n o . V i s i b i l i t y   =   b i t m a p ;  
 	 	 	 t h i s . d i s p l a y B u t t o n T e c h n o . T e x t   =   l c d   ?   R e s . S t r i n g s . C o m m a n d . D i s p l a y . T e c h n o . C R T . B u t t o n   :   R e s . S t r i n g s . C o m m a n d . D i s p l a y . T e c h n o . L C D . B u t t o n ;  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   U p d a t e C a l m B u t t o n s ( )  
 	 	 {  
 	 	 	 / / 	 M e t   à   j o u r   l e s   b o u t o n s   d e   l ' é d i t e u r   C A L M .  
 	 	 	 t h i s . c a l m B u t t o n S h o w . A c t i v e S t a t e   =   t h i s . c a l m E d i t o r . T e x t L a y o u t . S h o w T a b   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   U p d a t e C a l m E d i t o r ( )  
 	 	 {  
 	 	 	 / / 	 M e t   à   j o u r   l ' é d i t e u r   C A L M   a p r è s   u n   c h a n g e m e n t   d e   l a   t a i l l e   d e   l a   p o l i c e .  
 	 	 	 b o o l   b i g   =   ( t h i s . c a l m B u t t o n B i g . A c t i v e S t a t e   = =   A c t i v e S t a t e . Y e s ) ;  
  
 	 	 	 t h i s . c a l m E d i t o r . T e x t L a y o u t . D e f a u l t F o n t S i z e   =   b i g   ?   1 3 . 0   :   1 0 . 8 ;  
  
 	 	 	 / / 	 S u p p r i m e   t o u t e s   l e s   t a b u l a t i o n s .  
 	 	 	 w h i l e   ( t h i s . c a l m E d i t o r . T e x t L a y o u t . T a b C o u n t   >   0 )  
 	 	 	 {  
 	 	 	 	 t h i s . c a l m E d i t o r . T e x t L a y o u t . T a b R e m o v e A t ( 0 ) ;  
 	 	 	 }  
  
 	 	 	 / / 	 I n s è r e   d e   n o u v e l l e s   t a b u l a t i o n s ,   p r o p o r t i o n n e l l e s   à   l a   t a i l l e   d e   l a   p o l i c e .  
 	 	 	 f o r   ( i n t   i = 0 ;   i < 8 ;   i + + )  
 	 	 	 {  
 	 	 	 	 T e x t S t y l e . T a b   t a b   =   n e w   T e x t S t y l e . T a b ( ( b i g   ?   5 0 . 0 * 1 3 . 0 / 1 0 . 8   :   5 0 . 0 ) * i ,   T e x t T a b T y p e . L e f t ,   T e x t T a b L i n e . N o n e ) ;  
 	 	 	 	 t h i s . c a l m E d i t o r . T e x t L a y o u t . T a b I n s e r t ( t a b ) ;  
 	 	 	 }  
  
 	 	 	 / / 	 T O D O :   p o u r   m e t t r e   à   j o u r   l ' a s c e n s e u r   ( d e v r a i t   ê t r e   i n u t i l e )   !  
 	 	 	 t h i s . i g n o r e C h a n g e   =   t r u e ;  
 	 	 	 t h i s . c a l m E d i t o r . C u r s o r   =   0 ;  
 	 	 	 t h i s . c a l m E d i t o r . T e x t L a y o u t . I n s e r t C h a r a c t e r ( t h i s . c a l m E d i t o r . T e x t N a v i g a t o r . C o n t e x t ,   ' ? ' ) ;  
 	 	 	 t h i s . c a l m E d i t o r . T e x t L a y o u t . D e l e t e C h a r a c t e r ( t h i s . c a l m E d i t o r . T e x t N a v i g a t o r . C o n t e x t ,   - 1 ) ;  
 	 	 	 t h i s . i g n o r e C h a n g e   =   f a l s e ;  
  
 	 	 	 t h i s . c a l m E d i t o r . I n v a l i d a t e ( ) ;     / /   T O D O :   d e v r a i t   ê t r e   i n u t i l e  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   U p d a t e K e y b o a r d ( )  
 	 	 {  
 	 	 	 / / 	 M e t   à   j o u r   l e s   i n s c r i p t i o n s   s u r   l e   c l a v i e r   é m u l é .  
 	 	 	 b o o l   s h i f t   =   t h i s . S e a r c h K e y ( 0 x 0 8 ) . A c t i v e S t a t e   = =   A c t i v e S t a t e . Y e s ;  
 	 	 	 b o o l   c t r l     =   t h i s . S e a r c h K e y ( 0 x 1 0 ) . A c t i v e S t a t e   = =   A c t i v e S t a t e . Y e s ;  
  
 	 	 	 t h i s . d i s p l a y B u t t o n K e y . T e x t   =   t h i s . k e y b o a r d A r r o w s   ?   R e s . S t r i n g s . C o m m a n d . K e y b o a r d . N u m e r i c . B u t t o n   :   R e s . S t r i n g s . C o m m a n d . K e y b o a r d . A r r o w s . B u t t o n ;  
  
 	 	 	 f o r e a c h   ( M y W i d g e t s . P u s h B u t t o n   b u t t o n   i n   t h i s . k e y b o a r d B u t t o n s )  
 	 	 	 {  
 	 	 	 	 i f   ( b u t t o n . I n d e x   > =   0   & &   b u t t o n . I n d e x   < =   7 )  
 	 	 	 	 {  
 	 	 	 	 	 i n t   i   =   b u t t o n . I n d e x ;  
  
 	 	 	 	 	 i f   ( s h i f t )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 i   + =   0 x 0 8 ;  
 	 	 	 	 	 }  
  
 	 	 	 	 	 i f   ( c t r l )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 i   + =   0 x 1 0 ;  
 	 	 	 	 	 }  
  
 	 	 	 	 	 i f   ( i   < =   7 )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 b u t t o n . T e x t   =   M i s c . B o l d ( M i s c . F o n t S i z e ( b u t t o n . I n d e x . T o S t r i n g ( ) ,   2 0 0 ) ) ;  
 	 	 	 	 	 }  
 	 	 	 	 	 e l s e  
 	 	 	 	 	 {  
 	 	 	 	 	 	 b u t t o n . T e x t   =   s t r i n g . C o n c a t ( " ( " ,   i . T o S t r i n g ( " X 2 " ) ,   " ) < b r / > " ,   M i s c . B o l d ( M i s c . F o n t S i z e ( b u t t o n . I n d e x . T o S t r i n g ( ) ,   1 8 0 ) ) ) ;  
 	 	 	 	 	 }  
 	 	 	 	 }  
  
 	 	 	 	 i f   ( b u t t o n . I n d e x   <   1 0 0 )  
 	 	 	 	 {  
 	 	 	 	 	 b u t t o n . V i s i b i l i t y   =   ! t h i s . k e y b o a r d A r r o w s ;  
 	 	 	 	 }  
 	 	 	 	 e l s e  
 	 	 	 	 {  
 	 	 	 	 	 b u t t o n . V i s i b i l i t y   =   t h i s . k e y b o a r d A r r o w s ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   U p d a t e D a t a ( )  
 	 	 {  
 	 	 	 / / 	 M e t   à   j o u r   l e s   d o n n é e s ,   a p r è s   l e   c h a n g e m e n t   d ' u n e   v a l e u r   e n   m é m o i r e .  
 	 	 	 i f   ( t h i s . p a n e l M o d e   = =   " D e t a i l " )  
 	 	 	 {  
 	 	 	 	 t h i s . m e m o r y A c c e s s o r . U p d a t e D a t a ( ) ;  
 	 	 	 }  
  
 	 	 	 i f   ( t h i s . p a n e l M o d e   = =   " C o d e " )  
 	 	 	 {  
 	 	 	 	 t h i s . c o d e A c c e s s o r . U p d a t e D a t a ( ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   M a r k P C ( i n t   p c ,   b o o l   f o r c e )  
 	 	 {  
 	 	 	 / / 	 M o n t r e   l e s   d o n n é e s   p o i n t é e s   p a r   l e   P C .  
 	 	 	 i f   ( t h i s . p a n e l M o d e   = =   " D e t a i l " )  
 	 	 	 {  
 	 	 	 	 i f   ( f o r c e )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . m e m o r y A c c e s s o r . D i r t y M a r k P C ( ) ;  
 	 	 	 	 }  
  
 	 	 	 	 t h i s . m e m o r y A c c e s s o r . M a r k P C   =   p c ;  
 	 	 	 }  
  
 	 	 	 i f   ( t h i s . p a n e l M o d e   = =   " C o d e " )  
 	 	 	 {  
 	 	 	 	 i f   ( f o r c e )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . c o d e A c c e s s o r . D i r t y M a r k P C ( ) ;  
 	 	 	 	 }  
  
 	 	 	 	 t h i s . c o d e A c c e s s o r . M a r k P C   =   p c ;  
 	 	 	 }  
 	 	 }  
  
  
 	 	 p u b l i c   b o o l   I s E m p t y P a n e l  
 	 	 {  
 	 	 	 / / 	 E s t - o n   e n   m o d e   s a n s   p a n n e a u   d ' a f f i c h a g e   ?  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 r e t u r n   t h i s . p a n e l M o d e   = =   " Q u i c k " ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   i n t   A d d r e s s B i t s  
 	 	 {  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 r e t u r n   t h i s . G e t B i t s ( t h i s . a d d r e s s S w i t c h s ) ;  
 	 	 	 }  
 	 	 	 s e t  
 	 	 	 {  
 	 	 	 	 t h i s . S e t B i t s ( t h i s . a d d r e s s D i g i t s ,   t h i s . a d d r e s s L e d s ,   v a l u e ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   i n t   D a t a B i t s  
 	 	 {  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 r e t u r n   t h i s . G e t B i t s ( t h i s . d a t a S w i t c h s ) ;  
 	 	 	 }  
 	 	 	 s e t  
 	 	 	 {  
 	 	 	 	 t h i s . S e t B i t s ( t h i s . d a t a D i g i t s ,   t h i s . d a t a L e d s ,   v a l u e ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   S e t B i t s ( L i s t < M y W i d g e t s . D i g i t >   d i g i t s ,   L i s t < M y W i d g e t s . L e d >   l e d s ,   i n t   v a l u e )  
 	 	 {  
 	 	 	 / / 	 I n i t i a l i s e   u n e   r a n g é e   d e   l e d s .  
 	 	 	 i f   ( t h i s . I s E m p t y P a n e l )  
 	 	 	 {  
 	 	 	 	 r e t u r n ;  
 	 	 	 }  
  
 	 	 	 f o r   ( i n t   i = 0 ;   i < d i g i t s . C o u n t ;   i + + )  
 	 	 	 {  
 	 	 	 	 d i g i t s [ i ] . H e x V a l u e   =   ( v a l u e   > >   i * 4 )   &   0 x 0 f ;  
 	 	 	 }  
  
 	 	 	 f o r   ( i n t   i = 0 ;   i < l e d s . C o u n t ;   i + + )  
 	 	 	 {  
 	 	 	 	 l e d s [ i ] . A c t i v e S t a t e   =   ( v a l u e   &   ( 1   < <   i ) )   = =   0   ?   A c t i v e S t a t e . N o   :   A c t i v e S t a t e . Y e s ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   i n t   G e t B i t s ( L i s t < M y W i d g e t s . S w i t c h >   s w i t c h s )  
 	 	 {  
 	 	 	 / / 	 R e t o u r n e   l a   v a l e u r   d ' u n e   r a n g é e   d e   s w i t c h s .  
 	 	 	 i n t   v a l u e   =   0 ;  
  
 	 	 	 f o r   ( i n t   i = 0 ;   i < s w i t c h s . C o u n t ;   i + + )  
 	 	 	 {  
 	 	 	 	 i f   ( s w i t c h s [ i ] . A c t i v e S t a t e   = =   A c t i v e S t a t e . Y e s )  
 	 	 	 	 {  
 	 	 	 	 	 v a l u e   | =   ( 1   < <   i ) ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 r e t u r n   v a l u e ;  
 	 	 }  
  
  
 	 	 p u b l i c   v o i d   K e y b o a r d C h a n g e d ( M y W i d g e t s . P u s h B u t t o n   b u t t o n ,   b o o l   p r e s s e d )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   l o r s q u ' u n e   t o u c h e   d u   c l a v i e r   s i m u l é   a   é t é   p r e s s é e   o u   r e l â c h é e .  
 	 	 	 i n t   k e y s   =   t h i s . m e m o r y . R e a d ( C o m p o n e n t s . M e m o r y . P e r i p h K e y b o a r d ) ;  
  
 	 	 	 i f   ( b u t t o n . I n d e x   <   0 x 0 8 )  
 	 	 	 {  
 	 	 	 	 i f   ( p r e s s e d )  
 	 	 	 	 {  
 	 	 	 	 	 k e y s   & =   ~ 0 x 0 7 ;  
 	 	 	 	 	 k e y s   | =   b u t t o n . I n d e x ;  
 	 	 	 	 	 k e y s   | =   0 x 8 0 ;     / /   m e t   l e   b i t   f u l l  
 	 	 	 	 }  
 	 	 	 }  
 	 	 	 e l s e     / /   S h i f t ,   C t r l   o u   f l è c h e   ?  
 	 	 	 {  
 	 	 	 	 i n t   i n d e x   =   b u t t o n . I n d e x ;  
  
 	 	 	 	 i f   ( i n d e x   = =   1 0 0 )     / /   l e f t   ?  
 	 	 	 	 {  
 	 	 	 	 	 i n d e x   =   0 x 2 0 ;  
 	 	 	 	 }  
 	 	 	 	 e l s e   i f   ( i n d e x   = =   1 0 1 )     / /   d o w n   ?  
 	 	 	 	 {  
 	 	 	 	 	 i n d e x   =   0 x 0 8 ;  
 	 	 	 	 }  
 	 	 	 	 e l s e   i f   ( i n d e x   = =   1 0 2 )     / /   r i g h t   ?  
 	 	 	 	 {  
 	 	 	 	 	 i n d e x   =   0 x 4 0 ;  
 	 	 	 	 }  
 	 	 	 	 e l s e   i f   ( i n d e x   = =   1 0 3 )     / /   u p   ?  
 	 	 	 	 {  
 	 	 	 	 	 i n d e x   =   0 x 1 0 ;  
 	 	 	 	 }  
  
 	 	 	 	 i f   ( b u t t o n . A c t i v e S t a t e   = =   A c t i v e S t a t e . Y e s )  
 	 	 	 	 {  
 	 	 	 	 	 k e y s   | =   i n d e x ;  
 	 	 	 	 }  
 	 	 	 	 e l s e  
 	 	 	 	 {  
 	 	 	 	 	 k e y s   & =   ~ i n d e x ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 t h i s . m e m o r y . W r i t e ( C o m p o n e n t s . M e m o r y . P e r i p h K e y b o a r d ,   k e y s ) ;  
 	 	 }  
  
  
 	 	 # r e g i o n   P r o c e s s o r  
 	 	 p r o t e c t e d   v o i d   P r o c e s s o r R e s e t ( )  
 	 	 {  
 	 	 	 / / 	 R e s e t   d u   p r o c e s s e u r   p o u r   d é m a r r e r   à   l ' a d r e s s e   0 .  
 	 	 	 t h i s . p r o c e s s o r . R e s e t ( ) ;  
 	 	 	 t h i s . P r o c e s s o r F e e d b a c k ( ) ;  
  
 	 	 	 t h i s . m e m o r y . C l e a r P e r i p h ( ) ;  
 	 	 	 t h i s . S e a r c h K e y ( 0 x 0 8 ) . A c t i v e S t a t e   =   A c t i v e S t a t e . N o ;     / /   r e l â c h e   S h i f t  
 	 	 	 t h i s . S e a r c h K e y ( 0 x 1 0 ) . A c t i v e S t a t e   =   A c t i v e S t a t e . N o ;     / /   r e l â c h e   C t r l  
 	 	 	 t h i s . S e a r c h K e y ( 1 0 0 ) . A c t i v e S t a t e   =   A c t i v e S t a t e . N o ;     / /   r e l â c h e   L e f t  
 	 	 	 t h i s . S e a r c h K e y ( 1 0 1 ) . A c t i v e S t a t e   =   A c t i v e S t a t e . N o ;     / /   r e l â c h e   D o w n  
 	 	 	 t h i s . S e a r c h K e y ( 1 0 2 ) . A c t i v e S t a t e   =   A c t i v e S t a t e . N o ;     / /   r e l â c h e   R i g h t  
 	 	 	 t h i s . S e a r c h K e y ( 1 0 3 ) . A c t i v e S t a t e   =   A c t i v e S t a t e . N o ;     / /   r e l â c h e   U p  
 	 	 	 t h i s . U p d a t e K e y b o a r d ( ) ;  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   P r o c e s s o r S t a r t ( )  
 	 	 {  
 	 	 	 / / 	 D é m a r r e   l e   p r o c e s s e u r .  
 	 	 	 i f   ( t h i s . s w i t c h S t e p . A c t i v e S t a t e   = =   A c t i v e S t a t e . N o )     / /   C o n t i n u o u s   ?  
 	 	 	 {  
 	 	 	 	 i f   ( t h i s . c l o c k   = =   n u l l )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . c l o c k   =   n e w   T i m e r ( ) ;  
 	 	 	 	 	 t h i s . P r o c e s s o r C l o c k A d j u s t ( ) ;  
 	 	 	 	 	 t h i s . c l o c k . T i m e E l a p s e d   + =   n e w   E v e n t H a n d l e r ( t h i s . H a n d l e C l o c k T i m e E l a p s e d ) ;  
 	 	 	 	 	 t h i s . c l o c k . S t a r t ( ) ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 	 e l s e     / /   S t e p   ?  
 	 	 	 {  
 	 	 	 	 t h i s . P r o c e s s o r F e e d b a c k ( ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   P r o c e s s o r C l o c k A d j u s t ( )  
 	 	 {  
 	 	 	 / / 	 A j u s t e   l ' h o r l o g e   d u   p r o c e s s e u r .  
 	 	 	 i f   ( t h i s . c l o c k   ! =   n u l l )  
 	 	 	 {  
 	 	 	 	 / / 	 N ' u t i l i s e   p a s   l e   m o d e   A u t o R e p e a t ,   q u i   p o s e   d e s   p r o b l è m e s   d ' a c c u m u l a t i o n   d ' é v é n e m e n t s  
 	 	 	 	 / / 	 l o r s q u e   l e   t r a i t e m e n t   e s t   l e n t   ( p a r   e x e m p l e   a v e c   l e   p a n n e a u   [ C O D E ] ) .   U n   S t a r t   e s t  
 	 	 	 	 / / 	 r e f a i t   à   c h a q u e   é v é n e m e n t   H a n d l e C l o c k T i m e E l a p s e d .  
 	 	 	 	 t h i s . c l o c k . D e l a y   =   1 . 0 / S y s t e m . M a t h . M i n ( t h i s . i p s ,   D o l p h i n A p p l i c a t i o n . R e a l M a x I p s ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   P r o c e s s o r S t o p ( )  
 	 	 {  
 	 	 	 / / 	 S t o p p e   l e   p r o c e s s e u r .  
 	 	 	 i f   ( t h i s . c l o c k   ! =   n u l l )  
 	 	 	 {  
 	 	 	 	 t h i s . c l o c k . S t o p ( ) ;  
 	 	 	 	 t h i s . c l o c k . T i m e E l a p s e d   - =   n e w   E v e n t H a n d l e r ( t h i s . H a n d l e C l o c k T i m e E l a p s e d ) ;  
 	 	 	 	 t h i s . c l o c k . D i s p o s e ( ) ;  
 	 	 	 	 t h i s . c l o c k   =   n u l l ;  
 	 	 	 }  
  
 	 	 	 t h i s . b r e a k A d d r e s s   =   M i s c . u n d e f i n e d ;  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   P r o c e s s o r B r e a k I n ( i n t   r e t A d d r e s s )  
 	 	 {  
 	 	 	 / / 	 P a r t   j u s q u ' à   u n   b r e a k .  
 	 	 	 t h i s . b r e a k A d d r e s s   =   r e t A d d r e s s ;  
  
 	 	 	 t h i s . s w i t c h S t e p . A c t i v e S t a t e   =   A c t i v e S t a t e . N o ;     / /   C o n t i n u o u s  
 	 	 	 t h i s . U p d a t e B u t t o n s ( ) ;  
  
 	 	 	 t h i s . P r o c e s s o r S t a r t ( ) ;  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   P r o c e s s o r B r e a k O u t ( )  
 	 	 {  
 	 	 	 / / 	 S t o p p e   u n e   f o i s   l e   b r e a k   r e n c o n t r é .  
 	 	 	 t h i s . s w i t c h S t e p . A c t i v e S t a t e   =   A c t i v e S t a t e . Y e s ;     / /   S t e p  
 	 	 	 t h i s . U p d a t e B u t t o n s ( ) ;  
 	 	 	  
 	 	 	 t h i s . P r o c e s s o r S t o p ( ) ;  
 	 	 	 t h i s . P r o c e s s o r F e e d b a c k ( ) ;  
 	 	 }  
  
 	 	 p r o t e c t e d   b o o l   P r o c e s s o r C l o c k ( )  
 	 	 {  
 	 	 	 / / 	 E x é c u t e   u n e   i n s t r u c t i o n   d u   p r o c e s s e u r .  
 	 	 	 / / 	 R e t o u r n e   t r u e   s i   l e   p r o c e s s e u r   e s t   a r r i v é   s u r   u n   b r e a k .  
 	 	 	 t h i s . p r o c e s s o r . C l o c k ( ) ;  
  
 	 	 	 i f   ( t h i s . p r o c e s s o r . I s H a l t e d )  
 	 	 	 {  
 	 	 	 	 t h i s . P r o c e s s o r S t o p ( ) ;  
  
 	 	 	 	 t h i s . A d d r e s s B i t s   =   t h i s . A d d r e s s B i t s ;  
 	 	 	 	 t h i s . D a t a B i t s   =   0 ;  
 	 	 	 	  
 	 	 	 	 t h i s . b u t t o n R u n . A c t i v e S t a t e   =   A c t i v e S t a t e . N o ;  
 	 	 	 	 t h i s . U p d a t e B u t t o n s ( ) ;  
 	 	 	 }  
  
 	 	 	 i f   ( t h i s . b r e a k A d d r e s s   ! =   M i s c . u n d e f i n e d )  
 	 	 	 {  
 	 	 	 	 i n t   a d d r e s s   =   t h i s . p r o c e s s o r . G e t R e g i s t e r V a l u e ( " P C " ) ;  
 	 	 	 	 r e t u r n   a d d r e s s   = =   t h i s . b r e a k A d d r e s s ;  
 	 	 	 }  
  
 	 	 	 r e t u r n   f a l s e ;  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   P r o c e s s o r F e e d b a c k ( )  
 	 	 {  
 	 	 	 / / 	 F e e d b a c k   v i s u e l   d u   p r o c e s s e u r   d a n s   l e s   w i d g e t s   d e s   d i f f é r e n t s   p a n n e a u x .  
 	 	 	 i f   ( t h i s . I s E m p t y P a n e l )  
 	 	 	 {  
 	 	 	 	 r e t u r n ;  
 	 	 	 }  
  
 	 	 	 i n t   p c   =   t h i s . p r o c e s s o r . G e t R e g i s t e r V a l u e ( " P C " ) ;  
 	 	 	 t h i s . A d d r e s s B i t s   =   p c ;  
 	 	 	 t h i s . D a t a B i t s   =   t h i s . m e m o r y . R e a d ( p c ) ;  
  
 	 	 	 f o r e a c h   ( M y W i d g e t s . T e x t F i e l d H e x a   f i e l d   i n   t h i s . r e g i s t e r F i e l d s )  
 	 	 	 {  
 	 	 	 	 t h i s . i g n o r e C h a n g e   =   t r u e ;  
 	 	 	 	 f i e l d . H e x a V a l u e   =   t h i s . p r o c e s s o r . G e t R e g i s t e r V a l u e ( f i e l d . L a b e l ) ;  
 	 	 	 	 t h i s . i g n o r e C h a n g e   =   f a l s e ;  
 	 	 	 }  
  
 	 	 	 f o r e a c h   ( T e x t F i e l d   f i e l d   i n   t h i s . c o d e R e g i s t e r s )  
 	 	 	 {  
 	 	 	 	 t h i s . i g n o r e C h a n g e   =   t r u e ;  
 	 	 	 	 i n t   v a l u e   =   t h i s . p r o c e s s o r . G e t R e g i s t e r V a l u e ( f i e l d . N a m e ) ;  
 	 	 	 	 i n t   b i t C o u n t   =   t h i s . p r o c e s s o r . G e t R e g i s t e r S i z e ( f i e l d . N a m e ) ;  
 	 	 	 	 f i e l d . T e x t   =   M y W i d g e t s . T e x t F i e l d H e x a . G e t H e x a T e x t ( v a l u e ,   b i t C o u n t ) ;  
 	 	 	 	 t h i s . i g n o r e C h a n g e   =   f a l s e ;  
 	 	 	 }  
  
 	 	 	 t h i s . M a r k P C ( p c ,   f a l s e ) ;  
 	 	 	 t h i s . U p d a t e M e m o r y B a n k ( ) ;  
 	 	 }  
 	 	 # e n d r e g i o n  
  
 	 	 # r e g i o n   B i n a r y   S e r i a l i z a t i o n  
 	 	 p r o t e c t e d   b o o l   Q u e s t i o n Y e s N o ( s t r i n g   m e s s a g e )  
 	 	 {  
 	 	 	 / / 	 P o s e   u n e   q u e s t i o n   d e   t y p e   o u i / n o n .   R e t o u r n e   t r u e   e n   c a s   d e   r é p o n s e   p o s i t i v e .  
 	 	 	 s t r i n g   t i t l e   =   T e x t L a y o u t . C o n v e r t T o S i m p l e T e x t ( R e s . S t r i n g s . W i n d o w . T i t l e ) ;  
 	 	 	 s t r i n g   i c o n   =   " m a n i f e s t : E p s i t e c . C o m m o n . D i a l o g s . I m a g e s . Q u e s t i o n . i c o n " ;  
 	 	 	 C o m m o n . D i a l o g s . I D i a l o g   d i a l o g   =   C o m m o n . D i a l o g s . M e s s a g e D i a l o g . C r e a t e Y e s N o ( t i t l e ,   i c o n ,   m e s s a g e ,   n u l l ,   n u l l ,   n u l l ) ;  
 	 	 	 d i a l o g . O w n e r W i n d o w   =   t h i s . W i n d o w ;  
 	 	 	 d i a l o g . O p e n D i a l o g ( ) ;  
 	 	 	 r e t u r n   d i a l o g . D i a l o g R e s u l t   = =   C o m m o n . D i a l o g s . D i a l o g R e s u l t . Y e s ;  
 	 	 }  
  
 	 	 p r o t e c t e d   b o o l   D l g O p e n F i l e n a m e ( )  
 	 	 {  
 	 	 	 / / 	 D e m a n d e   l e   n o m   d u   f i c h i e r   à   o u v r i r .  
 	 	 	 C o m m o n . D i a l o g s . F i l e O p e n   d l g   =   n e w   C o m m o n . D i a l o g s . F i l e O p e n ( ) ;  
 	 	 	 d l g . T i t l e   =   T e x t L a y o u t . C o n v e r t T o S i m p l e T e x t ( R e s . S t r i n g s . D i a l o g . O p e n . T i t l e ) ;  
  
 	 	 	 i f   ( t h i s . f i r s t O p e n S a v e D i a l o g )  
 	 	 	 {  
 	 	 	 	 d l g . I n i t i a l D i r e c t o r y   =   t h i s . U s e r S a m p l e s P a t h ;  
 	 	 	 	 d l g . F i l e N a m e   =   " " ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 d l g . F i l e N a m e   =   " " ;  
 	 	 	 }  
  
 	 	 	 d l g . F i l t e r s . A d d ( " d o l p h i n " ,   R e s . S t r i n g s . D i a l o g . F i l e . P r o g r a m s ,   " * . d o l p h i n " ) ;  
 	 	 	 d l g . O w n e r   =   t h i s . W i n d o w ;  
 	 	 	 d l g . O p e n D i a l o g ( ) ;     / /   a f f i c h e   l e   d i a l o g u e . . .  
  
 	 	 	 i f   ( d l g . R e s u l t   ! =   C o m m o n . D i a l o g s . D i a l o g R e s u l t . A c c e p t )  
 	 	 	 {  
 	 	 	 	 r e t u r n   f a l s e ;  
 	 	 	 }  
  
 	 	 	 t h i s . f i l e n a m e   =   d l g . F i l e N a m e ;  
 	 	 	 r e t u r n   t r u e ;  
 	 	 }  
  
 	 	 p r o t e c t e d   b o o l   D l g S a v e F i l e n a m e ( )  
 	 	 {  
 	 	 	 / / 	 D e m a n d e   l e   n o m   d u   f i c h i e r   à   e n r e g i s t r e r .  
 	 	 	 C o m m o n . D i a l o g s . F i l e S a v e   d l g   =   n e w   C o m m o n . D i a l o g s . F i l e S a v e ( ) ;  
 	 	 	 d l g . P r o m p t F o r O v e r w r i t i n g   =   t r u e ;  
 	 	 	 d l g . T i t l e   =   T e x t L a y o u t . C o n v e r t T o S i m p l e T e x t ( R e s . S t r i n g s . D i a l o g . S a v e . T i t l e ) ;  
  
 	 	 	 i f   ( t h i s . f i r s t O p e n S a v e D i a l o g   & &   s t r i n g . I s N u l l O r E m p t y ( t h i s . f i l e n a m e ) )  
 	 	 	 {  
 	 	 	 	 d l g . I n i t i a l D i r e c t o r y   =   t h i s . U s e r S a m p l e s P a t h ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 d l g . F i l e N a m e   =   t h i s . f i l e n a m e ;  
 	 	 	 }  
  
 	 	 	 d l g . F i l t e r s . A d d ( " d o l p h i n " ,   R e s . S t r i n g s . D i a l o g . F i l e . P r o g r a m s ,   " * . d o l p h i n " ) ;  
 	 	 	 d l g . O w n e r W i n d o w   =   t h i s . W i n d o w ;  
 	 	 	 d l g . O p e n D i a l o g ( ) ;     / /   a f f i c h e   l e   d i a l o g u e . . .  
  
 	 	 	 i f   ( d l g . D i a l o g R e s u l t   ! =   C o m m o n . D i a l o g s . D i a l o g R e s u l t . A c c e p t )  
 	 	 	 {  
 	 	 	 	 r e t u r n   f a l s e ;  
 	 	 	 }  
  
 	 	 	 t h i s . f i l e n a m e   =   d l g . F i l e N a m e ;  
 	 	 	 r e t u r n   t r u e ;  
 	 	 }  
  
 	 	 p r o t e c t e d   C o m m o n . D i a l o g s . D i a l o g R e s u l t   D l g A u t o S a v e ( )  
 	 	 {  
 	 	 	 / / 	 D e m a n d e   s ' i l   f a u t   e n r e g i s t r e r   l e   p r o g r a m m e   e n   c o u r s .  
 	 	 	 i f   ( ! t h i s . d i r t y )  
 	 	 	 {  
 	 	 	 	 r e t u r n   C o m m o n . D i a l o g s . D i a l o g R e s u l t . N o ;  
 	 	 	 }  
  
 	 	 	 s t r i n g   t i t l e   =   T e x t L a y o u t . C o n v e r t T o S i m p l e T e x t ( R e s . S t r i n g s . W i n d o w . T i t l e ) ;  
 	 	 	 s t r i n g   i c o n   =   " m a n i f e s t : E p s i t e c . C o m m o n . D i a l o g s . I m a g e s . Q u e s t i o n . i c o n " ;  
  
 	 	 	 s t r i n g   m e s s a g e   =   R e s . S t r i n g s . D i a l o g . A u t o S a v e . Q u e s t i o n 1 ;  
  
 	 	 	 i f   ( ! s t r i n g . I s N u l l O r E m p t y ( t h i s . f i l e n a m e ) )  
 	 	 	 {  
 	 	 	 	 m e s s a g e   =   s t r i n g . F o r m a t ( R e s . S t r i n g s . D i a l o g . A u t o S a v e . Q u e s t i o n 2 ,   S y s t e m . I O . P a t h . G e t F i l e N a m e W i t h o u t E x t e n s i o n ( t h i s . f i l e n a m e ) ) ;  
 	 	 	 }  
  
 	 	 	 C o m m o n . D i a l o g s . I D i a l o g   d i a l o g   =   C o m m o n . D i a l o g s . M e s s a g e D i a l o g . C r e a t e Y e s N o C a n c e l ( t i t l e ,   i c o n ,   m e s s a g e ,   n u l l ,   n u l l ,   n u l l ) ;  
 	 	 	 d i a l o g . O w n e r W i n d o w   =   t h i s . W i n d o w ;  
 	 	 	 d i a l o g . O p e n D i a l o g ( ) ;  
 	 	 	 r e t u r n   d i a l o g . D i a l o g R e s u l t ;  
  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   C o p y S a m p l e F i l e s ( )  
 	 	 {  
 	 	 	 / / 	 C o p i e   t o u s   l e s   f i c h i e r s   d e   " c : / P r o g r a m   F i l e s / E p s i t e c / D a u p h i n / S a m p l e s "   d a n s   " c : / M e s   D o c u m e n t s / E p s i t e c / D a u p h i n " .  
 	 	 	 / / 	 S i   u n   f i c h i e r   e x i s t e   d é j à   à   l a   d e s t i n a t i o n   ( p a r   e x e m p l e   p a r c e   q u e   l ' u t i l i s a t e u r   à   d é j à   i n s t a l l é   l e   l o g i c i e l  
 	 	 	 / / 	 e t   q u ' i l   a   m o d i f i é   l e   f i c h i e r ) ,   i l   n ' e s t   p a s   c o p i é .  
 	 	 	 / / 	 C e c i   e s t   n é c e s s a i r e ,   c a r   i l   n ' e s t   p a s   a u t o r i s é   d ' é c r i r e   d a n s   " c : / P r o g r a m   F i l e s " .  
 	 	 	 / / 	 -   X P   s e m b l e   l ' a u t o r i s e r ,   m a i s   l e   l a n c e m e n t   d e   l ' a p p l i c a t i o n   " r é p a r e "   l e s   f i c h i e r s ,   e t   r e m e t   d o n c   l e s   v e r s i o n s   i n i t i a l e s   !  
 	 	 	 / / 	 -   V i s t a   l ' a u t o r i s e   e n   d é v i a n t   l ' é c r i t u r e   d a n s   u n   d o s s i e r   f a n t ô m e .  
 	 	 	 s t r i n g   s r c D i r   =   t h i s . O r i g i n a l S a m p l e s P a t h ;  
 	 	 	 s t r i n g   d s t D i r   =   t h i s . U s e r S a m p l e s P a t h ;  
  
 	 	 	 S y s t e m . I O . D i r e c t o r y . C r e a t e D i r e c t o r y ( d s t D i r ) ;  
  
 	 	 	 s t r i n g [ ]   s r c F i l e s   =   S y s t e m . I O . D i r e c t o r y . G e t F i l e s ( s r c D i r ,   " * . d o l p h i n " ,   S y s t e m . I O . S e a r c h O p t i o n . T o p D i r e c t o r y O n l y ) ;  
 	 	 	 f o r e a c h   ( s t r i n g   s r c F i l e   i n   s r c F i l e s )  
 	 	 	 {  
 	 	 	 	 s t r i n g   d s t F i l e   =   S y s t e m . I O . P a t h . C o m b i n e ( d s t D i r ,   S y s t e m . I O . P a t h . G e t F i l e N a m e ( s r c F i l e ) ) ;  
 	 	 	 	 i f   ( ! S y s t e m . I O . F i l e . E x i s t s ( d s t F i l e ) )  
 	 	 	 	 {  
 	 	 	 	 	 S y s t e m . I O . F i l e . C o p y ( s r c F i l e ,   d s t F i l e ) ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   N e w ( )  
 	 	 {  
 	 	 	 / / 	 E f f a c e   l e   p r o g r a m m e   e n   c o u r s .  
 	 	 	 i f   ( t h i s . A u t o S a v e ( ) )  
 	 	 	 {  
 	 	 	 	 t h i s . f i e l d P r o g r a m R e m . T e x t   =   D o l p h i n A p p l i c a t i o n . P r o g r a m E m p t y R e m ;  
 	 	 	 	 t h i s . f i e l d P r o g r a m R e m . C u r s o r   =   0 ;  
  
 	 	 	 	 t h i s . c a l m E d i t o r . T e x t   =   " " ;  
 	 	 	 	 t h i s . c a l m E d i t o r . C u r s o r   =   0 ;  
  
 	 	 	 	 t h i s . S t o p ( ) ;  
 	 	 	 	 t h i s . m e m o r y . C l e a r R a m ( ) ;  
 	 	 	 	 t h i s . f i l e n a m e   =   " " ;  
 	 	 	 	 t h i s . D i r t y   =   f a l s e ;  
 	 	 	 	 t h i s . d i r t y C a l m   =   f a l s e ;  
 	 	 	 	 t h i s . P r o c e s s o r R e s e t ( ) ;  
 	 	 	 	 t h i s . A d d r e s s B i t s   =   0 ;  
 	 	 	 	 t h i s . D a t a B i t s   =   0 ;  
 	 	 	 	 t h i s . U p d a t e D a t a ( ) ;  
 	 	 	 	 t h i s . U p d a t e B u t t o n s ( ) ;  
 	 	 	 	 t h i s . U p d a t e F i l e n a m e ( ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   b o o l   A u t o S a v e ( )  
 	 	 {  
 	 	 	 / / 	 D e m a n d e   s ' i l   f a u t   e n r e g i s t r e r   l e   p r o g r a m m e   e n   c o u r s   a v a n t   d e   p a s s e r   à   u n   a u t r e   p r o g r a m m e .  
 	 	 	 / / 	 R e t o u r n e   t r u e   s i   o n   p e u t   c o n t i n u e r   ( e t   d o n c   e f f a c e r   l e   p r o g r a m m e   e n   c o u r s   o u   e n   o u v r i r   u n   a u t r e ) .  
 	 	 	 C o m m o n . D i a l o g s . D i a l o g R e s u l t   r e s u l t   =   t h i s . D l g A u t o S a v e ( ) ;  
  
 	 	 	 i f   ( r e s u l t   = =   C o m m o n . D i a l o g s . D i a l o g R e s u l t . C a n c e l )  
 	 	 	 {  
 	 	 	 	 r e t u r n   f a l s e ;  
 	 	 	 }  
 	 	 	  
 	 	 	 i f   ( r e s u l t   = =   C o m m o n . D i a l o g s . D i a l o g R e s u l t . Y e s )  
 	 	 	 {  
 	 	 	 	 i f   ( s t r i n g . I s N u l l O r E m p t y ( t h i s . f i l e n a m e ) )  
 	 	 	 	 {  
 	 	 	 	 	 i f   ( ! t h i s . D l g S a v e F i l e n a m e ( ) )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 r e t u r n   f a l s e ;  
 	 	 	 	 	 }  
 	 	 	 	 }  
  
 	 	 	 	 i f   ( t h i s . S a v e ( ) )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . f i r s t O p e n S a v e D i a l o g   =   f a l s e ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 r e t u r n   t r u e ;  
 	 	 }  
  
 	 	 p r o t e c t e d   s t r i n g   O r i g i n a l S a m p l e s P a t h  
 	 	 {  
 	 	 	 / / 	 R e t o u r n e   l e   n o m   d u   d o s s i e r   c o n t e n a n t   l e s   e x e m p l e s   o r i g i n a u x .  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 r e t u r n   S y s t e m . I O . P a t h . C o m b i n e ( C o m m o n . S u p p o r t . G l o b a l s . D i r e c t o r i e s . E x e c u t a b l e ,   T e x t L a y o u t . C o n v e r t T o S i m p l e T e x t ( R e s . S t r i n g s . P a t h . O r i g i n a l S a m p l e s ) ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   s t r i n g   U s e r S a m p l e s P a t h  
 	 	 {  
 	 	 	 / / 	 R e t o u r n e   l e   n o m   d u   d o s s i e r   c o n t e n a n t   l e s   e x e m p l e s   m o d i f i a b l e s .  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 F o l d e r I t e m   m y D o c   =   F i l e M a n a g e r . G e t F o l d e r I t e m ( F o l d e r I d . M y D o c u m e n t s ,   F o l d e r Q u e r y M o d e . N o I c o n s ) ;  
 	 	 	 	 r e t u r n   S y s t e m . I O . P a t h . C o m b i n e ( m y D o c . F u l l P a t h ,   T e x t L a y o u t . C o n v e r t T o S i m p l e T e x t ( R e s . S t r i n g s . P a t h . U s e r S a m p l e s ) ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   s t r i n g   O p e n ( )  
 	 	 {  
 	 	 	 / / 	 O u v r e   u n   n o u v e a u   p r o g r a m m e .  
 	 	 	 / / 	 R e t o u r n e   u n e   é v e n t u e l l e   e r r e u r .  
 	 	 	 t h i s . S t o p ( ) ;  
 	 	 	 s t r i n g   e r r   =   n u l l ;  
  
 	 	 	 s t r i n g   d a t a   =   n u l l ;  
 	 	 	 t r y  
 	 	 	 {  
 	 	 	 	 d a t a   =   S y s t e m . I O . F i l e . R e a d A l l T e x t ( t h i s . f i l e n a m e ) ;  
 	 	 	 }  
 	 	 	 c a t c h  
 	 	 	 {  
 	 	 	 	 d a t a   =   n u l l ;  
 	 	 	 	 t h i s . f i l e n a m e   =   n u l l ;  
 	 	 	 	 e r r   =   R e s . S t r i n g s . E r r o r . O p e n ;  
 	 	 	 }  
  
 	 	 	 i f   ( d a t a   ! =   n u l l )  
 	 	 	 {  
 	 	 	 	 e r r   =   t h i s . D e s e r i a l i z e ( d a t a ) ;  
 	 	 	 	 i f   ( ! s t r i n g . I s N u l l O r E m p t y ( e r r ) )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . f i l e n a m e   =   n u l l ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 i f   ( t h i s . f i e l d P r o g r a m R e m . T e x t   ! =   D o l p h i n A p p l i c a t i o n . P r o g r a m E m p t y R e m )  
 	 	 	 {  
 	 	 	 	 t h i s . b o o k . A c t i v e P a g e   =   t h i s . p a g e P r o g r a m ;  
 	 	 	 }  
  
 	 	 	 t h i s . D i r t y   =   f a l s e ;  
 	 	 	 t h i s . d i r t y C a l m   =   f a l s e ;  
 	 	 	 t h i s . U p d a t e D a t a ( ) ;     / /   e n   p r e m i e r ,   p o u r   d é s a s s e m b l e r  
 	 	 	 t h i s . P r o c e s s o r R e s e t ( ) ;  
 	 	 	 t h i s . A d d r e s s B i t s   =   0 ;  
 	 	 	 t h i s . D a t a B i t s   =   0 ;  
 	 	 	 t h i s . U p d a t e B u t t o n s ( ) ;  
 	 	 	 t h i s . U p d a t e C l o c k B u t t o n s ( ) ;  
 	 	 	 t h i s . U p d a t e F i l e n a m e ( ) ;  
  
 	 	 	 r e t u r n   e r r ;  
 	 	 }  
  
 	 	 p r o t e c t e d   b o o l   S a v e ( )  
 	 	 {  
 	 	 	 / / 	 E n r e g i s t r e   l e   p r o g r a m m e   e n   c o u r s .  
 	 	 	 / / 	 R e t o u r n e   f a l s e   e n   c a s   d ' e r r e u r .  
 	 	 	 s t r i n g   d a t a   =   t h i s . S e r i a l i z e ( ) ;  
 	 	 	 t r y  
 	 	 	 {  
 	 	 	 	 S y s t e m . I O . F i l e . W r i t e A l l T e x t ( t h i s . f i l e n a m e ,   d a t a ) ;  
 	 	 	 }  
 	 	 	 c a t c h  
 	 	 	 {  
 	 	 	 	 t h i s . f i l e n a m e   =   n u l l ;  
 	 	 	 }  
  
 	 	 	 t h i s . D i r t y   =   f a l s e ;  
 	 	 	 t h i s . U p d a t e F i l e n a m e ( ) ;  
  
 	 	 	 r e t u r n   ! s t r i n g . I s N u l l O r E m p t y ( t h i s . f i l e n a m e ) ;  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   S t o p ( )  
 	 	 {  
 	 	 	 / / 	 S t o p p e   l e   p r o g r a m m e   e n   c o u r s .  
 	 	 	 t h i s . P r o c e s s o r S t o p ( ) ;  
 	 	 	 t h i s . P r o c e s s o r R e s e t ( ) ;  
 	 	 	 t h i s . b u t t o n R u n . A c t i v e S t a t e   =   A c t i v e S t a t e . N o ;  
 	 	 	 t h i s . U p d a t e B u t t o n s ( ) ;  
 	 	 }  
  
 	 	 p u b l i c   b o o l   D i r t y  
 	 	 {  
 	 	 	 / / 	 I n d i q u e   s i   l e   p r o g r a m m e   e n   c o u r s   d o i t   ê t r e   e n r e g i s t r é .  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 r e t u r n   t h i s . d i r t y ;  
 	 	 	 }  
 	 	 	 s e t  
 	 	 	 {  
 	 	 	 	 / / 	 S i   o n   v e u t   c o n s i d é r e r   l e   p r o g r a m m e   c o m m e   d e v a n t   ê t r e   e n r e g i s t r é   e t   q u e  
 	 	 	 	 / / 	 l a   R a m   e s t   e n t i è r e m e n t   v i d e ,   c e   n ' e s t   p a s   n é c e s s a i r e ,   c a r   l a   s é r i a l i s a t i o n  
 	 	 	 	 / / 	 n ' a u r a   a u c u n   p r o g r a m m e   à   s a u v e g a r d e r   !  
 	 	 	 	 i f   ( v a l u e   = =   t r u e   & &   t h i s . m e m o r y . I s E m p t y R a m   & &   s t r i n g . I s N u l l O r E m p t y ( t h i s . c a l m E d i t o r . T e x t ) )  
 	 	 	 	 {  
 	 	 	 	 	 r e t u r n ;  
 	 	 	 	 }  
  
 	 	 	 	 i f   ( t h i s . d i r t y   ! =   v a l u e )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . d i r t y   =   v a l u e ;  
 	 	 	 	 	 t h i s . U p d a t e S a v e ( ) ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   s t r i n g   S e r i a l i z e ( )  
 	 	 {  
 	 	 	 / / 	 S é r i a l i s e   l a   v u e   é d i t é e   e t   r e t o u r n e   l e   r é s u l t a t   d a n s   u n   s t r i n g .  
 	 	 	 S y s t e m . T e x t . S t r i n g B u i l d e r   b u f f e r   =   n e w   S y s t e m . T e x t . S t r i n g B u i l d e r ( ) ;  
 	 	 	 S y s t e m . I O . S t r i n g W r i t e r   s t r i n g W r i t e r   =   n e w   S y s t e m . I O . S t r i n g W r i t e r ( b u f f e r ) ;  
 	 	 	 X m l T e x t W r i t e r   w r i t e r   =   n e w   X m l T e x t W r i t e r ( s t r i n g W r i t e r ) ;  
 	 	 	 w r i t e r . F o r m a t t i n g   =   F o r m a t t i n g . I n d e n t e d ;  
  
 	 	 	 t h i s . W r i t e X m l ( w r i t e r ) ;  
  
 	 	 	 w r i t e r . F l u s h ( ) ;  
 	 	 	 w r i t e r . C l o s e ( ) ;  
 	 	 	 r e t u r n   b u f f e r . T o S t r i n g ( ) ;  
 	 	 }  
  
 	 	 p r o t e c t e d   s t r i n g   D e s e r i a l i z e ( s t r i n g   d a t a )  
 	 	 {  
 	 	 	 / / 	 D é s é r i a l i s e   l a   v u e   à   p a r t i r   d ' u n   s t r i n g   d e   d o n n é e s .  
 	 	 	 / / 	 R e t o u r n e   u n e   é v e n t u e l l e   e r r e u r .  
 	 	 	 S y s t e m . I O . S t r i n g R e a d e r   s t r i n g R e a d e r   =   n e w   S y s t e m . I O . S t r i n g R e a d e r ( d a t a ) ;  
 	 	 	 X m l T e x t R e a d e r   r e a d e r   =   n e w   X m l T e x t R e a d e r ( s t r i n g R e a d e r ) ;  
 	 	 	  
 	 	 	 s t r i n g   e r r   =   t h i s . R e a d X m l ( r e a d e r ) ;  
  
 	 	 	 r e a d e r . C l o s e ( ) ;  
 	 	 	 r e t u r n   e r r ;  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   W r i t e X m l ( X m l W r i t e r   w r i t e r )  
 	 	 {  
 	 	 	 / / 	 S é r i a l i s e   t o u t   l e   p r o g r a m m e .  
 	 	 	 w r i t e r . W r i t e S t a r t D o c u m e n t ( ) ;  
 	 	 	 w r i t e r . W r i t e S t a r t E l e m e n t ( " D o l p h i n " ) ;  
 	 	 	  
 	 	 	 w r i t e r . W r i t e E l e m e n t S t r i n g ( " V e r s i o n " ,   M i s c . G e t V e r s i o n ( ) ) ;  
 	 	 	 w r i t e r . W r i t e E l e m e n t S t r i n g ( " P r o c e s s o r N a m e " ,   t h i s . p r o c e s s o r . N a m e ) ;  
 	 	 	 w r i t e r . W r i t e E l e m e n t S t r i n g ( " P r o c e s s o r I P S " ,   t h i s . i p s . T o S t r i n g ( S y s t e m . G l o b a l i z a t i o n . C u l t u r e I n f o . I n v a r i a n t C u l t u r e ) ) ;  
 	 	 	 w r i t e r . W r i t e E l e m e n t S t r i n g ( " P r o c e s s o r S t e p " ,   ( t h i s . s w i t c h S t e p . A c t i v e S t a t e   = =   A c t i v e S t a t e . Y e s )   ?   " S "   :   " C " ) ;  
 	 	 	 w r i t e r . W r i t e E l e m e n t S t r i n g ( " P r o c e s s o r I n t o " ,   ( t h i s . s w i t c h I n t o . A c t i v e S t a t e   = =   A c t i v e S t a t e . Y e s )   ?   " I "   :   " O " ) ;  
 	 	 	 w r i t e r . W r i t e E l e m e n t S t r i n g ( " P a n e l M o d e " ,   t h i s . p a n e l M o d e ) ;  
 	 	 	 w r i t e r . W r i t e E l e m e n t S t r i n g ( " D i s p l a y B i t m a p " ,   ( t h i s . d i s p l a y B u t t o n M o d e . A c t i v e S t a t e   = =   A c t i v e S t a t e . Y e s )   ?   " Y "   :   " N " ) ;  
 	 	 	 w r i t e r . W r i t e E l e m e n t S t r i n g ( " D i s p l a y T e c h n o " ,   ( t h i s . d i s p l a y B i t m a p . T e c h n o l o g y   = =   M y W i d g e t s . D i s p l a y . T y p e . L C D )   ?   " L C D "   :   " C R T " ) ;  
 	 	 	 w r i t e r . W r i t e E l e m e n t S t r i n g ( " K e y b o a r d A r r o w s " ,   t h i s . k e y b o a r d A r r o w s   ?   " Y "   :   " N " ) ;  
  
 	 	 	 i f   ( t h i s . f i e l d P r o g r a m R e m . T e x t   ! =   D o l p h i n A p p l i c a t i o n . P r o g r a m E m p t y R e m )  
 	 	 	 {  
 	 	 	 	 w r i t e r . W r i t e E l e m e n t S t r i n g ( " R e m " ,   t h i s . f i e l d P r o g r a m R e m . T e x t ) ;  
 	 	 	 }  
  
 	 	 	 i f   ( ! s t r i n g . I s N u l l O r E m p t y ( t h i s . c a l m E d i t o r . T e x t ) )  
 	 	 	 {  
 	 	 	 	 w r i t e r . W r i t e E l e m e n t S t r i n g ( " S o u r c e " ,   t h i s . c a l m E d i t o r . T e x t ) ;  
 	 	 	 }  
  
 	 	 	 w r i t e r . W r i t e E l e m e n t S t r i n g ( " M e m o r y D a t a " ,   t h i s . m e m o r y . G e t C o n t e n t ( ) ) ;  
  
 	 	 	 w r i t e r . W r i t e E n d E l e m e n t ( ) ;  
 	 	 	 w r i t e r . W r i t e E n d D o c u m e n t ( ) ;  
 	 	 }  
  
 	 	 p r o t e c t e d   s t r i n g   R e a d X m l ( X m l R e a d e r   r e a d e r )  
 	 	 {  
 	 	 	 / / 	 D é s é r i a l i s e   t o u t   l e   p r o g r a m m e .  
 	 	 	 / / 	 R e t o u r n e   u n e   é v e n t u e l l e   e r r e u r .  
 	 	 	 t h i s . m e m o r y . C l e a r R a m ( ) ;  
  
 	 	 	 t h i s . f i e l d P r o g r a m R e m . T e x t   =   D o l p h i n A p p l i c a t i o n . P r o g r a m E m p t y R e m ;  
 	 	 	 t h i s . f i e l d P r o g r a m R e m . C u r s o r   =   0 ;  
 	 	 	  
 	 	 	 t h i s . c a l m E d i t o r . T e x t   =   " " ;  
 	 	 	 t h i s . c a l m E d i t o r . C u r s o r   =   0 ;  
  
 	 	 	 r e a d e r . R e a d ( ) ;  
 	 	 	 w h i l e   ( t r u e )  
 	 	 	 {  
 	 	 	 	 i f   ( r e a d e r . N o d e T y p e   = =   X m l N o d e T y p e . E l e m e n t )  
 	 	 	 	 {  
 	 	 	 	 	 s t r i n g   n a m e   =   r e a d e r . L o c a l N a m e ;  
  
 	 	 	 	 	 i f   ( n a m e   = =   " V e r s i o n " )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 s t r i n g   e l e m e n t   =   r e a d e r . R e a d E l e m e n t S t r i n g ( ) ;  
 	 	 	 	 	 	 i f   ( M i s c . C o m p a r e V e r s i o n s ( e l e m e n t ,   M i s c . G e t V e r s i o n ( ) )   >   0 )  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 r e t u r n   R e s . S t r i n g s . E r r o r . R e a d . V e r s i o n ;  
 	 	 	 	 	 	 }  
 	 	 	 	 	 	 r e a d e r . R e a d ( ) ;  
 	 	 	 	 	 }  
 	 	 	 	 	 e l s e   i f   ( n a m e   = =   " P r o c e s s o r N a m e " )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 s t r i n g   e l e m e n t   =   r e a d e r . R e a d E l e m e n t S t r i n g ( ) ;  
 	 	 	 	 	 	 r e a d e r . R e a d ( ) ;  
 	 	 	 	 	 }  
 	 	 	 	 	 e l s e   i f   ( n a m e   = =   " P r o c e s s o r I P S " )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 s t r i n g   e l e m e n t   =   r e a d e r . R e a d E l e m e n t S t r i n g ( ) ;  
 	 	 	 	 	 	 t h i s . i p s   =   d o u b l e . P a r s e ( e l e m e n t ,   S y s t e m . G l o b a l i z a t i o n . C u l t u r e I n f o . I n v a r i a n t C u l t u r e ) ;  
 	 	 	 	 	 	 r e a d e r . R e a d ( ) ;  
 	 	 	 	 	 }  
 	 	 	 	 	 e l s e   i f   ( n a m e   = =   " P r o c e s s o r S t e p " )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 s t r i n g   e l e m e n t   =   r e a d e r . R e a d E l e m e n t S t r i n g ( ) ;  
 	 	 	 	 	 	 t h i s . s w i t c h S t e p . A c t i v e S t a t e   =   ( e l e m e n t   = =   " S " )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 	 	 	 t h i s . U p d a t e B u t t o n s ( ) ;  
 	 	 	 	 	 	 r e a d e r . R e a d ( ) ;  
 	 	 	 	 	 }  
 	 	 	 	 	 e l s e   i f   ( n a m e   = =   " P r o c e s s o r I n t o " )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 s t r i n g   e l e m e n t   =   r e a d e r . R e a d E l e m e n t S t r i n g ( ) ;  
 	 	 	 	 	 	 t h i s . s w i t c h I n t o . A c t i v e S t a t e   =   ( e l e m e n t   = =   " I " )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 	 	 	 r e a d e r . R e a d ( ) ;  
 	 	 	 	 	 }  
 	 	 	 	 	 e l s e   i f   ( n a m e   = =   " P a n e l M o d e " )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 s t r i n g   e l e m e n t   =   r e a d e r . R e a d E l e m e n t S t r i n g ( ) ;  
 	 	 	 	 	 	 t h i s . p a n e l M o d e   =   e l e m e n t ;  
 	 	 	 	 	 	 t h i s . U p d a t e P a n e l M o d e ( ) ;  
 	 	 	 	 	 	 r e a d e r . R e a d ( ) ;  
 	 	 	 	 	 }  
 	 	 	 	 	 e l s e   i f   ( n a m e   = =   " D i s p l a y B i t m a p " )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 s t r i n g   e l e m e n t   =   r e a d e r . R e a d E l e m e n t S t r i n g ( ) ;  
 	 	 	 	 	 	 t h i s . d i s p l a y B u t t o n M o d e . A c t i v e S t a t e   =   ( e l e m e n t   = =   " Y " )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 	 	 	 t h i s . U p d a t e D i s p l a y M o d e ( ) ;  
 	 	 	 	 	 	 r e a d e r . R e a d ( ) ;  
 	 	 	 	 	 }  
 	 	 	 	 	 e l s e   i f   ( n a m e   = =   " D i s p l a y T e c h n o " )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 s t r i n g   e l e m e n t   =   r e a d e r . R e a d E l e m e n t S t r i n g ( ) ;  
 	 	 	 	 	 	 t h i s . d i s p l a y B i t m a p . T e c h n o l o g y   =   ( e l e m e n t   = =   " L C D " )   ?   M y W i d g e t s . D i s p l a y . T y p e . L C D   :   M y W i d g e t s . D i s p l a y . T y p e . C R T ;  
 	 	 	 	 	 	 t h i s . U p d a t e D i s p l a y M o d e ( ) ;  
 	 	 	 	 	 	 r e a d e r . R e a d ( ) ;  
 	 	 	 	 	 }  
 	 	 	 	 	 e l s e   i f   ( n a m e   = =   " K e y b o a r d A r r o w s " )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 s t r i n g   e l e m e n t   =   r e a d e r . R e a d E l e m e n t S t r i n g ( ) ;  
 	 	 	 	 	 	 t h i s . k e y b o a r d A r r o w s   =   ( e l e m e n t   = =   " Y " ) ;  
 	 	 	 	 	 	 t h i s . U p d a t e K e y b o a r d ( ) ;  
 	 	 	 	 	 	 r e a d e r . R e a d ( ) ;  
 	 	 	 	 	 }  
 	 	 	 	 	 e l s e   i f   ( n a m e   = =   " M e m o r y D a t a " )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 s t r i n g   e l e m e n t   =   r e a d e r . R e a d E l e m e n t S t r i n g ( ) ;  
 	 	 	 	 	 	 t h i s . m e m o r y . P u t C o n t e n t ( e l e m e n t ) ;  
 	 	 	 	 	 	 r e a d e r . R e a d ( ) ;  
 	 	 	 	 	 }  
 	 	 	 	 	 e l s e   i f   ( n a m e   = =   " R e m " )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 s t r i n g   e l e m e n t   =   r e a d e r . R e a d E l e m e n t S t r i n g ( ) ;  
 	 	 	 	 	 	 t h i s . f i e l d P r o g r a m R e m . T e x t   =   e l e m e n t ;  
 	 	 	 	 	 	 r e a d e r . R e a d ( ) ;  
 	 	 	 	 	 }  
 	 	 	 	 	 e l s e   i f   ( n a m e   = =   " S o u r c e " )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 s t r i n g   e l e m e n t   =   r e a d e r . R e a d E l e m e n t S t r i n g ( ) ;  
 	 	 	 	 	 	 t h i s . c a l m E d i t o r . T e x t   =   e l e m e n t ;  
 	 	 	 	 	 	 r e a d e r . R e a d ( ) ;  
 	 	 	 	 	 }  
 	 	 	 	 	 e l s e  
 	 	 	 	 	 {  
 	 	 	 	 	 	 r e a d e r . R e a d ( ) ;  
 	 	 	 	 	 }  
 	 	 	 	 }  
 	 	 	 	 e l s e   i f   ( r e a d e r . N o d e T y p e   = =   X m l N o d e T y p e . E n d E l e m e n t )  
 	 	 	 	 {  
 	 	 	 	 	 i f   ( r e a d e r . N a m e   = =   " D o l p h i n " )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 r e t u r n   n u l l ;     / /   o k  
 	 	 	 	 	 }  
 	 	 	 	 	 e l s e  
 	 	 	 	 	 {  
 	 	 	 	 	 	 r e t u r n   R e s . S t r i n g s . E r r o r . R e a d . F o r m a t ;  
 	 	 	 	 	 }  
 	 	 	 	 }  
 	 	 	 	 e l s e  
 	 	 	 	 {  
 	 	 	 	 	 r e a d e r . R e a d ( ) ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
 	 	 # e n d r e g i o n  
  
 	 	 # r e g i o n   S o u r c e   S e r i a l i z a t i o n  
 	 	 p r o t e c t e d   v o i d   C a l m O p e n ( )  
 	 	 {  
 	 	 	 / / 	 O u v r e   u n   p r o g r a m m e   s o u r c e   C A L M .  
 	 	 	 i f   ( t h i s . D l g O p e n C a l m ( ) )  
 	 	 	 {  
 	 	 	 	 s t r i n g   d a t a   =   n u l l ;  
 	 	 	 	 s t r i n g   e r r   =   n u l l ;  
 	 	 	 	 t r y  
 	 	 	 	 {  
 	 	 	 	 	 d a t a   =   S y s t e m . I O . F i l e . R e a d A l l T e x t ( t h i s . f i l e n a m e C a l m ) ;  
 	 	 	 	 }  
 	 	 	 	 c a t c h  
 	 	 	 	 {  
 	 	 	 	 	 d a t a   =   n u l l ;  
 	 	 	 	 	 t h i s . f i l e n a m e C a l m   =   n u l l ;  
 	 	 	 	 	 e r r   =   R e s . S t r i n g s . E r r o r . O p e n ;  
 	 	 	 	 }  
  
 	 	 	 	 i f   ( d a t a   = =   n u l l )  
 	 	 	 	 {  
 	 	 	 	 	 s t r i n g   t i t l e   =   T e x t L a y o u t . C o n v e r t T o S i m p l e T e x t ( R e s . S t r i n g s . W i n d o w . T i t l e ) ;  
 	 	 	 	 	 s t r i n g   i c o n   =   " m a n i f e s t : E p s i t e c . C o m m o n . D i a l o g s . I m a g e s . W a r n i n g . i c o n " ;  
 	 	 	 	 	 C o m m o n . D i a l o g s . I D i a l o g   d i a l o g   =   C o m m o n . D i a l o g s . M e s s a g e D i a l o g . C r e a t e O k ( t i t l e ,   i c o n ,   e r r ,   n u l l ,   n u l l ) ;  
 	 	 	 	 	 d i a l o g . O w n e r W i n d o w   =   t h i s . W i n d o w ;  
 	 	 	 	 	 d i a l o g . O p e n D i a l o g ( ) ;  
 	 	 	 	 }  
 	 	 	 	 e l s e  
 	 	 	 	 {  
 	 	 	 	 	 d a t a   =   d a t a . R e p l a c e ( " \ r \ n " ,   " \ n " ) ;  
 	 	 	 	 	 t h i s . c a l m E d i t o r . T e x t   =   T e x t L a y o u t . C o n v e r t T o T a g g e d T e x t ( d a t a ) ;  
 	 	 	 	 	 t h i s . c a l m E d i t o r . C u r s o r   =   0 ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   C a l m S a v e ( )  
 	 	 {  
 	 	 	 / / 	 E n r e g i s t r e   u n   p r o g r a m m e   s o u r c e   C A L M .  
 	 	 	 i f   ( t h i s . D l g S a v e C a l m ( ) )  
 	 	 	 {  
 	 	 	 	 s t r i n g   d a t a   =   T e x t L a y o u t . C o n v e r t T o S i m p l e T e x t ( t h i s . c a l m E d i t o r . T e x t ) ;  
 	 	 	 	 d a t a   =   d a t a . R e p l a c e ( " \ n " ,   " \ r \ n " ) ;  
 	 	 	 	 s t r i n g   e r r   =   n u l l ;  
 	 	 	 	 t r y  
 	 	 	 	 {  
 	 	 	 	 	 S y s t e m . I O . F i l e . W r i t e A l l T e x t ( t h i s . f i l e n a m e C a l m ,   d a t a ) ;  
 	 	 	 	 }  
 	 	 	 	 c a t c h  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . f i l e n a m e C a l m   =   n u l l ;  
 	 	 	 	 	 e r r   =   R e s . S t r i n g s . E r r o r . S a v e ;  
 	 	 	 	 }  
  
 	 	 	 	 i f   ( e r r   ! =   n u l l )  
 	 	 	 	 {  
 	 	 	 	 	 s t r i n g   t i t l e   =   T e x t L a y o u t . C o n v e r t T o S i m p l e T e x t ( R e s . S t r i n g s . W i n d o w . T i t l e ) ;  
 	 	 	 	 	 s t r i n g   i c o n   =   " m a n i f e s t : E p s i t e c . C o m m o n . D i a l o g s . I m a g e s . W a r n i n g . i c o n " ;  
 	 	 	 	 	 C o m m o n . D i a l o g s . I D i a l o g   d i a l o g   =   C o m m o n . D i a l o g s . M e s s a g e D i a l o g . C r e a t e O k ( t i t l e ,   i c o n ,   e r r ,   n u l l ,   n u l l ) ;  
 	 	 	 	 	 d i a l o g . O w n e r W i n d o w   =   t h i s . W i n d o w ;  
 	 	 	 	 	 d i a l o g . O p e n D i a l o g ( ) ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   b o o l   D l g O p e n C a l m ( )  
 	 	 {  
 	 	 	 / / 	 D e m a n d e   l e   n o m   d u   f i c h i e r   à   o u v r i r .  
 	 	 	 C o m m o n . D i a l o g s . F i l e O p e n   d l g   =   n e w   C o m m o n . D i a l o g s . F i l e O p e n ( ) ;  
 	 	 	 d l g . T i t l e   =   T e x t L a y o u t . C o n v e r t T o S i m p l e T e x t ( R e s . S t r i n g s . D i a l o g . O p e n C a l m . T i t l e ) ;  
  
 	 	 	 i f   ( t h i s . f i r s t O p e n S a v e D i a l o g )  
 	 	 	 {  
 	 	 	 	 d l g . I n i t i a l D i r e c t o r y   =   t h i s . U s e r S a m p l e s P a t h ;  
 	 	 	 	 d l g . F i l e N a m e   =   " " ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 d l g . F i l e N a m e   =   " " ;  
 	 	 	 }  
  
 	 	 	 d l g . F i l t e r s . A d d ( " t x t " ,   R e s . S t r i n g s . D i a l o g . F i l e . P r o g r a m s ,   " * . t x t " ) ;  
 	 	 	 d l g . O w n e r   =   t h i s . W i n d o w ;  
 	 	 	 d l g . O p e n D i a l o g ( ) ;     / /   a f f i c h e   l e   d i a l o g u e . . .  
  
 	 	 	 i f   ( d l g . R e s u l t   ! =   C o m m o n . D i a l o g s . D i a l o g R e s u l t . A c c e p t )  
 	 	 	 {  
 	 	 	 	 r e t u r n   f a l s e ;  
 	 	 	 }  
  
 	 	 	 t h i s . f i l e n a m e C a l m   =   d l g . F i l e N a m e ;  
 	 	 	 r e t u r n   t r u e ;  
 	 	 }  
  
 	 	 p r o t e c t e d   b o o l   D l g S a v e C a l m ( )  
 	 	 {  
 	 	 	 / / 	 D e m a n d e   l e   n o m   d u   f i c h i e r   à   e n r e g i s t r e r .  
 	 	 	 C o m m o n . D i a l o g s . F i l e S a v e   d l g   =   n e w   C o m m o n . D i a l o g s . F i l e S a v e ( ) ;  
 	 	 	 d l g . P r o m p t F o r O v e r w r i t i n g   =   t r u e ;  
 	 	 	 d l g . T i t l e   =   T e x t L a y o u t . C o n v e r t T o S i m p l e T e x t ( R e s . S t r i n g s . D i a l o g . S a v e C a l m . T i t l e ) ;  
  
 	 	 	 i f   ( t h i s . f i r s t O p e n S a v e D i a l o g   & &   s t r i n g . I s N u l l O r E m p t y ( t h i s . f i l e n a m e C a l m ) )  
 	 	 	 {  
 	 	 	 	 d l g . I n i t i a l D i r e c t o r y   =   t h i s . U s e r S a m p l e s P a t h ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 d l g . F i l e N a m e   =   t h i s . f i l e n a m e C a l m ;  
 	 	 	 }  
  
 	 	 	 d l g . F i l t e r s . A d d ( " t x t " ,   R e s . S t r i n g s . D i a l o g . F i l e . P r o g r a m s ,   " * . t x t " ) ;  
 	 	 	 d l g . O w n e r W i n d o w   =   t h i s . W i n d o w ;  
 	 	 	 d l g . O p e n D i a l o g ( ) ;     / /   a f f i c h e   l e   d i a l o g u e . . .  
  
 	 	 	 i f   ( d l g . D i a l o g R e s u l t   ! =   C o m m o n . D i a l o g s . D i a l o g R e s u l t . A c c e p t )  
 	 	 	 {  
 	 	 	 	 r e t u r n   f a l s e ;  
 	 	 	 }  
  
 	 	 	 t h i s . f i l e n a m e C a l m   =   d l g . F i l e N a m e ;  
 	 	 	 r e t u r n   t r u e ;  
 	 	 }  
 	 	 # e n d r e g i o n  
  
 	 	 # r e g i o n   C h e c k  
 	 	 p r o t e c t e d   v o i d   S t a r t C h e c k ( )  
 	 	 {  
 	 	 	 / / 	 L a n c e   l e   p r o c e s s u s   a s y n c h r o n e   q u i   v a   s e   c o n n e c t e r   a u   s i t e   w e b  
 	 	 	 / / 	 e t   r e g a r d e r   s ' i l   y   a   u n e   v e r s i o n   p l u s   r é c e n t e .  
 	 	 	 / / 	 C h a q u e   e x é c u t i o n   i n c r é m e n t e   l e   c o m p t e u r   ' / c o u n t e r / c h e c k / D a u p h i n '   d a n s   l a   b a s e   M y S Q L .  
 	 	 	 t h i s . c h e c k e r   =   n e w   V e r s i o n C h e c k e r ( t y p e o f   ( A p p . D o l p h i n . D o l p h i n A p p l i c a t i o n ) . A s s e m b l y ) ;  
 	 	 	 s t r i n g   u r l   =   s t r i n g . C o n c a t ( " h t t p : / / w w w . e p s i t e c . c h / d y n a m i c s / c h e c k . p h p ? s o f t w a r e = " ,   R e s . S t r i n g s . P a t h . A p p l i c a t i o n N a m e ,   " & v e r s i o n = " ,   t h i s . c h e c k e r . C u r r e n t V e r s i o n ) ;  
 	 	 	 t h i s . c h e c k e r . S t a r t C h e c k ( u r l ) ;  
 	 	 }  
  
 	 	 p r o t e c t e d   v o i d   E n d C h e c k ( )  
 	 	 {  
 	 	 	 / / 	 A t t e n d   l a   f i n   d u   p r o c e s s u s   d e   c h e c k   e t   i n d i q u e   s i   u n e   m i s e   à   j o u r   e s t  
 	 	 	 / / 	 d i s p o n i b l e .  
 	 	 	 i f   (   t h i s . c h e c k e r   = =   n u l l   )     r e t u r n ;  
  
 	 	 	 S y s t e m . T h r e a d i n g . T h r e a d   t h r e a d   =   n e w   S y s t e m . T h r e a d i n g . T h r e a d ( t h i s . C h e c k T h r e a d ) ;  
 	 	 	 t h r e a d . N a m e   =   " V e r s i o n C h e c k " ;  
 	 	 	 t h r e a d . S t a r t ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   C h e c k T h r e a d ( )  
 	 	 {  
 	 	 	 / / 	 V é r i f i e   s ' i l   y   a   u n e   n o u v e l l e   v e r s i o n ,   m a i s   l a i s s e   d ' a b o r d   l e   t e m p s  
 	 	 	 / / 	 à   l ' a p p l i c a t i o n   d e   d é m a r r e r   !  
  
 	 	 	 S y s t e m . T h r e a d i n g . T h r e a d . S l e e p ( 1 0 0 0 ) ;  
  
 	 	 	 w h i l e   (   ! t h i s . c h e c k e r . I s R e a d y   )  
 	 	 	 {  
 	 	 	 	 S y s t e m . T h r e a d i n g . T h r e a d . S l e e p ( 1 0 0 ) ;     / /   a t t e n d   0 . 1 s  
 	 	 	 }  
  
 	 	 	 A p p l i c a t i o n . Q u e u e A s y n c C a l l b a c k (  
 	 	 	 	 d e l e g a t e ( )  
 	 	 	 	 {  
 	 	 	 	 	 i f   ( t h i s . c h e c k e r . F o u n d N e w e r V e r s i o n )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 s t r i n g   v e r s i o n   =   t h i s . c h e c k e r . N e w e r V e r s i o n ;  
 	 	 	 	 	 	 s t r i n g   u r l   =   t h i s . c h e c k e r . N e w e r V e r s i o n U r l ;  
  
 	 	 	 	 	 	 D i a l o g s . D o w n l o a d   d l g   =   n e w   D i a l o g s . D o w n l o a d ( t h i s ) ;  
 	 	 	 	 	 	 d l g . S e t I n f o ( v e r s i o n ,   u r l ) ;  
 	 	 	 	 	 	 d l g . S h o w ( ) ;  
 	 	 	 	 	 }  
  
 	 	 	 	 	 t h i s . c h e c k e r   =   n u l l ;  
 	 	 	 	 } ) ;  
 	 	 }  
 	 	 # e n d r e g i o n  
  
 	 	 # r e g i o n   E v e n t   h a n d l e r  
 	 	 p r i v a t e   v o i d   H a n d l e B u t t o n N e w C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 / / 	 B o u t o n   o u v r i r   c l i q u é .  
 	 	 	 t h i s . N e w ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e B u t t o n O p e n C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 / / 	 B o u t o n   o u v r i r   c l i q u é .  
 	 	 	 i f   ( t h i s . A u t o S a v e ( )   & &   t h i s . D l g O p e n F i l e n a m e ( ) )  
 	 	 	 {  
 	 	 	 	 s t r i n g   e r r   =   t h i s . O p e n ( ) ;  
 	 	 	 	 i f   ( s t r i n g . I s N u l l O r E m p t y ( e r r ) )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . f i r s t O p e n S a v e D i a l o g   =   f a l s e ;  
 	 	 	 	 }  
 	 	 	 	 e l s e  
 	 	 	 	 {  
 	 	 	 	 	 s t r i n g   t i t l e   =   T e x t L a y o u t . C o n v e r t T o S i m p l e T e x t ( R e s . S t r i n g s . W i n d o w . T i t l e ) ;  
 	 	 	 	 	 s t r i n g   i c o n   =   " m a n i f e s t : E p s i t e c . C o m m o n . D i a l o g s . I m a g e s . W a r n i n g . i c o n " ;  
 	 	 	 	 	 C o m m o n . D i a l o g s . I D i a l o g   d i a l o g   =   C o m m o n . D i a l o g s . M e s s a g e D i a l o g . C r e a t e O k ( t i t l e ,   i c o n ,   e r r ,   n u l l ,   n u l l ) ;  
 	 	 	 	 	 d i a l o g . O w n e r W i n d o w   =   t h i s . W i n d o w ;  
 	 	 	 	 	 d i a l o g . O p e n D i a l o g ( ) ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e B u t t o n S a v e C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 / / 	 B o u t o n   e n r e g i s t r e r   c l i q u é .  
 	 	 	 i f   ( t h i s . D l g S a v e F i l e n a m e ( ) )  
 	 	 	 {  
 	 	 	 	 i f   ( t h i s . S a v e ( ) )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . f i r s t O p e n S a v e D i a l o g   =   f a l s e ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e B u t t o n L o o k C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 / / 	 B o u t o n   " a u t r e   l o o k "   c l i q u é .  
 	 	 	 s w i t c h   ( D o l p h i n A p p l i c a t i o n . l o o k )  
 	 	 	 {  
 	 	 	 	 c a s e   L o o k . G r a y A n d R e d :  
 	 	 	 	 	 D o l p h i n A p p l i c a t i o n . l o o k   =   L o o k . V i s t a ;  
 	 	 	 	 	 E p s i t e c . C o m m o n . W i d g e t s . A d o r n e r s . F a c t o r y . S e t A c t i v e ( " L o o k R o y a l e " ) ;  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   L o o k . V i s t a :  
 	 	 	 	 	 D o l p h i n A p p l i c a t i o n . l o o k   =   L o o k . M e t a l ;  
 	 	 	 	 	 E p s i t e c . C o m m o n . W i d g e t s . A d o r n e r s . F a c t o r y . S e t A c t i v e ( " L o o k M e t a l " ) ;  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   L o o k . M e t a l :  
 	 	 	 	 	 D o l p h i n A p p l i c a t i o n . l o o k   =   L o o k . G r a y A n d R e d ;  
 	 	 	 	 	 E p s i t e c . C o m m o n . W i d g e t s . A d o r n e r s . F a c t o r y . S e t A c t i v e ( " L o o k S i m p l y " ) ;  
 	 	 	 	 	 b r e a k ;  
 	 	 	 }  
  
 	 	 	 t h i s . m a i n P a n e l . I n v a l i d a t e ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e B u t t o n A b o u t C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 / / 	 B o u t o n   " à   p r o p o s   d e "   c l i q u é .  
 	 	 	 D i a l o g s . A b o u t   d l g   =   n e w   D i a l o g s . A b o u t ( t h i s ) ;  
 	 	 	 d l g . S h o w ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e B u t t o n M o d e C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 / / 	 B o u t o n   p o u r   c h o i s i r   l e   m o d e   d e s   p a n n e a u x   c l i q u é .  
 	 	 	 M y W i d g e t s . P u s h B u t t o n   b u t t o n   =   s e n d e r   a s   M y W i d g e t s . P u s h B u t t o n ;  
 	 	 	 t h i s . p a n e l M o d e   =   b u t t o n . N a m e ;  
  
 	 	 	 t h i s . U p d a t e P a n e l M o d e ( ) ;  
 	 	 	 t h i s . U p d a t e D a t a ( ) ;  
 	 	 	 t h i s . P r o c e s s o r F e e d b a c k ( ) ;  
 	 	 	 t h i s . M a r k P C ( t h i s . p r o c e s s o r . G e t R e g i s t e r V a l u e ( " P C " ) ,   f a l s e ) ;  
 	 	 	 t h i s . D i r t y   =   t r u e ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e C l o c k T i m e E l a p s e d ( o b j e c t   s e n d e r )  
 	 	 {  
 	 	 	 / / 	 L e   t i m e r   d e m a n d e   d ' e x é c u t e r   l ' i n s t r u c t i o n   s u i v a n t e .  
 	 	 	 t h i s . c l o c k . S t a r t ( ) ;     / /   r e d é m a r r e   l e   t i m e r  
  
 	 	 	 i f   ( t h i s . i p s   >   D o l p h i n A p p l i c a t i o n . R e a l M a x I p s )  
 	 	 	 {  
 	 	 	 	 / / 	 L o r s q u ' o n   e x é c u t e   p l u s i e u r s   P r o c e s s o r C l o c k   p a r   H a n d l e C l o c k T i m e E l a p s e d ,   i l   e s t   i m p o r t a n t  
 	 	 	 	 / / 	 d e   d i f f é r e r   M e m o r y A c c e s s o r . U p d a t e D a t a   e t   C o d e A c c e s s o r . U p d a t e D a t a .   S a n s   c e l a ,   u n   é n o r m e  
 	 	 	 	 / / 	 r a l e n t i s s e m e n t   a   l i e u   l o r s q u ' u n e   i n s t r u c t i o n   é c r i t   e n   m é m o i r e .  
 	 	 	 	 t h i s . m e m o r y A c c e s s o r . I s D e f e r U p d a t e D a t a   =   t r u e ;  
 	 	 	 	 t h i s . c o d e A c c e s s o r . I s D e f e r U p d a t e D a t a   =   t r u e ;  
  
 	 	 	 	 i n t   c o u n t   =   ( i n t )   ( t h i s . i p s / D o l p h i n A p p l i c a t i o n . R e a l M a x I p s ) ;  
 	 	 	 	 f o r   ( i n t   i = 0 ;   i < c o u n t ;   i + + )  
 	 	 	 	 {  
 	 	 	 	 	 i f   ( t h i s . P r o c e s s o r C l o c k ( ) )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 t h i s . m e m o r y A c c e s s o r . I s D e f e r U p d a t e D a t a   =   f a l s e ;  
 	 	 	 	 	 	 t h i s . c o d e A c c e s s o r . I s D e f e r U p d a t e D a t a   =   f a l s e ;  
  
 	 	 	 	 	 	 t h i s . P r o c e s s o r B r e a k O u t ( ) ;  
 	 	 	 	 	 	 r e t u r n ;  
 	 	 	 	 	 }  
 	 	 	 	 }  
  
 	 	 	 	 t h i s . m e m o r y A c c e s s o r . I s D e f e r U p d a t e D a t a   =   f a l s e ;  
 	 	 	 	 t h i s . c o d e A c c e s s o r . I s D e f e r U p d a t e D a t a   =   f a l s e ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 i f   ( t h i s . P r o c e s s o r C l o c k ( ) )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . P r o c e s s o r B r e a k O u t ( ) ;  
 	 	 	 	 	 r e t u r n ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 t h i s . P r o c e s s o r F e e d b a c k ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e B u t t o n R u n C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 / / 	 B o u t o n   [ R / S ]   c l i q u é .  
 	 	 	 i f   ( t h i s . b u t t o n R u n . A c t i v e S t a t e   = =   A c t i v e S t a t e . N o )  
 	 	 	 {  
 	 	 	 	 i f   ( t h i s . d i r t y C a l m )     / /   s o u r c e   C A L M   m o d i f i é   ?  
 	 	 	 	 {  
 	 	 	 	 	 i f   ( t h i s . Q u e s t i o n Y e s N o ( R e s . S t r i n g s . Q u e s t i o n . A s s e m b l e r . D o ) )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 i f   ( t h i s . a s s e m b l e r . A c t i o n ( t h i s . c a l m E d i t o r ,   t h i s . W i n d o w ,   f a l s e ) )  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 t h i s . d i r t y C a l m   =   f a l s e ;  
 	 	 	 	 	 	 }  
 	 	 	 	 	 	 e l s e  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 r e t u r n ;  
 	 	 	 	 	 	 }  
 	 	 	 	 	 }  
 	 	 	 	 }  
  
 	 	 	 	 t h i s . P r o c e s s o r R e s e t ( ) ;  
 	 	 	 	 t h i s . P r o c e s s o r S t a r t ( ) ;  
 	 	 	 	  
 	 	 	 	 t h i s . b u t t o n R u n . A c t i v e S t a t e   =   A c t i v e S t a t e . Y e s ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . P r o c e s s o r S t o p ( ) ;  
  
 	 	 	 	 t h i s . A d d r e s s B i t s   =   t h i s . A d d r e s s B i t s ;  
 	 	 	 	 t h i s . D a t a B i t s   =   0 ;  
 	 	 	 	  
 	 	 	 	 t h i s . b u t t o n R u n . A c t i v e S t a t e   =   A c t i v e S t a t e . N o ;  
 	 	 	 }  
  
 	 	 	 t h i s . U p d a t e B u t t o n s ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e B u t t o n R e s e t C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 / / 	 B o u t o n   [ R E S E T ]   c l i q u é .  
 	 	 	 t h i s . P r o c e s s o r R e s e t ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e B u t t o n S t e p C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 / / 	 B o u t o n   [ S ]   c l i q u é .  
 	 	 	 i f   ( t h i s . s w i t c h I n t o . A c t i v e S t a t e   = =   A c t i v e S t a t e . Y e s )     / /   i n t o   ?  
 	 	 	 {  
 	 	 	 	 t h i s . P r o c e s s o r C l o c k ( ) ;  
 	 	 	 }  
 	 	 	 e l s e     / /   o v e r   ?  
 	 	 	 {  
 	 	 	 	 i n t   r e t A d d r e s s ;  
 	 	 	 	 i f   ( t h i s . p r o c e s s o r . I s C a l l ( o u t   r e t A d d r e s s ) )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . P r o c e s s o r B r e a k I n ( r e t A d d r e s s ) ;  
 	 	 	 	 }  
 	 	 	 	 e l s e  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . P r o c e s s o r C l o c k ( ) ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 t h i s . P r o c e s s o r F e e d b a c k ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e B u t t o n C l o c k C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 / / 	 B o u t o n   p o u r   c h o i s i r   l a   f r é q u e n c e   d ' h o r l o g e   c l i q u é .  
 	 	 	 M y W i d g e t s . P u s h B u t t o n   b u t t o n   =   s e n d e r   a s   M y W i d g e t s . P u s h B u t t o n ;  
  
 	 	 	 t h i s . i p s   =   b u t t o n . I n d e x ;  
 	 	 	 t h i s . U p d a t e C l o c k B u t t o n s ( ) ;  
 	 	 	 t h i s . P r o c e s s o r C l o c k A d j u s t ( ) ;  
  
 	 	 	 t h i s . D i r t y   =   t r u e ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e S w i t c h S t e p C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 / / 	 S w i t c h   C O N T / S T E P   b a s c u l é .  
 	 	 	 t h i s . s w i t c h S t e p . A c t i v e S t a t e   =   ( t h i s . s w i t c h S t e p . A c t i v e S t a t e   = =   A c t i v e S t a t e . N o )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 t h i s . U p d a t e B u t t o n s ( ) ;  
  
 	 	 	 i f   ( t h i s . b u t t o n R u n . A c t i v e S t a t e   = =   A c t i v e S t a t e . Y e s )  
 	 	 	 {  
 	 	 	 	 i f   ( t h i s . s w i t c h S t e p . A c t i v e S t a t e   = =   A c t i v e S t a t e . N o )     / /   C o n t i n u o u s   ?  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . P r o c e s s o r S t a r t ( ) ;  
 	 	 	 	 }  
 	 	 	 	 e l s e     / /   S t e p   ?  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . P r o c e s s o r S t o p ( ) ;  
 	 	 	 	 	 t h i s . P r o c e s s o r F e e d b a c k ( ) ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 t h i s . D i r t y   =   t r u e ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e S w i t c h I n t o C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 / / 	 S w i t c h   O V E R / I N T O   b a s c u l é .  
 	 	 	 t h i s . s w i t c h I n t o . A c t i v e S t a t e   =   ( t h i s . s w i t c h I n t o . A c t i v e S t a t e   = =   A c t i v e S t a t e . N o )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 t h i s . D i r t y   =   t r u e ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e B u t t o n M e m o r y P r e s s e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 / / 	 B o u t o n   [ R ] / [ W ]   d ' a c c è s   à   l a   m é m o i r e   p r e s s é .  
 	 	 	 i f   ( t h i s . b u t t o n R u n . A c t i v e S t a t e   = =   A c t i v e S t a t e . Y e s )  
 	 	 	 {  
 	 	 	 	 r e t u r n ;  
 	 	 	 }  
  
 	 	 	 i f   ( s e n d e r   = =   t h i s . b u t t o n M e m o r y R e a d )     / /   r e a d   ?  
 	 	 	 {  
 	 	 	 	 t h i s . D a t a B i t s   =   t h i s . m e m o r y . R e a d ( t h i s . A d d r e s s B i t s ) ;  
 	 	 	 }  
  
 	 	 	 i f   ( s e n d e r   = =   t h i s . b u t t o n M e m o r y W r i t e )     / /   w r i t e   ?  
 	 	 	 {  
 	 	 	 	 t h i s . m e m o r y . W r i t e W i t h D i r t y ( t h i s . A d d r e s s B i t s ,   t h i s . D a t a B i t s ) ;  
 	 	 	 	 t h i s . D a t a B i t s   =   t h i s . m e m o r y . R e a d ( t h i s . A d d r e s s B i t s ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e B u t t o n M e m o r y R e l e a s e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 / / 	 B o u t o n   [ M ]   r e l â c h é .  
 	 	 	 i f   ( t h i s . b u t t o n R u n . A c t i v e S t a t e   = =   A c t i v e S t a t e . Y e s )  
 	 	 	 {  
 	 	 	 	 r e t u r n ;  
 	 	 	 }  
  
 	 	 	 t h i s . D a t a B i t s   =   0 ;  
  
 	 	 	 / / 	 N é c e s s a i r e   m ê m e   e n   l e c t u r e ,   c a r   l a   l e c t u r e   d u   c l a v i e r   c l e a r   l e   b i t   f u l l   !  
 	 	 	 t h i s . U p d a t e D a t a ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e A d d r e s s S w i t c h C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 / / 	 S w i t c h   d ' a d r e s s e   b a s c u l é .  
 	 	 	 i f   ( t h i s . b u t t o n R u n . A c t i v e S t a t e   = =   A c t i v e S t a t e . Y e s )  
 	 	 	 {  
 	 	 	 	 r e t u r n ;  
 	 	 	 }  
  
 	 	 	 M y W i d g e t s . S w i t c h   b u t t o n   =   s e n d e r   a s   M y W i d g e t s . S w i t c h ;  
  
 	 	 	 i f   ( b u t t o n . A c t i v e S t a t e   = =   A c t i v e S t a t e . N o )  
 	 	 	 {  
 	 	 	 	 b u t t o n . A c t i v e S t a t e   =   A c t i v e S t a t e . Y e s ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 b u t t o n . A c t i v e S t a t e   =   A c t i v e S t a t e . N o ;  
 	 	 	 }  
  
 	 	 	 t h i s . A d d r e s s B i t s   =   t h i s . A d d r e s s B i t s ;     / /   a l l u m e   l e s   l e d s   s e l o n   l e s   s w i t c h s  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e D a t a S w i t c h C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 / / 	 S w i t c h   d e   d a t a   b a s c u l é .  
 	 	 	 i f   ( t h i s . b u t t o n R u n . A c t i v e S t a t e   = =   A c t i v e S t a t e . Y e s )  
 	 	 	 {  
 	 	 	 	 r e t u r n ;  
 	 	 	 }  
  
 	 	 	 M y W i d g e t s . S w i t c h   b u t t o n   =   s e n d e r   a s   M y W i d g e t s . S w i t c h ;  
  
 	 	 	 i f   ( b u t t o n . A c t i v e S t a t e   = =   A c t i v e S t a t e . N o )  
 	 	 	 {  
 	 	 	 	 b u t t o n . A c t i v e S t a t e   =   A c t i v e S t a t e . Y e s ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 b u t t o n . A c t i v e S t a t e   =   A c t i v e S t a t e . N o ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e P r o c e s s o r R e g i s t e r C h a n g e d ( o b j e c t   s e n d e r )  
 	 	 {  
 	 	 	 / / 	 L a   v a l e u r   d ' u n   r e g i s t r e   d u   p r o c e s s e u r   a   é t é   c h a n g é e .  
 	 	 	 i f   ( t h i s . i g n o r e C h a n g e )  
 	 	 	 {  
 	 	 	 	 r e t u r n ;  
 	 	 	 }  
  
 	 	 	 T e x t F i e l d   f i e l d   =   s e n d e r   a s   T e x t F i e l d ;  
 	 	 	 i n t   v a l u e   =   M i s c . P a r s e H e x a ( f i e l d . T e x t ) ;  
 	 	 	 t h i s . p r o c e s s o r . S e t R e g i s t e r V a l u e ( f i e l d . N a m e ,   v a l u e ) ;  
  
 	 	 	 i f   ( f i e l d . N a m e   = =   " P C " )  
 	 	 	 {  
 	 	 	 	 i n t   p c   =   t h i s . p r o c e s s o r . G e t R e g i s t e r V a l u e ( " P C " ) ;  
 	 	 	 	 t h i s . M a r k P C ( p c ,   f a l s e ) ;  
 	 	 	 	 t h i s . U p d a t e M e m o r y B a n k ( ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e P r o c e s s o r H e x a V a l u e C h a n g e d ( o b j e c t   s e n d e r )  
 	 	 {  
 	 	 	 / / 	 L a   v a l e u r   d ' u n   r e g i s t r e   d u   p r o c e s s e u r   a   é t é   c h a n g é e .  
 	 	 	 i f   ( t h i s . i g n o r e C h a n g e )  
 	 	 	 {  
 	 	 	 	 r e t u r n ;  
 	 	 	 }  
  
 	 	 	 M y W i d g e t s . T e x t F i e l d H e x a   f i e l d   =   s e n d e r   a s   M y W i d g e t s . T e x t F i e l d H e x a ;  
 	 	 	 t h i s . p r o c e s s o r . S e t R e g i s t e r V a l u e ( f i e l d . N a m e ,   f i e l d . H e x a V a l u e ) ;  
  
 	 	 	 i f   ( f i e l d . N a m e   = =   " P C " )  
 	 	 	 {  
 	 	 	 	 i n t   p c   =   t h i s . p r o c e s s o r . G e t R e g i s t e r V a l u e ( " P C " ) ;  
 	 	 	 	 t h i s . M a r k P C ( p c ,   f a l s e ) ;  
 	 	 	 	 t h i s . U p d a t e M e m o r y B a n k ( ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e M e m o r y B u t t o n P C C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . m e m o r y A c c e s s o r . F o l l o w P C   =   ! t h i s . m e m o r y A c c e s s o r . F o l l o w P C ;  
  
 	 	 	 i f   ( t h i s . m e m o r y A c c e s s o r . F o l l o w P C )  
 	 	 	 {  
 	 	 	 	 t h i s . M a r k P C ( t h i s . p r o c e s s o r . G e t R e g i s t e r V a l u e ( " P C " ) ,   t r u e ) ;  
 	 	 	 }  
  
 	 	 	 t h i s . U p d a t e M e m o r y B a n k ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e M e m o r y B u t t o n C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 / / 	 B o u t o n   [ R A M ] ,   [ R O M ]   o u   [ P E R ]   c l i q u é .  
 	 	 	 M y W i d g e t s . P u s h B u t t o n   b u t t o n   =   s e n d e r   a s   M y W i d g e t s . P u s h B u t t o n ;  
 	 	 	 t h i s . m e m o r y A c c e s s o r . B a n k   =   b u t t o n . N a m e ;  
 	 	 	 t h i s . U p d a t e M e m o r y B a n k ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e C o d e A c c e s s o r I n s t r u c t i o n S e l e c t e d ( o b j e c t   s e n d e r )  
 	 	 {  
 	 	 	 / / 	 L ' i n s t r u c t i o n   s é l e c t i o n n é e   d a n s   l e   p a n n e a u   C o d e   a   c h a n g é .  
 	 	 	 i n t   a d d r e s s   =   t h i s . c o d e A c c e s s o r . A d d r e s s S e l e c t e d ;  
 	 	 	 b o o l   i s R e a d O n l y   =   t h i s . m e m o r y . I s R e a d O n l y ( t h i s . c o d e A c c e s s o r . M e m o r y S t a r t ) ;  
  
 	 	 	 t h i s . c o d e B u t t o n A d d . E n a b l e   =   ( a d d r e s s   ! =   M i s c . u n d e f i n e d   & &   ! i s R e a d O n l y ) ;  
 	 	 	 t h i s . c o d e B u t t o n S u b . E n a b l e   =   ( a d d r e s s   ! =   M i s c . u n d e f i n e d   & &   ! i s R e a d O n l y ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e C o d e A c c e s s o r B a n k C h a n g e d ( o b j e c t   s e n d e r )  
 	 	 {  
 	 	 	 t h i s . U p d a t e M e m o r y B a n k ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e C o d e B u t t o n P C C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . c o d e A c c e s s o r . F o l l o w P C   =   ! t h i s . c o d e A c c e s s o r . F o l l o w P C ;  
  
 	 	 	 i f   ( t h i s . c o d e A c c e s s o r . F o l l o w P C )  
 	 	 	 {  
 	 	 	 	 t h i s . M a r k P C ( t h i s . p r o c e s s o r . G e t R e g i s t e r V a l u e ( " P C " ) ,   t r u e ) ;  
 	 	 	 }  
  
 	 	 	 t h i s . U p d a t e M e m o r y B a n k ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e C o d e B u t t o n C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 / / 	 B o u t o n   [ R A M ] ,   [ R O M ]   o u   [ P E R ]   c l i q u é .  
 	 	 	 M y W i d g e t s . P u s h B u t t o n   b u t t o n   =   s e n d e r   a s   M y W i d g e t s . P u s h B u t t o n ;  
 	 	 	 t h i s . c o d e A c c e s s o r . B a n k   =   b u t t o n . N a m e ;  
 	 	 	 t h i s . U p d a t e M e m o r y B a n k ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e C o d e A d d B u t t o n C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 / / 	 B o u t o n   [ + ]   c l i q u é .  
 	 	 	 i n t   a d d r e s s   =   t h i s . c o d e A c c e s s o r . A d d r e s s S e l e c t e d ;  
 	 	 	 t h i s . m e m o r y . S h i f t R a m ( a d d r e s s ,   1 ) ;  
 	 	 	 t h i s . U p d a t e D a t a ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e C o d e S u b B u t t o n C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 / / 	 B o u t o n   [ - ]   c l i q u é .  
 	 	 	 i n t   a d d r e s s   =   t h i s . c o d e A c c e s s o r . A d d r e s s S e l e c t e d ;  
 	 	 	 i n t   l e n g t h   =   t h i s . c o d e A c c e s s o r . L e n g t h S e l e c t e d ;  
 	 	 	 t h i s . m e m o r y . S h i f t R a m ( a d d r e s s ,   - l e n g t h ) ;  
 	 	 	 t h i s . U p d a t e D a t a ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e C a l m O p e n C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 / / 	 B o u t o n   " o p e n   C A L M "   c l i q u é .  
 	 	 	 t h i s . C a l m O p e n ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e C a l m S a v e C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 / / 	 B o u t o n   " s a v e   C A L M "   c l i q u é .  
 	 	 	 t h i s . C a l m S a v e ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e C a l m S h o w C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 / / 	 B o u t o n   " s h o w   p a r r u s "   c l i q u é .  
 	 	 	 t h i s . c a l m E d i t o r . T e x t L a y o u t . S h o w L i n e B r e a k   =   ! t h i s . c a l m E d i t o r . T e x t L a y o u t . S h o w L i n e B r e a k ;  
 	 	 	 t h i s . c a l m E d i t o r . T e x t L a y o u t . S h o w T a b   =   ! t h i s . c a l m E d i t o r . T e x t L a y o u t . S h o w T a b ;  
 	 	 	 t h i s . c a l m E d i t o r . I n v a l i d a t e ( ) ;     / /   T O D O :   d e v r a i t   ê t r e   i n u t i l e ,   n o n   ?  
 	 	 	 t h i s . U p d a t e C a l m B u t t o n s ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e C a l m B i g C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 / / 	 B o u t o n   " g r a n d e   p o l i c e "   c l i q u é .  
 	 	 	 i f   ( t h i s . c a l m B u t t o n B i g . A c t i v e S t a t e   = =   A c t i v e S t a t e . N o )  
 	 	 	 {  
 	 	 	 	 t h i s . c a l m B u t t o n B i g . A c t i v e S t a t e   =   A c t i v e S t a t e . Y e s ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . c a l m B u t t o n B i g . A c t i v e S t a t e   =   A c t i v e S t a t e . N o ;  
 	 	 	 }  
  
 	 	 	 t h i s . U p d a t e C a l m E d i t o r ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e C a l m F u l l C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 / / 	 B o u t o n   " p l e i n   é c r a n "   c l i q u é .  
 	 	 	 i f   ( t h i s . c a l m B u t t o n F u l l . A c t i v e S t a t e   = =   A c t i v e S t a t e . N o )  
 	 	 	 {  
 	 	 	 	 t h i s . c a l m B u t t o n F u l l . A c t i v e S t a t e   =   A c t i v e S t a t e . Y e s ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . c a l m B u t t o n F u l l . A c t i v e S t a t e   =   A c t i v e S t a t e . N o ;  
 	 	 	 }  
  
 	 	 	 b o o l   f u l l   =   ( t h i s . c a l m B u t t o n F u l l . A c t i v e S t a t e   = =   A c t i v e S t a t e . Y e s ) ;  
  
 	 	 	 t h i s . p a n e l T i t l e . V i s i b i l i t y   =   ! f u l l ;  
 	 	 	 t h i s . r i g h t P a n e l . V i s i b i l i t y   =   ! f u l l ;  
 	 	 	 t h i s . l e f t H e a d e r . V i s i b i l i t y   =   ! f u l l ;  
 	 	 	 t h i s . t o p L e f t S e p . V i s i b i l i t y   =   ! f u l l ;  
 	 	 	 t h i s . l e f t C l o c k . V i s i b i l i t y   =   ! f u l l ;  
  
 	 	 	 t h i s . c a l m P a n e l . P r e f e r r e d H e i g h t   =   f u l l   ?   5 5 2   :   D o l p h i n A p p l i c a t i o n . P a n e l H e i g h t ;  
 	 	 	 t h i s . c a l m P a n e l . M i n W i d t h   =   f u l l   ?   7 5 2   :   D o l p h i n A p p l i c a t i o n . P a n e l W i d t h ;  
 	 	 	 t h i s . c a l m P a n e l . M a x W i d t h   =   f u l l   ?   7 5 2   :   D o l p h i n A p p l i c a t i o n . P a n e l W i d t h ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e C a l m E r r o r C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 / / 	 B o u t o n   [ E R R ]   c l i q u é ,   p o u r   c h e r c h e r   l ' e r r e u r   s u i v a n t e .  
 	 	 	 i n t   c u r s o r   =   t h i s . c a l m E d i t o r . C u r s o r ;  
 	 	 	 i n t   i n d e x   =   - 1 ;  
  
 	 	 	 s t r i n g   t e x t   =   T e x t L a y o u t . C o n v e r t T o S i m p l e T e x t ( t h i s . c a l m E d i t o r . T e x t ) ;  
 	 	 	 i f   ( c u r s o r + 1   <   t e x t . L e n g t h )  
 	 	 	 {  
 	 	 	 	 i n d e x   =   t e x t . I n d e x O f ( " ^   " ,   c u r s o r + 1 ) ;     / /   c h e r c h e   d e p u i s   l a   p o s i t i o n   d u   c u r s e u r  
 	 	 	 }  
  
 	 	 	 i f   ( i n d e x   = =   - 1   & &   c u r s o r   >   0 )  
 	 	 	 {  
 	 	 	 	 i n d e x   =   t e x t . I n d e x O f ( " ^   " ) ;     / /   c h e r c h e   d e p u i s   l e   d é b u t  
 	 	 	 }  
  
 	 	 	 i f   ( i n d e x   = =   - 1 )     / /   p a s   t r o u v é   ?  
 	 	 	 {  
 	 	 	 	 s t r i n g   t i t l e   =   T e x t L a y o u t . C o n v e r t T o S i m p l e T e x t ( R e s . S t r i n g s . W i n d o w . T i t l e ) ;  
 	 	 	 	 s t r i n g   i c o n   =   " m a n i f e s t : E p s i t e c . C o m m o n . D i a l o g s . I m a g e s . W a r n i n g . i c o n " ;  
 	 	 	 	 s t r i n g   e r r   =   R e s . S t r i n g s . Q u e s t i o n . A s s e m b l e r . O k ;  
 	 	 	 	 C o m m o n . D i a l o g s . I D i a l o g   d i a l o g   =   C o m m o n . D i a l o g s . M e s s a g e D i a l o g . C r e a t e O k ( t i t l e ,   i c o n ,   e r r ,   n u l l ,   n u l l ) ;  
 	 	 	 	 d i a l o g . O w n e r W i n d o w   =   t h i s . W i n d o w ;  
 	 	 	 	 d i a l o g . O p e n D i a l o g ( ) ;  
 	 	 	 }  
 	 	 	 e l s e     / /   t r o u v é   ?  
 	 	 	 {  
 	 	 	 	 t h i s . c a l m E d i t o r . C u r s o r   =   i n d e x ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e C a l m A s s e m b l e r C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 / / 	 B o u t o n   [ A S S E M B L E R ]   c l i q u é .  
 	 	 	 t h i s . S t o p ( ) ;  
 	 	 	 i f   ( t h i s . a s s e m b l e r . A c t i o n ( t h i s . c a l m E d i t o r ,   t h i s . W i n d o w ,   t r u e ) )  
 	 	 	 {  
 	 	 	 	 t h i s . d i r t y C a l m   =   f a l s e ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e C a l m E d i t o r T e x t C h a n g e d ( o b j e c t   s e n d e r )  
 	 	 {  
 	 	 	 / / 	 L e   t e x t e   s o u r c e   C A L M   a   é t é   c h a n g é .  
 	 	 	 i f   ( t h i s . i g n o r e C h a n g e )  
 	 	 	 {  
 	 	 	 	 r e t u r n ;  
 	 	 	 }  
  
 	 	 	 t h i s . D i r t y   =   t r u e ;  
 	 	 	 t h i s . d i r t y C a l m   =   t r u e ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e D i s p l a y B u t t o n M o d e C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 / / 	 B o u t o n   [ D I S P L A Y ]   c l i q u é .  
 	 	 	 i f   ( t h i s . d i s p l a y B u t t o n M o d e . A c t i v e S t a t e   = =   A c t i v e S t a t e . N o )  
 	 	 	 {  
 	 	 	 	 t h i s . d i s p l a y B u t t o n M o d e . A c t i v e S t a t e   =   A c t i v e S t a t e . Y e s ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . d i s p l a y B u t t o n M o d e . A c t i v e S t a t e   =   A c t i v e S t a t e . N o ;  
 	 	 	 }  
  
 	 	 	 t h i s . U p d a t e D i s p l a y M o d e ( ) ;  
 	 	 	 t h i s . D i r t y   =   t r u e ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e D i s p l a y B u t t o n T e c h n o C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 / / 	 B o u t o n   [ C R T / L C D ]   c l i q u é .  
 	 	 	 i f   ( t h i s . d i s p l a y B i t m a p . T e c h n o l o g y   = =   M y W i d g e t s . D i s p l a y . T y p e . C R T )  
 	 	 	 {  
 	 	 	 	 t h i s . d i s p l a y B i t m a p . T e c h n o l o g y   =   M y W i d g e t s . D i s p l a y . T y p e . L C D ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . d i s p l a y B i t m a p . T e c h n o l o g y   =   M y W i d g e t s . D i s p l a y . T y p e . C R T ;  
 	 	 	 }  
  
 	 	 	 t h i s . U p d a t e D i s p l a y M o d e ( ) ;  
 	 	 	 t h i s . D i r t y   =   t r u e ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e D i s p l a y B u t t o n C l s C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 / / 	 B o u t o n   [ C L S ]   c l i q u é .  
 	 	 	 t h i s . m e m o r y . C l e a r D i s p l a y ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e D i s p l a y B u t t o n K e y C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 / / 	 B o u t o n   [ A R R O W S / N U M E R I C ]   c l i q u é .  
 	 	 	 t h i s . k e y b o a r d A r r o w s   =   ! t h i s . k e y b o a r d A r r o w s ;  
 	 	 	 t h i s . U p d a t e K e y b o a r d ( ) ;  
 	 	 	 t h i s . D i r t y   =   t r u e ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e K e y b o a r d B u t t o n P r e s s e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 / / 	 T o u c h e   d u   c l a v i e r   s i m u l é   p r e s s é e .  
 	 	 	 M y W i d g e t s . P u s h B u t t o n   b u t t o n   =   s e n d e r   a s   M y W i d g e t s . P u s h B u t t o n ;  
  
 	 	 	 i f   ( b u t t o n . I n d e x   = =   0 x 0 8   | |   b u t t o n . I n d e x   = =   0 x 1 0   | |   b u t t o n . I n d e x   > =   1 0 0 )     / /   s h i f t ,   c t r l   o u   f l è c h e   ?  
 	 	 	 {  
 	 	 	 	 b u t t o n . A c t i v e S t a t e   =   ( b u t t o n . A c t i v e S t a t e   = =   A c t i v e S t a t e . Y e s )   ?   A c t i v e S t a t e . N o   :   A c t i v e S t a t e . Y e s ;  
 	 	 	 	 t h i s . U p d a t e K e y b o a r d ( ) ;  
 	 	 	 }  
  
 	 	 	 t h i s . K e y b o a r d C h a n g e d ( b u t t o n ,   t r u e ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e K e y b o a r d B u t t o n R e l e a s e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 / / 	 T o u c h e   d u   c l a v i e r   s i m u l é   r e l â c h é e .  
 	 	 	 M y W i d g e t s . P u s h B u t t o n   b u t t o n   =   s e n d e r   a s   M y W i d g e t s . P u s h B u t t o n ;  
 	 	 	 t h i s . K e y b o a r d C h a n g e d ( b u t t o n ,   f a l s e ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e B u t t o n S t y l e C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 / / 	 B o u t o n   p o u r   m o d i f i e r   l e   s t y l e   d u   c o m m e n t a i r e   c l i q u é .  
 	 	 	 I c o n B u t t o n   b u t t o n   =   s e n d e r   a s   I c o n B u t t o n ;  
  
 	 	 	 i f   ( b u t t o n . N a m e   = =   " F o n t B o l d " )  
 	 	 	 {  
 	 	 	 	 t h i s . f i e l d P r o g r a m R e m . T e x t N a v i g a t o r . S e l e c t i o n B o l d   =   ! t h i s . f i e l d P r o g r a m R e m . T e x t N a v i g a t o r . S e l e c t i o n B o l d ;  
 	 	 	 }  
  
 	 	 	 i f   ( b u t t o n . N a m e   = =   " F o n t I t a l i c " )  
 	 	 	 {  
 	 	 	 	 t h i s . f i e l d P r o g r a m R e m . T e x t N a v i g a t o r . S e l e c t i o n I t a l i c   =   ! t h i s . f i e l d P r o g r a m R e m . T e x t N a v i g a t o r . S e l e c t i o n I t a l i c ;  
 	 	 	 }  
  
 	 	 	 i f   ( b u t t o n . N a m e   = =   " F o n t U n d e r l i n e " )  
 	 	 	 {  
 	 	 	 	 t h i s . f i e l d P r o g r a m R e m . T e x t N a v i g a t o r . S e l e c t i o n U n d e r l i n e   =   ! t h i s . f i e l d P r o g r a m R e m . T e x t N a v i g a t o r . S e l e c t i o n U n d e r l i n e ;  
 	 	 	 }  
  
 	 	 	 t h i s . D i r t y   =   t r u e ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e F i e l d P r o g r a m R e m T e x t C h a n g e d ( o b j e c t   s e n d e r )  
 	 	 {  
 	 	 	 / / 	 L e   c o m m e n t a i r e   l i é   a u   p r o g r a m m e   e s t   c h a n g é .  
 	 	 	 t h i s . D i r t y   =   t r u e ;  
 	 	 }  
 	 	 # e n d r e g i o n  
  
  
 	 	 # r e g i o n   L o o k  
 	 	 p r o t e c t e d   e n u m   L o o k  
 	 	 {  
 	 	 	 G r a y A n d R e d , 	 	 / /   l o o k   s t a n d a r d   g r i s   e t   r o u g e  
 	 	 	 V i s t a , 	 	 	 / /   l o o k   " V i s t a "   b l e u   e t   o r a n g e  
 	 	 	 M e t a l , 	 	 	 / /   l o o k   m é t a l i q u e   g r i s - v e r t   e t   j a u n e  
 	 	 }  
  
 	 	 p u b l i c   s t a t i c   C o l o r   C o l o r H i l i t e  
 	 	 {  
 	 	 	 / / 	 R e t o u r n e   l a   c o u l e u r   d e   m i s e   e n   é v i d e n c e ,   l o r s q u ' u n   b o u t o n   e s t   a l l u m é .  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 s w i t c h   ( D o l p h i n A p p l i c a t i o n . l o o k )  
 	 	 	 	 {  
 	 	 	 	 	 c a s e   L o o k . G r a y A n d R e d :  
 	 	 	 	 	 	 r e t u r n   C o l o r . F r o m R g b ( 1 . 0 0 ,   0 . 0 0 ,   0 . 0 0 ) ;     / /   r o u g e  
  
 	 	 	 	 	 c a s e   L o o k . V i s t a :  
 	 	 	 	 	 	 r e t u r n   C o l o r . F r o m R g b ( 2 5 4 . 0 / 2 5 5 . 0 ,   1 5 6 . 0 / 2 5 5 . 0 ,   8 4 . 0 / 2 5 5 . 0 ) ;     / /   o r a n g e  
  
 	 	 	 	 	 c a s e   L o o k . M e t a l :  
 	 	 	 	 	 	 r e t u r n   C o l o r . F r o m R g b ( 2 5 4 . 0 / 2 5 5 . 0 ,   2 2 4 . 0 / 2 5 5 . 0 ,   8 4 . 0 / 2 5 5 . 0 ) ;     / /   j a u n e  
  
 	 	 	 	 	 d e f a u l t :  
 	 	 	 	 	 	 r e t u r n   C o l o r . F r o m R g b ( 1 . 0 0 ,   0 . 0 0 ,   0 . 0 0 ) ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
 	 	 p u b l i c   s t a t i c   C o l o r   F r o m B r i g h t n e s s ( d o u b l e   b r i g h t n e s s )  
 	 	 {  
 	 	 	 / / 	 R e t o u r n e   l a   c o u l e u r   à   p a r t i r   d e   l ' i n t e n s i t é .  
 	 	 	 i f   ( D o l p h i n A p p l i c a t i o n . l o o k   = =   L o o k . G r a y A n d R e d )  
 	 	 	 {  
 	 	 	 	 r e t u r n   C o l o r . F r o m B r i g h t n e s s ( b r i g h t n e s s ) ;  
 	 	 	 }  
  
 	 	 	 i f   ( D o l p h i n A p p l i c a t i o n . l o o k   = =   L o o k . V i s t a )  
 	 	 	 {  
 	 	 	 	 b r i g h t n e s s   =   S y s t e m . M a t h . P o w ( b r i g h t n e s s ,   1 . 2 ) ;  
 	 	 	 	 b r i g h t n e s s   =   M i s c . N o r m ( b r i g h t n e s s ) ;  
  
 	 	 	 	 f o r   ( i n t   i = 0 ;   i < D o l p h i n A p p l i c a t i o n . r g b . L e n g t h ;   i + = 3 )  
 	 	 	 	 {  
 	 	 	 	 	 i f   ( b r i g h t n e s s   = =   D o l p h i n A p p l i c a t i o n . r g b [ i + 1 ] )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 r e t u r n   C o l o r . F r o m R g b ( D o l p h i n A p p l i c a t i o n . r g b [ i + 0 ] ,   b r i g h t n e s s ,   D o l p h i n A p p l i c a t i o n . r g b [ i + 2 ] ) ;  
 	 	 	 	 	 }  
 	 	 	 	 	 e l s e   i f   ( b r i g h t n e s s   >   D o l p h i n A p p l i c a t i o n . r g b [ i + 1 ] )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 d o u b l e   d   =   ( b r i g h t n e s s - D o l p h i n A p p l i c a t i o n . r g b [ i + 1 ] ) / ( D o l p h i n A p p l i c a t i o n . r g b [ i - 3 + 1 ] - D o l p h i n A p p l i c a t i o n . r g b [ i + 1 ] ) ;  
 	 	 	 	 	 	 d o u b l e   r   =   D o l p h i n A p p l i c a t i o n . r g b [ i + 0 ]   +   d * ( D o l p h i n A p p l i c a t i o n . r g b [ i - 3 + 0 ] - D o l p h i n A p p l i c a t i o n . r g b [ i + 0 ] ) ;  
 	 	 	 	 	 	 d o u b l e   b   =   D o l p h i n A p p l i c a t i o n . r g b [ i + 2 ]   +   d * ( D o l p h i n A p p l i c a t i o n . r g b [ i - 3 + 2 ] - D o l p h i n A p p l i c a t i o n . r g b [ i + 2 ] ) ;  
 	 	 	 	 	 	 r e t u r n   C o l o r . F r o m R g b ( r ,   b r i g h t n e s s ,   b ) ;  
 	 	 	 	 	 }  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 i f   ( D o l p h i n A p p l i c a t i o n . l o o k   = =   L o o k . M e t a l )  
 	 	 	 {  
 	 	 	 	 b r i g h t n e s s   =   S y s t e m . M a t h . P o w ( b r i g h t n e s s ,   1 . 5 ) ;  
 	 	 	 	 b r i g h t n e s s   =   M i s c . N o r m ( b r i g h t n e s s ) ;  
  
 	 	 	 	 f o r   ( i n t   i = 0 ;   i < D o l p h i n A p p l i c a t i o n . r g b . L e n g t h ;   i + = 3 )  
 	 	 	 	 {  
 	 	 	 	 	 i f   ( b r i g h t n e s s   = =   D o l p h i n A p p l i c a t i o n . r g b [ i + 1 ] )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 r e t u r n   C o l o r . F r o m R g b ( D o l p h i n A p p l i c a t i o n . r g b [ i + 0 ] ,   b r i g h t n e s s ,   b r i g h t n e s s ) ;  
 	 	 	 	 	 }  
 	 	 	 	 	 e l s e   i f   ( b r i g h t n e s s   >   D o l p h i n A p p l i c a t i o n . r g b [ i + 1 ] )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 d o u b l e   d   =   ( b r i g h t n e s s - D o l p h i n A p p l i c a t i o n . r g b [ i + 1 ] ) / ( D o l p h i n A p p l i c a t i o n . r g b [ i - 3 + 1 ] - D o l p h i n A p p l i c a t i o n . r g b [ i + 1 ] ) ;  
 	 	 	 	 	 	 d o u b l e   r   =   D o l p h i n A p p l i c a t i o n . r g b [ i + 0 ]   +   d * ( D o l p h i n A p p l i c a t i o n . r g b [ i - 3 + 0 ] - D o l p h i n A p p l i c a t i o n . r g b [ i + 0 ] ) ;  
 	 	 	 	 	 	 r e t u r n   C o l o r . F r o m R g b ( r ,   b r i g h t n e s s ,   b r i g h t n e s s ) ;  
 	 	 	 	 	 }  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 r e t u r n   C o l o r . F r o m B r i g h t n e s s ( 0 ) ;  
 	 	 }  
  
 	 	 / / 	 C e t t e   t a b l e   r e p r o d u i t   l e s   b l e u s   d e   O f f i c e   W o r d   2 0 0 7 :  
 	 	 p r o t e c t e d   s t a t i c   d o u b l e [ ]   r g b   =  
 	 	 {  
 	 	 	 2 5 5 . 0 / 2 5 5 . 0 ,   2 5 5 . 0 / 2 5 5 . 0 ,   2 5 5 . 0 / 2 5 5 . 0 ,  
 	 	 	 2 1 9 . 0 / 2 5 5 . 0 ,   2 4 4 . 0 / 2 5 5 . 0 ,   2 5 4 . 0 / 2 5 5 . 0 ,  
 	 	 	 2 0 4 . 0 / 2 5 5 . 0 ,   2 2 3 . 0 / 2 5 5 . 0 ,   2 4 8 . 0 / 2 5 5 . 0 ,  
 	 	 	 1 9 1 . 0 / 2 5 5 . 0 ,   2 1 9 . 0 / 2 5 5 . 0 ,   2 5 5 . 0 / 2 5 5 . 0 ,  
 	 	 	 1 7 7 . 0 / 2 5 5 . 0 ,   2 0 3 . 0 / 2 5 5 . 0 ,   2 3 5 . 0 / 2 5 5 . 0 ,  
 	 	 	 1 5 3 . 0 / 2 5 5 . 0 ,   1 8 4 . 0 / 2 5 5 . 0 ,   2 2 3 . 0 / 2 5 5 . 0 ,  
 	 	 	 1 4 0 . 0 / 2 5 5 . 0 ,   1 7 4 . 0 / 2 5 5 . 0 ,   2 1 7 . 0 / 2 5 5 . 0 ,  
 	 	 	 1 2 1 . 0 / 2 5 5 . 0 ,   1 5 7 . 0 / 2 5 5 . 0 ,   2 0 3 . 0 / 2 5 5 . 0 ,  
 	 	 	   9 5 . 0 / 2 5 5 . 0 ,   1 3 8 . 0 / 2 5 5 . 0 ,   1 9 4 . 0 / 2 5 5 . 0 ,  
 	 	 	   7 9 . 0 / 2 5 5 . 0 ,   1 0 2 . 0 / 2 5 5 . 0 ,   1 3 2 . 0 / 2 5 5 . 0 ,  
 	 	 	   6 6 . 0 / 2 5 5 . 0 ,     7 5 . 0 / 2 5 5 . 0 ,     9 9 . 0 / 2 5 5 . 0 ,  
 	 	 	     0 . 0 / 2 5 5 . 0 ,       0 . 0 / 2 5 5 . 0 ,       0 . 0 / 2 5 5 . 0 ,  
 	 	 } ;  
  
 	 	 / / 	 L o o k   a c t u e l l e m e n t   s é l e c t i o n n é .  
 	 	 p r o t e c t e d   s t a t i c   L o o k   l o o k   =   L o o k . G r a y A n d R e d ;  
 	 	 # e n d r e g i o n  
  
  
 	 	 p u b l i c   s t a t i c   r e a d o n l y   d o u b l e   M a i n W i d t h     =   8 0 0 ;  
 	 	 p u b l i c   s t a t i c   r e a d o n l y   d o u b l e   M a i n H e i g h t   =   6 0 0 ;  
 	 	 p u b l i c   s t a t i c   r e a d o n l y   d o u b l e   M a i n M a r g i n   =   6 ;  
  
 	 	 p u b l i c   s t a t i c   r e a d o n l y   d o u b l e   P a n e l W i d t h     =   4 0 0 ;  
 	 	 p u b l i c   s t a t i c   r e a d o n l y   d o u b l e   P a n e l H e i g h t   =   4 6 4 ;  
  
 	 	 p r o t e c t e d   s t a t i c   r e a d o n l y   d o u b l e   R e a l M a x I p s   =   2 0 ;  
 	 	 p u b l i c   s t a t i c   r e a d o n l y   s t r i n g   A p p l i c a t i o n T i t l e   =   R e s . S t r i n g s . W i n d o w . A p p l i c a t i o n T i t l e ;  
 	 	 p r o t e c t e d   s t a t i c   r e a d o n l y   s t r i n g   P r o g r a m E m p t y R e m   =   R e s . S t r i n g s . W i n d o w . C o m m e n t ;  
  
  
 	 	 p r o t e c t e d   C o m m o n . S u p p o r t . R e s o u r c e M a n a g e r P o o l 	 r e s o u r c e M a n a g e r P o o l ;  
 	 	 p r o t e c t e d   V e r s i o n C h e c k e r 	 	 	 	 	 	 c h e c k e r ;  
 	 	 p r o t e c t e d   M y W i d g e t s . M a i n P a n e l 	 	 	 	 	 m a i n P a n e l ;  
 	 	 p r o t e c t e d   M y W i d g e t s . P a n e l 	 	 	 	 	 	 p a n e l T i t l e ;  
 	 	 p r o t e c t e d   M y W i d g e t s . P a n e l 	 	 	 	 	 	 l e f t P a n e l ;  
 	 	 p r o t e c t e d   M y W i d g e t s . P a n e l 	 	 	 	 	 	 r i g h t P a n e l ;  
 	 	 p r o t e c t e d   M y W i d g e t s . P a n e l 	 	 	 	 	 	 l e f t H e a d e r ;  
 	 	 p r o t e c t e d   M y W i d g e t s . P a n e l 	 	 	 	 	 	 l e f t C l o c k ;  
 	 	 p r o t e c t e d   M y W i d g e t s . P a n e l 	 	 	 	 	 	 l e f t P a n e l B u s ;  
 	 	 p r o t e c t e d   M y W i d g e t s . P a n e l 	 	 	 	 	 	 l e f t P a n e l D e t a i l ;  
 	 	 p r o t e c t e d   M y W i d g e t s . P a n e l 	 	 	 	 	 	 l e f t P a n e l C o d e ;  
 	 	 p r o t e c t e d   M y W i d g e t s . P a n e l 	 	 	 	 	 	 l e f t P a n e l C a l m ;  
 	 	 p r o t e c t e d   M y W i d g e t s . P a n e l 	 	 	 	 	 	 l e f t P a n e l Q u i c k ;  
 	 	 p r o t e c t e d   M y W i d g e t s . P a n e l 	 	 	 	 	 	 c l o c k B u s P a n e l ;  
 	 	 p r o t e c t e d   M y W i d g e t s . P a n e l 	 	 	 	 	 	 h e l p P a n e l ;  
 	 	 p r o t e c t e d   M y W i d g e t s . P a n e l 	 	 	 	 	 	 c a l m P a n e l ;  
 	 	 p r o t e c t e d   M y W i d g e t s . L i n e 	 	 	 	 	 	 t o p L e f t S e p ;  
 	 	 p r o t e c t e d   I c o n B u t t o n 	 	 	 	 	 	 	 b u t t o n N e w ;  
 	 	 p r o t e c t e d   I c o n B u t t o n 	 	 	 	 	 	 	 b u t t o n O p e n ;  
 	 	 p r o t e c t e d   I c o n B u t t o n 	 	 	 	 	 	 	 b u t t o n S a v e ;  
 	 	 p r o t e c t e d   I c o n B u t t o n 	 	 	 	 	 	 	 b u t t o n L o o k ;  
 	 	 p r o t e c t e d   I c o n B u t t o n 	 	 	 	 	 	 	 b u t t o n A b o u t ;  
 	 	 p r o t e c t e d   M y W i d g e t s . P u s h B u t t o n 	 	 	 	 	 b u t t o n M o d e B u s ;  
 	 	 p r o t e c t e d   M y W i d g e t s . P u s h B u t t o n 	 	 	 	 	 b u t t o n M o d e D e t a i l ;  
 	 	 p r o t e c t e d   M y W i d g e t s . P u s h B u t t o n 	 	 	 	 	 b u t t o n M o d e C o d e ;  
 	 	 p r o t e c t e d   M y W i d g e t s . P u s h B u t t o n 	 	 	 	 	 b u t t o n M o d e C a l m ;  
 	 	 p r o t e c t e d   M y W i d g e t s . P u s h B u t t o n 	 	 	 	 	 b u t t o n M o d e Q u i c k ;  
 	 	 p r o t e c t e d   M y W i d g e t s . P u s h B u t t o n 	 	 	 	 	 b u t t o n R u n ;  
 	 	 p r o t e c t e d   M y W i d g e t s . P u s h B u t t o n 	 	 	 	 	 b u t t o n S t e p ;  
 	 	 p r o t e c t e d   M y W i d g e t s . P u s h B u t t o n 	 	 	 	 	 b u t t o n M e m o r y R e a d ;  
 	 	 p r o t e c t e d   M y W i d g e t s . P u s h B u t t o n 	 	 	 	 	 b u t t o n M e m o r y W r i t e ;  
 	 	 p r o t e c t e d   M y W i d g e t s . P u s h B u t t o n 	 	 	 	 	 b u t t o n C l o c k 6 ;  
 	 	 p r o t e c t e d   M y W i d g e t s . P u s h B u t t o n 	 	 	 	 	 b u t t o n C l o c k 5 ;  
 	 	 p r o t e c t e d   M y W i d g e t s . P u s h B u t t o n 	 	 	 	 	 b u t t o n C l o c k 4 ;  
 	 	 p r o t e c t e d   M y W i d g e t s . P u s h B u t t o n 	 	 	 	 	 b u t t o n C l o c k 3 ;  
 	 	 p r o t e c t e d   M y W i d g e t s . P u s h B u t t o n 	 	 	 	 	 b u t t o n C l o c k 2 ;  
 	 	 p r o t e c t e d   M y W i d g e t s . P u s h B u t t o n 	 	 	 	 	 b u t t o n C l o c k 1 ;  
 	 	 p r o t e c t e d   M y W i d g e t s . P u s h B u t t o n 	 	 	 	 	 b u t t o n C l o c k 0 ;  
 	 	 p r o t e c t e d   M y W i d g e t s . S w i t c h 	 	 	 	 	 	 s w i t c h S t e p ;  
 	 	 p r o t e c t e d   M y W i d g e t s . S w i t c h 	 	 	 	 	 	 s w i t c h I n t o ;  
 	 	 p r o t e c t e d   L i s t < M y W i d g e t s . D i g i t > 	 	 	 	 	 a d d r e s s D i g i t s ;  
 	 	 p r o t e c t e d   L i s t < M y W i d g e t s . L e d > 	 	 	 	 	 a d d r e s s L e d s ;  
 	 	 p r o t e c t e d   L i s t < M y W i d g e t s . S w i t c h > 	 	 	 	 a d d r e s s S w i t c h s ;  
 	 	 p r o t e c t e d   L i s t < M y W i d g e t s . D i g i t > 	 	 	 	 	 d a t a D i g i t s ;  
 	 	 p r o t e c t e d   L i s t < M y W i d g e t s . L e d > 	 	 	 	 	 d a t a L e d s ;  
 	 	 p r o t e c t e d   L i s t < M y W i d g e t s . S w i t c h > 	 	 	 	 d a t a S w i t c h s ;  
 	 	 p r o t e c t e d   M y W i d g e t s . P u s h B u t t o n 	 	 	 	 	 d i s p l a y B u t t o n M o d e ;  
 	 	 p r o t e c t e d   M y W i d g e t s . P u s h B u t t o n 	 	 	 	 	 d i s p l a y B u t t o n T e c h n o ;  
 	 	 p r o t e c t e d   M y W i d g e t s . P u s h B u t t o n 	 	 	 	 	 d i s p l a y B u t t o n C l s ;  
 	 	 p r o t e c t e d   M y W i d g e t s . P u s h B u t t o n 	 	 	 	 	 d i s p l a y B u t t o n K e y ;  
 	 	 p r o t e c t e d   L i s t < M y W i d g e t s . D i g i t > 	 	 	 	 	 d i s p l a y D i g i t s ;  
 	 	 p r o t e c t e d   M y W i d g e t s . D i s p l a y 	 	 	 	 	 	 d i s p l a y B i t m a p ;  
 	 	 p r o t e c t e d   L i s t < M y W i d g e t s . P u s h B u t t o n > 	 	 	 k e y b o a r d B u t t o n s ;  
 	 	 p r o t e c t e d   M y W i d g e t s . P u s h B u t t o n 	 	 	 	 	 b u t t o n R e s e t ;  
 	 	 p r o t e c t e d   L i s t < M y W i d g e t s . T e x t F i e l d H e x a > 	 	 	 r e g i s t e r F i e l d s ;  
 	 	 p r o t e c t e d   M y W i d g e t s . M e m o r y A c c e s s o r 	 	 	 	 m e m o r y A c c e s s o r ;  
 	 	 p r o t e c t e d   M y W i d g e t s . P u s h B u t t o n 	 	 	 	 	 m e m o r y B u t t o n P C ;  
 	 	 p r o t e c t e d   M y W i d g e t s . P u s h B u t t o n 	 	 	 	 	 m e m o r y B u t t o n M ;  
 	 	 p r o t e c t e d   M y W i d g e t s . P u s h B u t t o n 	 	 	 	 	 m e m o r y B u t t o n R ;  
 	 	 p r o t e c t e d   M y W i d g e t s . P u s h B u t t o n 	 	 	 	 	 m e m o r y B u t t o n P ;  
 	 	 p r o t e c t e d   M y W i d g e t s . P u s h B u t t o n 	 	 	 	 	 m e m o r y B u t t o n D ;  
 	 	 p r o t e c t e d   M y W i d g e t s . C o d e A c c e s s o r 	 	 	 	 c o d e A c c e s s o r ;  
 	 	 p r o t e c t e d   M y W i d g e t s . P u s h B u t t o n 	 	 	 	 	 c o d e B u t t o n P C ;  
 	 	 p r o t e c t e d   M y W i d g e t s . P u s h B u t t o n 	 	 	 	 	 c o d e B u t t o n M ;  
 	 	 p r o t e c t e d   M y W i d g e t s . P u s h B u t t o n 	 	 	 	 	 c o d e B u t t o n R ;  
 	 	 p r o t e c t e d   M y W i d g e t s . P u s h B u t t o n 	 	 	 	 	 c o d e B u t t o n A d d ;  
 	 	 p r o t e c t e d   M y W i d g e t s . P u s h B u t t o n 	 	 	 	 	 c o d e B u t t o n S u b ;  
 	 	 p r o t e c t e d   L i s t < T e x t F i e l d > 	 	 	 	 	 	 c o d e R e g i s t e r s ;  
 	 	 p r o t e c t e d   T e x t F i e l d M u l t i 	 	 	 	 	 	 c a l m E d i t o r ;  
 	 	 p r o t e c t e d   I c o n B u t t o n 	 	 	 	 	 	 	 c a l m B u t t o n O p e n ;  
 	 	 p r o t e c t e d   I c o n B u t t o n 	 	 	 	 	 	 	 c a l m B u t t o n S a v e ;  
 	 	 p r o t e c t e d   I c o n B u t t o n 	 	 	 	 	 	 	 c a l m B u t t o n S h o w ;  
 	 	 p r o t e c t e d   I c o n B u t t o n 	 	 	 	 	 	 	 c a l m B u t t o n B i g ;  
 	 	 p r o t e c t e d   I c o n B u t t o n 	 	 	 	 	 	 	 c a l m B u t t o n F u l l ;  
 	 	 p r o t e c t e d   M y W i d g e t s . P u s h B u t t o n 	 	 	 	 	 c a l m B u t t o n E r r ;  
 	 	 p r o t e c t e d   M y W i d g e t s . P u s h B u t t o n 	 	 	 	 	 c a l m B u t t o n A s s ;  
 	 	 p r o t e c t e d   T a b B o o k 	 	 	 	 	 	 	 	 b o o k ;  
 	 	 p r o t e c t e d   T a b P a g e 	 	 	 	 	 	 	 	 p a g e P r o g r a m ;  
 	 	 p r o t e c t e d   T e x t F i e l d M u l t i 	 	 	 	 	 	 f i e l d P r o g r a m R e m ;  
  
 	 	 p r o t e c t e d   C o m p o n e n t s . M e m o r y 	 	 	 	 	 	 m e m o r y ;  
 	 	 p r o t e c t e d   C o m p o n e n t s . A b s t r a c t P r o c e s s o r 	 	 	 p r o c e s s o r ;  
 	 	 p r o t e c t e d   A s s e m b l e r 	 	 	 	 	 	 	 	 a s s e m b l e r ;  
 	 	 p r o t e c t e d   i n t 	 	 	 	 	 	 	 	 	 b r e a k A d d r e s s ;  
 	 	 p r o t e c t e d   T i m e r 	 	 	 	 	 	 	 	 	 c l o c k ;  
 	 	 p r o t e c t e d   d o u b l e 	 	 	 	 	 	 	 	 i p s ;  
 	 	 p r o t e c t e d   s t r i n g 	 	 	 	 	 	 	 	 p a n e l M o d e ;  
 	 	 p r o t e c t e d   s t r i n g 	 	 	 	 	 	 	 	 f i l e n a m e ;  
 	 	 p r o t e c t e d   s t r i n g 	 	 	 	 	 	 	 	 f i l e n a m e C a l m ;  
 	 	 p r o t e c t e d   b o o l 	 	 	 	 	 	 	 	 	 f i r s t O p e n S a v e D i a l o g ;  
 	 	 p r o t e c t e d   b o o l 	 	 	 	 	 	 	 	 	 k e y b o a r d A r r o w s ;  
 	 	 p r o t e c t e d   b o o l 	 	 	 	 	 	 	 	 	 d i r t y ;  
 	 	 p r o t e c t e d   b o o l 	 	 	 	 	 	 	 	 	 d i r t y C a l m ;  
 	 	 p r o t e c t e d   b o o l 	 	 	 	 	 	 	 	 	 i g n o r e C h a n g e ;  
 	 }  
 }  
 