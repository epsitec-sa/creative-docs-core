ÿþu s i n g   S y s t e m . C o l l e c t i o n s . G e n e r i c ;  
 u s i n g   S y s t e m . T e x t . R e g u l a r E x p r e s s i o n s ;  
 u s i n g   E p s i t e c . C o m m o n . W i d g e t s ;  
 u s i n g   E p s i t e c . C o m m o n . S u p p o r t ;  
 u s i n g   E p s i t e c . C o m m o n . T y p e s ;  
 u s i n g   E p s i t e c . C o m m o n . D r a w i n g ;  
  
 n a m e s p a c e   E p s i t e c . C o m m o n . D e s i g n e r . V i e w e r s  
 {  
 	 / / /   < s u m m a r y >  
 	 / / /   P e r m e t   d e   r e p r é s e n t e r   l e s   r e s s o u r c e s   d ' u n   m o d u l e .  
 	 / / /   < / s u m m a r y >  
 	 p u b l i c   c l a s s   E n t i t i e s   :   A b s t r a c t C a p t i o n s  
 	 {  
 	 	 p r o t e c t e d   i n t e r n a l   E n t i t i e s ( M o d u l e   m o d u l e ,   P a n e l s C o n t e x t   c o n t e x t ,   R e s o u r c e A c c e s s   a c c e s s ,   D e s i g n e r A p p l i c a t i o n   d e s i g n e r A p p l i c a t i o n )  
 	 	 	 :   b a s e   ( m o d u l e ,   c o n t e x t ,   a c c e s s ,   d e s i g n e r A p p l i c a t i o n )  
 	 	 {  
 	 	 	 b o o l   i s F u l l S c r e e n   =   ( t h i s . d e s i g n e r A p p l i c a t i o n . D i s p l a y M o d e S t a t e   = =   D e s i g n e r A p p l i c a t i o n . D i s p l a y M o d e . F u l l S c r e e n ) ;  
 	 	 	 b o o l   i s W i n d o w           =   ( t h i s . d e s i g n e r A p p l i c a t i o n . D i s p l a y M o d e S t a t e   = =   D e s i g n e r A p p l i c a t i o n . D i s p l a y M o d e . W i n d o w ) ;  
  
 	 	 	 t h i s . s h o w T o p F r a m e   =   i s W i n d o w ;  
  
 	 	 	 t h i s . l a s t G r o u p . D o c k   =   i s W i n d o w   ?   D o c k S t y l e . F i l l   :   D o c k S t y l e . T o p ;  
 	 	 	 t h i s . l a s t G r o u p . V i s i b i l i t y   =   ! i s F u l l S c r e e n ;  
 	 	 	 t h i s . l a s t G r o u p . P r e f e r r e d H e i g h t   =   2 0 0 ;  
  
 	 	 	 t h i s . h s p l i t t e r   =   n e w   H S p l i t t e r ( t h i s . l a s t P a n e ) ;  
 	 	 	 t h i s . h s p l i t t e r . D o c k   =   D o c k S t y l e . T o p ;  
  
 	 	 	 t h i s . e d i t o r G r o u p   =   n e w   F r a m e B o x ( i s W i n d o w   ?   ( W i d g e t )   t h i s . d e s i g n e r A p p l i c a t i o n . V i e w e r s W i n d o w . R o o t   :   t h i s . l a s t P a n e ) ;  
 	 	 	 t h i s . e d i t o r G r o u p . P a d d i n g   =   n e w   M a r g i n s   ( i s W i n d o w   ?   0   :   1 0 ) ;  
 	 	 	 t h i s . e d i t o r G r o u p . D o c k   =   D o c k S t y l e . F i l l ;  
  
 	 	 	 t h i s . l a s t G r o u p . M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   i s W i n d o w   ?   0   :   1 0 ,   0 ,   0 ) ;  
  
 	 	 	 t h i s . s h o w H i d e T o p F r a m e B u t t o n   =   n e w   G l y p h B u t t o n  
 	 	 	 {  
 	 	 	 	 P a r e n t   =   t h i s . l a s t P a n e ,  
 	 	 	 	 B u t t o n S t y l e   =   W i d g e t s . B u t t o n S t y l e . S l i d e r ,  
 	 	 	 	 P r e f e r r e d S i z e   =   n e w   S i z e   ( 1 7 ,   1 7 ) ,  
 	 	 	 	 A n c h o r   =   A n c h o r S t y l e s . T o p R i g h t ,  
 	 	 	 	 M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   2 ,   2 ,   0 ) ,  
 	 	 	 	 V i s i b i l i t y   =   ! i s W i n d o w ,  
 	 	 	 } ;  
  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p   ( t h i s . s h o w H i d e T o p F r a m e B u t t o n ,   " M o n t r e   o u   c a c h e   l e   p a n n e a u   s u p é r i e u r   d e s   t e x t e s " ) ;  
  
 	 	 	 t h i s . s h o w H i d e T o p F r a m e B u t t o n . C l i c k e d   + =   d e l e g a t e  
 	 	 	 {  
 	 	 	 	 t h i s . s h o w T o p F r a m e   =   ! t h i s . s h o w T o p F r a m e ;  
 	 	 	 	 t h i s . U p d a t e T o p F r a m e   ( ) ;  
 	 	 	 } ;  
  
 	 	 	 t h i s . U p d a t e T o p F r a m e   ( ) ;  
  
 	 	 	 / / 	 C r é e   l e s   g r a n d s   b l o c s   d e   w i d g e t s .  
 	 	 	 v a r   b a n d   =   n e w   F r a m e B o x   ( t h i s . e d i t o r G r o u p ) ;  
 	 	 	 b a n d . D o c k   =   D o c k S t y l e . F i l l ;  
  
 	 	 	 t h i s . e d i t o r   =   n e w   E n t i t i e s E d i t o r . E d i t o r ( b a n d ) ;  
 	 	 	 t h i s . e d i t o r . E n t i t i e s   =   t h i s ;  
 	 	 	 t h i s . e d i t o r . M o d u l e   =   t h i s . m o d u l e ;  
 	 	 	 t h i s . e d i t o r . D o c k   =   D o c k S t y l e . F i l l ;  
 	 	 	 t h i s . e d i t o r . A r e a S i z e   =   t h i s . a r e a S i z e ;  
 	 	 	 t h i s . e d i t o r . Z o o m   =   t h i s . Z o o m ;  
 	 	 	 t h i s . e d i t o r . S i z e C h a n g e d   + =   n e w   E v e n t H a n d l e r < D e p e n d e n c y P r o p e r t y C h a n g e d E v e n t A r g s > ( t h i s . H a n d l e E d i t o r S i z e C h a n g e d ) ;  
 	 	 	 t h i s . e d i t o r . A r e a S i z e C h a n g e d   + =   t h i s . H a n d l e E d i t o r A r e a S i z e C h a n g e d ;  
 	 	 	 t h i s . e d i t o r . A r e a O f f s e t C h a n g e d   + =   t h i s . H a n d l e E d i t o r A r e a O f f s e t C h a n g e d ;  
 	 	 	 t h i s . e d i t o r . Z o o m C h a n g e d   + =   t h i s . H a n d l e E d i t o r Z o o m C h a n g e d ;  
 	 	 	 T o o l T i p . D e f a u l t . R e g i s t e r D y n a m i c T o o l T i p H o s t ( t h i s . e d i t o r ) ;     / /   p o u r   v o i r   l e s   t o o l t i p s   d y n a m i q u e s  
  
 	 	 	 t h i s . v s c r o l l e r   =   n e w   V S c r o l l e r ( b a n d ) ;  
 	 	 	 t h i s . v s c r o l l e r . I s I n v e r t e d   =   t r u e ;  
 	 	 	 t h i s . v s c r o l l e r . D o c k   =   D o c k S t y l e . R i g h t ;  
 	 	 	 t h i s . v s c r o l l e r . V a l u e C h a n g e d   + =   t h i s . H a n d l e S c r o l l e r V a l u e C h a n g e d ;  
 	 	 	 t h i s . e d i t o r . V S c r o l l e r   =   t h i s . v s c r o l l e r ;  
  
 	 	 	 t h i s . g r o u p T o o l b a r   =   n e w   M y W i d g e t s . R e s e t B o x ( t h i s . e d i t o r G r o u p ) ;  
 	 	 	 t h i s . g r o u p T o o l b a r . I s P a t c h   =   t h i s . m o d u l e . I s P a t c h ;  
 	 	 	 t h i s . g r o u p T o o l b a r . D o c k   =   D o c k S t y l e . B o t t o m ;  
 	 	 	 t h i s . g r o u p T o o l b a r . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   0 ,   5 ,   0 ) ;  
 	 	 	 t h i s . g r o u p T o o l b a r . R e s e t B u t t o n . C l i c k e d   + =   t h i s . H a n d l e R e s e t B u t t o n C l i c k e d ;  
  
 	 	 	 t h i s . t o o l b a r   =   n e w   H T o o l B a r ( t h i s . g r o u p T o o l b a r . G r o u p B o x ) ;  
 	 	 	 t h i s . t o o l b a r . D o c k   =   D o c k S t y l e . F i l l ;  
  
 	 	 	 t h i s . h s c r o l l e r   =   n e w   H S c r o l l e r ( t h i s . e d i t o r G r o u p ) ;  
 	 	 	 t h i s . h s c r o l l e r . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   t h i s . v s c r o l l e r . P r e f e r r e d W i d t h ,   0 ,   0 ) ;  
 	 	 	 t h i s . h s c r o l l e r . D o c k   =   D o c k S t y l e . B o t t o m ;  
 	 	 	 t h i s . h s c r o l l e r . V a l u e C h a n g e d   + =   t h i s . H a n d l e S c r o l l e r V a l u e C h a n g e d ;  
  
 	 	 	 / / 	 P e u p l e   l a   t o o l b a r .  
 	 	 	 t h i s . b u t t o n S u b V i e w A   =   n e w   M y W i d g e t s . E n t i t y S u b V i e w ( t h i s . t o o l b a r ) ;  
 	 	 	 t h i s . b u t t o n S u b V i e w A . T e x t   =   R e s . S t r i n g s . E n t i t i e s . S u b V i e w . Q u i c k . A ;  
 	 	 	 t h i s . b u t t o n S u b V i e w A . P r e f e r r e d W i d t h   =   t h i s . b u t t o n S u b V i e w A . P r e f e r r e d H e i g h t ;  
 	 	 	 t h i s . b u t t o n S u b V i e w A . B u t t o n S t y l e   =   B u t t o n S t y l e . A c t i v a b l e I c o n ;  
 	 	 	 t h i s . b u t t o n S u b V i e w A . A u t o F o c u s   =   f a l s e ;  
 	 	 	 t h i s . b u t t o n S u b V i e w A . D o c k   =   D o c k S t y l e . L e f t ;  
 	 	 	 t h i s . b u t t o n S u b V i e w A . C l i c k e d   + =   t h i s . H a n d l e B u t t o n S u b V i e w C l i c k e d ;  
 	 	 	 t h i s . b u t t o n S u b V i e w A . D r a g S t a r t i n g   + =   t h i s . H a n d l e B u t t o n S u b V i e w D r a g S t a r t i n g ;  
 	 	 	 t h i s . b u t t o n S u b V i e w A . D r a g E n d i n g   + =   t h i s . H a n d l e B u t t o n S u b V i e w D r a g E n d i n g ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . b u t t o n S u b V i e w A ,   R e s . S t r i n g s . E n t i t i e s . S u b V i e w . L o n g . A ) ;  
  
 	 	 	 t h i s . b u t t o n S u b V i e w B   =   n e w   M y W i d g e t s . E n t i t y S u b V i e w ( t h i s . t o o l b a r ) ;  
 	 	 	 t h i s . b u t t o n S u b V i e w B . T e x t   =   R e s . S t r i n g s . E n t i t i e s . S u b V i e w . Q u i c k . B ;  
 	 	 	 t h i s . b u t t o n S u b V i e w B . P r e f e r r e d W i d t h   =   t h i s . b u t t o n S u b V i e w B . P r e f e r r e d H e i g h t ;  
 	 	 	 t h i s . b u t t o n S u b V i e w B . B u t t o n S t y l e   =   B u t t o n S t y l e . A c t i v a b l e I c o n ;  
 	 	 	 t h i s . b u t t o n S u b V i e w B . A u t o F o c u s   =   f a l s e ;  
 	 	 	 t h i s . b u t t o n S u b V i e w B . D o c k   =   D o c k S t y l e . L e f t ;  
 	 	 	 t h i s . b u t t o n S u b V i e w B . C l i c k e d   + =   t h i s . H a n d l e B u t t o n S u b V i e w C l i c k e d ;  
 	 	 	 t h i s . b u t t o n S u b V i e w B . D r a g S t a r t i n g   + =   t h i s . H a n d l e B u t t o n S u b V i e w D r a g S t a r t i n g ;  
 	 	 	 t h i s . b u t t o n S u b V i e w B . D r a g E n d i n g   + =   t h i s . H a n d l e B u t t o n S u b V i e w D r a g E n d i n g ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . b u t t o n S u b V i e w B ,   R e s . S t r i n g s . E n t i t i e s . S u b V i e w . L o n g . B ) ;  
  
 	 	 	 t h i s . b u t t o n S u b V i e w C   =   n e w   M y W i d g e t s . E n t i t y S u b V i e w ( t h i s . t o o l b a r ) ;  
 	 	 	 t h i s . b u t t o n S u b V i e w C . T e x t   =   R e s . S t r i n g s . E n t i t i e s . S u b V i e w . Q u i c k . C ;  
 	 	 	 t h i s . b u t t o n S u b V i e w C . P r e f e r r e d W i d t h   =   t h i s . b u t t o n S u b V i e w C . P r e f e r r e d H e i g h t ;  
 	 	 	 t h i s . b u t t o n S u b V i e w C . B u t t o n S t y l e   =   B u t t o n S t y l e . A c t i v a b l e I c o n ;  
 	 	 	 t h i s . b u t t o n S u b V i e w C . A u t o F o c u s   =   f a l s e ;  
 	 	 	 t h i s . b u t t o n S u b V i e w C . D o c k   =   D o c k S t y l e . L e f t ;  
 	 	 	 t h i s . b u t t o n S u b V i e w C . C l i c k e d   + =   t h i s . H a n d l e B u t t o n S u b V i e w C l i c k e d ;  
 	 	 	 t h i s . b u t t o n S u b V i e w C . D r a g S t a r t i n g   + =   t h i s . H a n d l e B u t t o n S u b V i e w D r a g S t a r t i n g ;  
 	 	 	 t h i s . b u t t o n S u b V i e w C . D r a g E n d i n g   + =   t h i s . H a n d l e B u t t o n S u b V i e w D r a g E n d i n g ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . b u t t o n S u b V i e w C ,   R e s . S t r i n g s . E n t i t i e s . S u b V i e w . L o n g . C ) ;  
  
 	 	 	 t h i s . b u t t o n S u b V i e w T   =   n e w   M y W i d g e t s . E n t i t y S u b V i e w ( t h i s . t o o l b a r ) ;  
 	 	 	 t h i s . b u t t o n S u b V i e w T . T e x t   =   R e s . S t r i n g s . E n t i t i e s . S u b V i e w . Q u i c k . T ;  
 	 	 	 t h i s . b u t t o n S u b V i e w T . P r e f e r r e d W i d t h   =   t h i s . b u t t o n S u b V i e w T . P r e f e r r e d H e i g h t ;  
 	 	 	 t h i s . b u t t o n S u b V i e w T . B u t t o n S t y l e   =   B u t t o n S t y l e . A c t i v a b l e I c o n ;  
 	 	 	 t h i s . b u t t o n S u b V i e w T . A u t o F o c u s   =   f a l s e ;  
 	 	 	 t h i s . b u t t o n S u b V i e w T . D o c k   =   D o c k S t y l e . L e f t ;  
 	 	 	 t h i s . b u t t o n S u b V i e w T . M a r g i n s   =   n e w   M a r g i n s ( 2 ,   0 ,   0 ,   0 ) ;  
 	 	 	 t h i s . b u t t o n S u b V i e w T . C l i c k e d   + =   t h i s . H a n d l e B u t t o n S u b V i e w C l i c k e d ;  
 	 	 	 t h i s . b u t t o n S u b V i e w T . D r a g S t a r t i n g   + =   t h i s . H a n d l e B u t t o n S u b V i e w D r a g S t a r t i n g ;  
 	 	 	 t h i s . b u t t o n S u b V i e w T . D r a g E n d i n g   + =   t h i s . H a n d l e B u t t o n S u b V i e w D r a g E n d i n g ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . b u t t o n S u b V i e w T ,   R e s . S t r i n g s . E n t i t i e s . S u b V i e w . L o n g . T ) ;  
  
 	 	 	 I c o n S e p a r a t o r   s e p   =   n e w   I c o n S e p a r a t o r ( t h i s . t o o l b a r ) ;  
 	 	 	 s e p . D o c k   =   D o c k S t y l e . L e f t ;  
  
 	 	 	 t h i s . b u t t o n Z o o m P a g e   =   n e w   I c o n B u t t o n ( t h i s . t o o l b a r ) ;  
 	 	 	 t h i s . b u t t o n Z o o m P a g e . I c o n U r i   =   M i s c . I c o n ( " Z o o m P a g e " ) ;  
 	 	 	 t h i s . b u t t o n Z o o m P a g e . B u t t o n S t y l e   =   B u t t o n S t y l e . A c t i v a b l e I c o n ;  
 	 	 	 t h i s . b u t t o n Z o o m P a g e . A u t o F o c u s   =   f a l s e ;  
 	 	 	 t h i s . b u t t o n Z o o m P a g e . D o c k   =   D o c k S t y l e . L e f t ;  
 	 	 	 t h i s . b u t t o n Z o o m P a g e . C l i c k e d   + =   t h i s . H a n d l e B u t t o n Z o o m C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . b u t t o n Z o o m P a g e ,   R e s . S t r i n g s . E n t i t i e s . A c t i o n . Z o o m P a g e ) ;  
  
 	 	 	 t h i s . b u t t o n Z o o m M i n   =   n e w   I c o n B u t t o n ( t h i s . t o o l b a r ) ;  
 	 	 	 t h i s . b u t t o n Z o o m M i n . I c o n U r i   =   M i s c . I c o n ( " Z o o m M i n " ) ;  
 	 	 	 t h i s . b u t t o n Z o o m M i n . B u t t o n S t y l e   =   B u t t o n S t y l e . A c t i v a b l e I c o n ;  
 	 	 	 t h i s . b u t t o n Z o o m M i n . A u t o F o c u s   =   f a l s e ;  
 	 	 	 t h i s . b u t t o n Z o o m M i n . D o c k   =   D o c k S t y l e . L e f t ;  
 	 	 	 t h i s . b u t t o n Z o o m M i n . C l i c k e d   + =   t h i s . H a n d l e B u t t o n Z o o m C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . b u t t o n Z o o m M i n ,   R e s . S t r i n g s . E n t i t i e s . A c t i o n . Z o o m M i n ) ;  
  
 	 	 	 t h i s . b u t t o n Z o o m D e f a u l t   =   n e w   I c o n B u t t o n ( t h i s . t o o l b a r ) ;  
 	 	 	 t h i s . b u t t o n Z o o m D e f a u l t . I c o n U r i   =   M i s c . I c o n ( " Z o o m D e f a u l t " ) ;  
 	 	 	 t h i s . b u t t o n Z o o m D e f a u l t . B u t t o n S t y l e   =   B u t t o n S t y l e . A c t i v a b l e I c o n ;  
 	 	 	 t h i s . b u t t o n Z o o m D e f a u l t . A u t o F o c u s   =   f a l s e ;  
 	 	 	 t h i s . b u t t o n Z o o m D e f a u l t . D o c k   =   D o c k S t y l e . L e f t ;  
 	 	 	 t h i s . b u t t o n Z o o m D e f a u l t . C l i c k e d   + =   t h i s . H a n d l e B u t t o n Z o o m C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . b u t t o n Z o o m D e f a u l t ,   R e s . S t r i n g s . E n t i t i e s . A c t i o n . Z o o m D e f a u l t ) ;  
  
 	 	 	 t h i s . b u t t o n Z o o m M a x   =   n e w   I c o n B u t t o n ( t h i s . t o o l b a r ) ;  
 	 	 	 t h i s . b u t t o n Z o o m M a x . I c o n U r i   =   M i s c . I c o n ( " Z o o m M a x " ) ;  
 	 	 	 t h i s . b u t t o n Z o o m M a x . B u t t o n S t y l e   =   B u t t o n S t y l e . A c t i v a b l e I c o n ;  
 	 	 	 t h i s . b u t t o n Z o o m M a x . A u t o F o c u s   =   f a l s e ;  
 	 	 	 t h i s . b u t t o n Z o o m M a x . D o c k   =   D o c k S t y l e . L e f t ;  
 	 	 	 t h i s . b u t t o n Z o o m M a x . C l i c k e d   + =   t h i s . H a n d l e B u t t o n Z o o m C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . b u t t o n Z o o m M a x ,   R e s . S t r i n g s . E n t i t i e s . A c t i o n . Z o o m M a x ) ;  
  
 	 	 	 t h i s . f i e l d Z o o m   =   n e w   S t a t u s F i e l d ( t h i s . t o o l b a r ) ;  
 	 	 	 t h i s . f i e l d Z o o m . P r e f e r r e d W i d t h   =   5 0 ;  
 	 	 	 t h i s . f i e l d Z o o m . M a r g i n s   =   n e w   M a r g i n s ( 5 ,   5 ,   1 ,   1 ) ;  
 	 	 	 t h i s . f i e l d Z o o m . D o c k   =   D o c k S t y l e . L e f t ;  
 	 	 	 t h i s . f i e l d Z o o m . C l i c k e d   + =   t h i s . H a n d l e F i e l d Z o o m C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . f i e l d Z o o m ,   R e s . S t r i n g s . E n t i t i e s . A c t i o n . Z o o m M e n u ) ;  
  
 	 	 	 t h i s . s l i d e r Z o o m   =   n e w   H S l i d e r ( t h i s . t o o l b a r ) ;  
 	 	 	 t h i s . s l i d e r Z o o m . M i n V a l u e   =   ( d e c i m a l )   E n t i t i e s . z o o m M i n ;  
 	 	 	 t h i s . s l i d e r Z o o m . M a x V a l u e   =   ( d e c i m a l )   E n t i t i e s . z o o m M a x ;  
 	 	 	 t h i s . s l i d e r Z o o m . S m a l l C h a n g e   =   ( d e c i m a l )   0 . 1 ;  
 	 	 	 t h i s . s l i d e r Z o o m . L a r g e C h a n g e   =   ( d e c i m a l )   0 . 2 ;  
 	 	 	 t h i s . s l i d e r Z o o m . R e s o l u t i o n   =   ( d e c i m a l )   0 . 0 1 ;  
 	 	 	 t h i s . s l i d e r Z o o m . P r e f e r r e d W i d t h   =   9 0 ;  
 	 	 	 t h i s . s l i d e r Z o o m . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   0 ,   4 ,   4 ) ;  
 	 	 	 t h i s . s l i d e r Z o o m . D o c k   =   D o c k S t y l e . L e f t ;  
 	 	 	 t h i s . s l i d e r Z o o m . V a l u e C h a n g e d   + =   t h i s . H a n d l e S l i d e r Z o o m V a l u e C h a n g e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p ( t h i s . s l i d e r Z o o m ,   R e s . S t r i n g s . E n t i t i e s . A c t i o n . Z o o m S l i d e r ) ;  
  
 	 	 	 t h i s . b u t t o n G r i d   =   n e w   I c o n B u t t o n   ( t h i s . t o o l b a r ) ;  
 	 	 	 t h i s . b u t t o n G r i d . I c o n U r i   =   M i s c . I c o n   ( " G r i d " ) ;  
 	 	 	 t h i s . b u t t o n G r i d . B u t t o n S t y l e   =   B u t t o n S t y l e . A c t i v a b l e I c o n ;  
 	 	 	 t h i s . b u t t o n G r i d . A u t o F o c u s   =   f a l s e ;  
 	 	 	 t h i s . b u t t o n G r i d . M a r g i n s   =   n e w   M a r g i n s   ( 1 0 ,   0 ,   0 ,   0 ) ;  
 	 	 	 t h i s . b u t t o n G r i d . D o c k   =   D o c k S t y l e . L e f t ;  
 	 	 	 t h i s . b u t t o n G r i d . C l i c k e d   + =   t h i s . H a n d l e B u t t o n G r i d C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p   ( t h i s . b u t t o n G r i d ,   " G r i l l e   m a g n é t i q u e " ) ;  
  
 	 	 	 t h i s . b u t t o n A d d E n t i t y   =   n e w   I c o n B u t t o n   ( t h i s . t o o l b a r ) ;  
 	 	 	 t h i s . b u t t o n A d d E n t i t y . I c o n U r i   =   M i s c . I c o n   ( " A d d E n t i t y " ) ;  
 	 	 	 t h i s . b u t t o n A d d E n t i t y . A u t o F o c u s   =   f a l s e ;  
 	 	 	 t h i s . b u t t o n A d d E n t i t y . M a r g i n s   =   n e w   M a r g i n s   ( 1 ,   0 ,   0 ,   0 ) ;  
 	 	 	 t h i s . b u t t o n A d d E n t i t y . D o c k   =   D o c k S t y l e . L e f t ;  
 	 	 	 t h i s . b u t t o n A d d E n t i t y . C l i c k e d   + =   t h i s . H a n d l e B u t t o n A d d E n t i t y C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p   ( t h i s . b u t t o n A d d E n t i t y ,   " A j o u t e   u n e   e n t i t é   e x i s t a n t e   d a n s   l e   d e s s i n " ) ;  
  
 	 	 	 t h i s . b u t t o n S a v e A l l B i t m a p s   =   n e w   I c o n B u t t o n   ( t h i s . t o o l b a r ) ;  
 	 	 	 t h i s . b u t t o n S a v e A l l B i t m a p s . I c o n U r i   =   M i s c . I c o n   ( " S a v e A l l B i t m a p s " ) ;  
 	 	 	 t h i s . b u t t o n S a v e A l l B i t m a p s . A u t o F o c u s   =   f a l s e ;  
 	 	 	 t h i s . b u t t o n S a v e A l l B i t m a p s . D o c k   =   D o c k S t y l e . R i g h t ;  
 	 	 	 t h i s . b u t t o n S a v e A l l B i t m a p s . C l i c k e d   + =   t h i s . H a n d l e B u t t o n S a v e A l l B i t m a p s C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p   ( t h i s . b u t t o n S a v e A l l B i t m a p s ,   R e s . S t r i n g s . E n t i t i e s . A c t i o n . S a v e A l l B i t m a p s ) ;  
  
 	 	 	 t h i s . b u t t o n S a v e B i t m a p   =   n e w   I c o n B u t t o n   ( t h i s . t o o l b a r ) ;  
 	 	 	 t h i s . b u t t o n S a v e B i t m a p . I c o n U r i   =   M i s c . I c o n   ( " S a v e B i t m a p " ) ;  
 	 	 	 t h i s . b u t t o n S a v e B i t m a p . A u t o F o c u s   =   f a l s e ;  
 	 	 	 t h i s . b u t t o n S a v e B i t m a p . D o c k   =   D o c k S t y l e . R i g h t ;  
 	 	 	 t h i s . b u t t o n S a v e B i t m a p . C l i c k e d   + =   t h i s . H a n d l e B u t t o n S a v e B i t m a p C l i c k e d ;  
 	 	 	 T o o l T i p . D e f a u l t . S e t T o o l T i p   ( t h i s . b u t t o n S a v e B i t m a p ,   R e s . S t r i n g s . E n t i t i e s . A c t i o n . S a v e B i t m a p ) ;  
  
 	 	 	 t h i s . A r e a S i z e   =   n e w   S i z e   ( 1 0 0 ,   1 0 0 ) ;  
  
 	 	 	 t h i s . e d i t o r . U p d a t e G e o m e t r y ( ) ;  
 	 	 	 t h i s . U p d a t e Z o o m ( ) ;  
 	 	 	 t h i s . U p d a t e A l l ( ) ;  
 	 	 	 t h i s . U p d a t e S u b V i e w ( ) ;  
 	 	 }  
  
 	 	 p r o t e c t e d   o v e r r i d e   v o i d   D i s p o s e ( b o o l   d i s p o s i n g )  
 	 	 {  
 	 	 	 i f   ( d i s p o s i n g )  
 	 	 	 {  
 	 	 	 	 t h i s . e d i t o r . S i z e C h a n g e d   - =   n e w   E v e n t H a n d l e r < D e p e n d e n c y P r o p e r t y C h a n g e d E v e n t A r g s > ( t h i s . H a n d l e E d i t o r S i z e C h a n g e d ) ;  
 	 	 	 	 t h i s . e d i t o r . A r e a S i z e C h a n g e d   - =   t h i s . H a n d l e E d i t o r A r e a S i z e C h a n g e d ;  
 	 	 	 	 t h i s . e d i t o r . A r e a O f f s e t C h a n g e d   - =   t h i s . H a n d l e E d i t o r A r e a O f f s e t C h a n g e d ;  
 	 	 	 	 t h i s . e d i t o r . Z o o m C h a n g e d   - =   t h i s . H a n d l e E d i t o r Z o o m C h a n g e d ;  
  
 	 	 	 	 t h i s . v s c r o l l e r . V a l u e C h a n g e d   - =   t h i s . H a n d l e S c r o l l e r V a l u e C h a n g e d ;  
 	 	 	 	 t h i s . h s c r o l l e r . V a l u e C h a n g e d   - =   t h i s . H a n d l e S c r o l l e r V a l u e C h a n g e d ;  
  
 	 	 	 	 t h i s . g r o u p T o o l b a r . R e s e t B u t t o n . C l i c k e d   - =   t h i s . H a n d l e R e s e t B u t t o n C l i c k e d ;  
  
 	 	 	 	 t h i s . b u t t o n S u b V i e w A . C l i c k e d   - =   t h i s . H a n d l e B u t t o n S u b V i e w C l i c k e d ;  
 	 	 	 	 t h i s . b u t t o n S u b V i e w A . D r a g S t a r t i n g   - =   t h i s . H a n d l e B u t t o n S u b V i e w D r a g S t a r t i n g ;  
 	 	 	 	 t h i s . b u t t o n S u b V i e w A . D r a g E n d i n g   - =   t h i s . H a n d l e B u t t o n S u b V i e w D r a g E n d i n g ;  
 	 	 	 	 t h i s . b u t t o n S u b V i e w B . C l i c k e d   - =   t h i s . H a n d l e B u t t o n S u b V i e w C l i c k e d ;  
 	 	 	 	 t h i s . b u t t o n S u b V i e w B . D r a g S t a r t i n g   - =   t h i s . H a n d l e B u t t o n S u b V i e w D r a g S t a r t i n g ;  
 	 	 	 	 t h i s . b u t t o n S u b V i e w B . D r a g E n d i n g   - =   t h i s . H a n d l e B u t t o n S u b V i e w D r a g E n d i n g ;  
 	 	 	 	 t h i s . b u t t o n S u b V i e w C . C l i c k e d   - =   t h i s . H a n d l e B u t t o n S u b V i e w C l i c k e d ;  
 	 	 	 	 t h i s . b u t t o n S u b V i e w C . D r a g S t a r t i n g   - =   t h i s . H a n d l e B u t t o n S u b V i e w D r a g S t a r t i n g ;  
 	 	 	 	 t h i s . b u t t o n S u b V i e w C . D r a g E n d i n g   - =   t h i s . H a n d l e B u t t o n S u b V i e w D r a g E n d i n g ;  
 	 	 	 	 t h i s . b u t t o n S u b V i e w T . C l i c k e d   - =   t h i s . H a n d l e B u t t o n S u b V i e w C l i c k e d ;  
 	 	 	 	 t h i s . b u t t o n S u b V i e w T . D r a g S t a r t i n g   - =   t h i s . H a n d l e B u t t o n S u b V i e w D r a g S t a r t i n g ;  
 	 	 	 	 t h i s . b u t t o n S u b V i e w T . D r a g E n d i n g   - =   t h i s . H a n d l e B u t t o n S u b V i e w D r a g E n d i n g ;  
 	 	 	 	  
 	 	 	 	 t h i s . b u t t o n Z o o m P a g e . C l i c k e d   - =   t h i s . H a n d l e B u t t o n Z o o m C l i c k e d ;  
 	 	 	 	 t h i s . b u t t o n Z o o m M i n . C l i c k e d   - =   t h i s . H a n d l e B u t t o n Z o o m C l i c k e d ;  
 	 	 	 	 t h i s . b u t t o n Z o o m D e f a u l t . C l i c k e d   - =   t h i s . H a n d l e B u t t o n Z o o m C l i c k e d ;  
 	 	 	 	 t h i s . b u t t o n Z o o m M a x . C l i c k e d   - =   t h i s . H a n d l e B u t t o n Z o o m C l i c k e d ;  
 	 	 	 	  
 	 	 	 	 t h i s . f i e l d Z o o m . C l i c k e d   - =   t h i s . H a n d l e F i e l d Z o o m C l i c k e d ;  
 	 	 	 	 t h i s . s l i d e r Z o o m . V a l u e C h a n g e d   - =   t h i s . H a n d l e S l i d e r Z o o m V a l u e C h a n g e d ;  
  
 	 	 	 	 t h i s . b u t t o n G r i d . C l i c k e d   - =   t h i s . H a n d l e B u t t o n G r i d C l i c k e d ;  
 	 	 	 	 t h i s . b u t t o n S a v e B i t m a p . C l i c k e d   - =   t h i s . H a n d l e B u t t o n S a v e B i t m a p C l i c k e d ;  
 	 	 	 	 t h i s . b u t t o n S a v e A l l B i t m a p s . C l i c k e d   - =   t h i s . H a n d l e B u t t o n S a v e A l l B i t m a p s C l i c k e d ;  
 	 	 	 }  
  
 	 	 	 b a s e . D i s p o s e ( d i s p o s i n g ) ;  
 	 	 }  
  
  
 	 	 p u b l i c   o v e r r i d e   R e s o u r c e A c c e s s . T y p e   R e s o u r c e T y p e  
 	 	 {  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 r e t u r n   R e s o u r c e A c c e s s . T y p e . E n t i t i e s ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p u b l i c   o v e r r i d e   b o o l   H a s U s e f u l V i e w e r W i n d o w  
 	 	 {  
 	 	 	 / / 	 I n d i q u e   s i   c e t t e   v u e   a   l ' u t i l i t é   d ' u n e   f e n ê t r e   s u p p l é m e n t a i r e .  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 r e t u r n   t r u e ;  
 	 	 	 }  
 	 	 }  
  
  
 	 	 p u b l i c   b o o l   E d i t E x p r e s s i o n ( D r u i d   f i e l d I d )  
 	 	 {  
 	 	 	 / / 	 E d i t e   l ' e x p r e s s i o n   d ' u n   c h a m p .  
 	 	 	 i f   ( t h i s . e d i t o r   = =   n u l l )  
 	 	 	 {  
 	 	 	 	 r e t u r n   f a l s e ;  
 	 	 	 }  
  
 	 	 	 r e t u r n   t h i s . e d i t o r . E d i t E x p r e s s i o n ( f i e l d I d ) ;  
 	 	 }  
  
  
 	 	 p r i v a t e   S i z e   A r e a S i z e  
 	 	 {  
 	 	 	 / / 	 D i m e n s i o n s   d e   l a   s u r f a c e   p o u r   r e p r é s e n t e r   l e s   b o î t e s   e t   l e s   l i a i s o n s .  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 r e t u r n   t h i s . a r e a S i z e ;  
 	 	 	 }  
 	 	 	 s e t  
 	 	 	 {  
 	 	 	 	 i f   ( t h i s . a r e a S i z e   ! =   v a l u e )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . a r e a S i z e   =   v a l u e ;  
  
 	 	 	 	 	 t h i s . e d i t o r . A r e a S i z e   =   t h i s . a r e a S i z e ;  
 	 	 	 	 	 t h i s . U p d a t e S c r o l l e r ( ) ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
 	 	 p u b l i c   i n t   S u b V i e w  
 	 	 {  
 	 	 	 / / 	 S o u s - v u e   u t i l i s é e   p o u r   r e p r é s e n t e r   l e s   b o î t e s   e t   l e s   l i a i s o n s .  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 r e t u r n   E n t i t i e s . s u b V i e w ;  
 	 	 	 }  
 	 	 	 s e t  
 	 	 	 {  
 	 	 	 	 i f   ( E n t i t i e s . s u b V i e w   ! =   v a l u e )  
 	 	 	 	 {  
 	 	 	 	 	 i f   ( ! t h i s . d e s i g n e r A p p l i c a t i o n . T e r m i n a t e ( ) )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 r e t u r n ;  
 	 	 	 	 	 }  
  
 	 	 	 	 	 E n t i t i e s . s u b V i e w   =   v a l u e ;  
 	 	 	 	 	 t h i s . d e s i g n e r A p p l i c a t i o n . L o c a t o r F i x ( ) ;  
  
 	 	 	 	 	 t h i s . U p d a t e S u b V i e w ( ) ;  
 	 	 	 	 	 t h i s . U p d a t e T i t l e ( ) ;  
 	 	 	 	 	 t h i s . U p d a t e E d i t ( ) ;  
 	 	 	 	 	 t h i s . U p d a t e C o l o r ( ) ;  
 	 	 	 	 	 t h i s . U p d a t e M o d i f i c a t i o n s C u l t u r e ( ) ;  
 	 	 	 	 	 t h i s . U p d a t e C o m m a n d s ( ) ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
 	 	 p u b l i c   s t a t i c   s t r i n g   S u b V i e w N a m e ( i n t   s u b V i e w )  
 	 	 {  
 	 	 	 / / 	 R e t o u r n e   l e   n o m   d e   l a   s o u s - v u e   u t i l i s é e .  
 	 	 	 s w i t c h   ( s u b V i e w )  
 	 	 	 {  
 	 	 	 	 c a s e   0 :       r e t u r n   R e s . S t r i n g s . E n t i t i e s . S u b V i e w . Q u i c k . A ;  
 	 	 	 	 c a s e   1 :       r e t u r n   R e s . S t r i n g s . E n t i t i e s . S u b V i e w . Q u i c k . B ;  
 	 	 	 	 c a s e   2 :       r e t u r n   R e s . S t r i n g s . E n t i t i e s . S u b V i e w . Q u i c k . C ;  
 	 	 	 	 d e f a u l t :     r e t u r n   R e s . S t r i n g s . E n t i t i e s . S u b V i e w . Q u i c k . T ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p u b l i c   d o u b l e   Z o o m  
 	 	 {  
 	 	 	 / / 	 Z o o m   p o u r   r e p r é s e n t e r   l e s   b o î t e s   e t   l e s   l i a i s o n s .  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 i f   ( E n t i t i e s . i s Z o o m P a g e )  
 	 	 	 	 {  
 	 	 	 	 	 r e t u r n   t h i s . Z o o m P a g e ;  
 	 	 	 	 }  
 	 	 	 	 e l s e  
 	 	 	 	 {  
 	 	 	 	 	 r e t u r n   E n t i t i e s . z o o m ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 	 s e t  
 	 	 	 {  
 	 	 	 	 i f   ( E n t i t i e s . z o o m   ! =   v a l u e )  
 	 	 	 	 {  
 	 	 	 	 	 E n t i t i e s . z o o m   =   v a l u e ;  
  
 	 	 	 	 	 t h i s . U p d a t e Z o o m ( ) ;  
 	 	 	 	 	 t h i s . U p d a t e S c r o l l e r ( ) ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   d o u b l e   Z o o m P a g e  
 	 	 {  
 	 	 	 / / 	 R e t o u r n e   l e   z o o m   p e r m e t t a n t   d e   v o i r   t o u t e   l a   s u r f a c e   d e   t r a v a i l .  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 d o u b l e   z x   =   t h i s . e d i t o r . C l i e n t . B o u n d s . W i d t h     /   t h i s . e d i t o r . A r e a S i z e . W i d t h ;  
 	 	 	 	 d o u b l e   z y   =   t h i s . e d i t o r . C l i e n t . B o u n d s . H e i g h t   /   t h i s . e d i t o r . A r e a S i z e . H e i g h t ;  
 	 	 	 	 d o u b l e   z o o m   =   S y s t e m . M a t h . M i n ( z x ,   z y ) ;  
  
 	 	 	 	 z o o m   =   S y s t e m . M a t h . M a x ( z o o m ,   E n t i t i e s . z o o m M i n ) ;  
 	 	 	 	 z o o m   =   S y s t e m . M a t h . M i n ( z o o m ,   E n t i t i e s . z o o m M a x ) ;  
 	 	 	 	  
 	 	 	 	 z o o m   =   S y s t e m . M a t h . F l o o r ( z o o m * 1 0 0 ) / 1 0 0 ;     / /   4 5 . 8 %   - >   4 6 %  
 	 	 	 	 r e t u r n   z o o m ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r o t e c t e d   o v e r r i d e   v o i d   U p d a t e T i t l e ( )  
 	 	 {  
 	 	 	 / / 	 M e t   à   j o u r   l e   t i t r e   e n   d e s s u s   d e   l a   z o n e   s c r o l l a b l e .  
 	 	 	 b a s e . U p d a t e T i t l e ( ) ;  
  
 	 	 	 f o r e a c h   ( E n t i t i e s E d i t o r . O b j e c t B o x   b o x   i n   t h i s . e d i t o r . B o x e s )  
 	 	 	 {  
 	 	 	 	 b o x . U p d a t e T i t l e ( ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   U p d a t e T o p F r a m e ( )  
 	 	 {  
 	 	 	 b o o l   i s F u l l S c r e e n   =   ( t h i s . d e s i g n e r A p p l i c a t i o n . D i s p l a y M o d e S t a t e   = =   D e s i g n e r A p p l i c a t i o n . D i s p l a y M o d e . F u l l S c r e e n ) ;  
 	 	 	 b o o l   i s W i n d o w           =   ( t h i s . d e s i g n e r A p p l i c a t i o n . D i s p l a y M o d e S t a t e   = =   D e s i g n e r A p p l i c a t i o n . D i s p l a y M o d e . W i n d o w ) ;  
  
 	 	 	 t h i s . s h o w H i d e T o p F r a m e B u t t o n . G l y p h S h a p e   =   t h i s . s h o w T o p F r a m e   ?   G l y p h S h a p e . T r i a n g l e U p   :   G l y p h S h a p e . T r i a n g l e D o w n ;  
  
 	 	 	 t h i s . l a s t G r o u p . V i s i b i l i t y   =   t h i s . s h o w T o p F r a m e ;  
 	 	 	 t h i s . h s p l i t t e r . V i s i b i l i t y   =   t h i s . s h o w T o p F r a m e   & &   ! i s F u l l S c r e e n   & &   ! i s W i n d o w ;  
  
 	 	 	 i f   ( i s W i n d o w )  
 	 	 	 {  
 	 	 	 	 t h i s . e d i t o r G r o u p . M a r g i n s   =   n e w   M a r g i n s   ( 0 ) ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . e d i t o r G r o u p . M a r g i n s   =   n e w   M a r g i n s   ( 0 ,   0 ,   t h i s . s h o w T o p F r a m e   ?   0   :   1 0 ,   0 ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   U p d a t e Z o o m ( )  
 	 	 {  
 	 	 	 / / 	 M e t   à   j o u r   t o u t   c e   q u i   d é p e n d   d u   z o o m .  
 	 	 	 t h i s . e d i t o r . Z o o m   =   t h i s . Z o o m ;  
  
 	 	 	 t h i s . f i e l d Z o o m . T e x t   =   s t r i n g . C o n c a t ( S y s t e m . M a t h . F l o o r ( t h i s . Z o o m * 1 0 0 ) . T o S t r i n g ( ) ,   " % " ) ;  
  
 	 	 	 t h i s . i g n o r e C h a n g e   =   t r u e ;  
 	 	 	 t h i s . s l i d e r Z o o m . V a l u e   =   ( d e c i m a l )   t h i s . Z o o m ;  
 	 	 	 t h i s . i g n o r e C h a n g e   =   f a l s e ;  
  
 	 	 	 t h i s . b u t t o n Z o o m P a g e . A c t i v e S t a t e         =   ( E n t i t i e s . i s Z o o m P a g e                             )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 t h i s . b u t t o n Z o o m M i n . A c t i v e S t a t e           =   ( t h i s . Z o o m   = =   E n t i t i e s . z o o m M i n         )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 t h i s . b u t t o n Z o o m D e f a u l t . A c t i v e S t a t e   =   ( t h i s . Z o o m   = =   E n t i t i e s . z o o m D e f a u l t )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 t h i s . b u t t o n Z o o m M a x . A c t i v e S t a t e           =   ( t h i s . Z o o m   = =   E n t i t i e s . z o o m M a x         )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   U p d a t e S c r o l l e r ( )  
 	 	 {  
 	 	 	 / / 	 M e t   à   j o u r   l e s   a s c e n s e u r s ,   e n   f o n c t i o n   d u   z o o m   c o u r a n t   e t   d e   l a   t a i l l e   d e   l ' é d i t e u r .  
 	 	 	 d o u b l e   w   =   t h i s . a r e a S i z e . W i d t h * t h i s . Z o o m   -   t h i s . e d i t o r . C l i e n t . S i z e . W i d t h ;  
 	 	 	 i f   ( w   < =   0   | |   t h i s . e d i t o r . C l i e n t . S i z e . W i d t h   < =   0 )  
 	 	 	 {  
 	 	 	 	 t h i s . h s c r o l l e r . E n a b l e   =   f a l s e ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . h s c r o l l e r . E n a b l e   =   t r u e ;  
 	 	 	 	 t h i s . h s c r o l l e r . M i n V a l u e   =   ( d e c i m a l )   0 ;  
 	 	 	 	 t h i s . h s c r o l l e r . M a x V a l u e   =   ( d e c i m a l )   w ;  
 	 	 	 	 t h i s . h s c r o l l e r . S m a l l C h a n g e   =   ( d e c i m a l )   ( w / 1 0 ) ;  
 	 	 	 	 t h i s . h s c r o l l e r . L a r g e C h a n g e   =   ( d e c i m a l )   ( w / 5 ) ;  
 	 	 	 	 t h i s . h s c r o l l e r . V i s i b l e R a n g e R a t i o   =   ( d e c i m a l )   ( t h i s . e d i t o r . C l i e n t . S i z e . W i d t h   /   ( t h i s . a r e a S i z e . W i d t h * t h i s . Z o o m ) ) ;  
 	 	 	 }  
  
 	 	 	 d o u b l e   h   =   t h i s . a r e a S i z e . H e i g h t * t h i s . Z o o m   -   t h i s . e d i t o r . C l i e n t . S i z e . H e i g h t ;  
 	 	 	 i f   ( h   < =   0   | |   t h i s . e d i t o r . C l i e n t . S i z e . H e i g h t   < =   0 )  
 	 	 	 {  
 	 	 	 	 t h i s . v s c r o l l e r . E n a b l e   =   f a l s e ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . v s c r o l l e r . E n a b l e   =   t r u e ;  
 	 	 	 	 t h i s . v s c r o l l e r . M i n V a l u e   =   ( d e c i m a l )   0 ;  
 	 	 	 	 t h i s . v s c r o l l e r . M a x V a l u e   =   ( d e c i m a l )   h ;  
 	 	 	 	 t h i s . v s c r o l l e r . S m a l l C h a n g e   =   ( d e c i m a l )   ( h / 1 0 ) ;  
 	 	 	 	 t h i s . v s c r o l l e r . L a r g e C h a n g e   =   ( d e c i m a l )   ( h / 5 ) ;  
 	 	 	 	 t h i s . v s c r o l l e r . V i s i b l e R a n g e R a t i o   =   ( d e c i m a l )   ( t h i s . e d i t o r . C l i e n t . S i z e . H e i g h t   /   ( t h i s . a r e a S i z e . H e i g h t * t h i s . Z o o m ) ) ;  
 	 	 	 }  
  
 	 	 	 t h i s . e d i t o r . I s S c r o l l e r E n a b l e   =   t h i s . h s c r o l l e r . E n a b l e   | |   t h i s . v s c r o l l e r . E n a b l e ;  
 	 	 	 t h i s . H a n d l e S c r o l l e r V a l u e C h a n g e d ( n u l l ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   U p d a t e G r i d ( )  
 	 	 {  
 	 	 	 t h i s . b u t t o n G r i d . A c t i v e S t a t e   =   t h i s . e d i t o r . G r i d   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 }  
  
  
 	 	 p r o t e c t e d   o v e r r i d e   v o i d   U p d a t e E d i t ( )  
 	 	 {  
 	 	 	 / / 	 M e t   à   j o u r   l e s   l i g n e s   é d i t a b l e s   e n   f o n c t i o n   d e   l a   s é l e c t i o n   d a n s   l e   t a b l e a u .  
 	 	 	 b a s e . U p d a t e E d i t ( ) ;  
  
 	 	 	 i f   ( ! t h i s . D e s e r i a l i z e ( t r u e ) )  
 	 	 	 {  
 	 	 	 	 C u l t u r e M a p   i t e m   =   t h i s . a c c e s s . C o l l e c t i o n V i e w . C u r r e n t I t e m   a s   C u l t u r e M a p ;  
  
 	 	 	 	 i f   ( i t e m   ! =   n u l l )  
 	 	 	 	 {  
 	 	 	 	 	 v a r   b o x   =   n e w   E n t i t i e s E d i t o r . O b j e c t B o x ( t h i s . e d i t o r ) ;  
 	 	 	 	 	 b o x . I s R o o t   =   t r u e ;     / /   l a   p r e m i è r e   b o î t e   e s t   t o u j o u r s   l a   b o î t e   r a c i n e  
 	 	 	 	 	 b o x . S e t C o n t e n t ( i t e m ) ;  
 	 	 	 	 	 t h i s . e d i t o r . A d d B o x ( b o x ) ;  
 	 	 	 	 }  
  
 	 	 	 	 t h i s . e d i t o r . C r e a t e C o n n e c t i o n s ( ) ;  
 	 	 	 	 t h i s . e d i t o r . U p d a t e A f t e r G e o m e t r y C h a n g e d ( n u l l ) ;  
 	 	 	 }  
  
 	 	 	 t h i s . U p d a t e R e s e t ( ) ;  
 	 	 	 t h i s . Z o o m   =   t h i s . Z o o m ;  
 	 	 }  
  
 	 	 p u b l i c   v o i d   U p d a t e R e s e t ( )  
 	 	 {  
 	 	 	 / / 	 M e t   à   j o u r   l e   b o u t o n   ' r e s e t ' .  
 	 	 	 C u l t u r e M a p   i t e m   =   t h i s . a c c e s s . C o l l e c t i o n V i e w . C u r r e n t I t e m   a s   C u l t u r e M a p ;  
 	 	 	 C u l t u r e M a p S o u r c e   s o u r c e   =   t h i s . a c c e s s . G e t C u l t u r e M a p S o u r c e ( i t e m ) ;  
 	 	 	 t h i s . C o l o r i z e R e s e t B o x ( t h i s . g r o u p T o o l b a r ,   s o u r c e ,   f a l s e ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   U p d a t e S u b V i e w ( )  
 	 	 {  
 	 	 	 / / 	 M e t   à   j o u r   l e   b o u t o n   s é l e c t i o n n é   p o u r   l a   s o u s - v u e .  
 	 	 	 t h i s . b u t t o n S u b V i e w A . A c t i v e S t a t e   =   ( t h i s . S u b V i e w   = =   0 )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 t h i s . b u t t o n S u b V i e w B . A c t i v e S t a t e   =   ( t h i s . S u b V i e w   = =   1 )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 t h i s . b u t t o n S u b V i e w C . A c t i v e S t a t e   =   ( t h i s . S u b V i e w   = =   2 )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 	 t h i s . b u t t o n S u b V i e w T . A c t i v e S t a t e   =   ( t h i s . S u b V i e w   = =   3 )   ?   A c t i v e S t a t e . Y e s   :   A c t i v e S t a t e . N o ;  
 	 	 }  
  
 	 	 p u b l i c   o v e r r i d e   b o o l   T e r m i n a t e ( b o o l   s o f t )  
 	 	 {  
 	 	 	 / / 	 T e r m i n e   l e   t r a v a i l   s u r   u n e   r e s s o u r c e ,   a v a n t   d e   p a s s e r   à   u n e   a u t r e .  
 	 	 	 / / 	 S i   s o f t   =   t r u e ,   o n   s é r i a l i s e   t e m p o r a i r e m e n t   s a n s   p o s e r   d e   q u e s t i o n .  
 	 	 	 / / 	 R e t o u r n e   f a l s e   s i   l ' u t i l i s a t e u r   a   c h o i s i   " a n n u l e r " .  
 	 	 	 b o o l   d i r t y   =   t h i s . m o d u l e . A c c e s s E n t i t i e s . I s L o c a l D i r t y ;  
  
 	 	 	 b a s e . T e r m i n a t e ( s o f t ) ;  
  
 	 	 	 i f   ( d i r t y )  
 	 	 	 {  
 	 	 	 	 i f   ( s o f t )  
 	 	 	 	 {  
 	 	 	 	 	 i f   ( t h i s . d r u i d T o S e r i a l i z e . I s V a l i d )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 E n t i t i e s . s o f t S e r i a l i z e   =   t h i s . e d i t o r . S e r i a l i z e ( ) ;  
 	 	 	 	 	 }  
 	 	 	 	 	 e l s e  
 	 	 	 	 	 {  
 	 	 	 	 	 	 E n t i t i e s . s o f t S e r i a l i z e   =   n u l l ;  
 	 	 	 	 	 }  
 	 	 	 	 }  
 	 	 	 	 e l s e  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . S e r i a l i z e ( ) ;  
                                         t h i s . m o d u l e . A c c e s s E n t i t i e s . P e r s i s t C h a n g e s ( ) ;  
 	 	 	 	 	 t h i s . m o d u l e . A c c e s s E n t i t i e s . C l e a r L o c a l D i r t y ( ) ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 r e t u r n   t r u e ;  
 	 	 }  
  
  
 	 	 p u b l i c   o v e r r i d e   s t r i n g   G e t I t e m N a m e ( C u l t u r e M a p   i t e m )  
 	 	 {  
 	 	 	 / / 	 R e t o u r n e   l e   n o m   d ' u n   i t e m .  
 	 	 	 / / 	 S ' i l   s ' a g i t   d ' u n e   e n t i t é   d o n t   o n   g é n è r e   l e   s c h é m a ,   l e   n o m   e s t   e n   g r a s .  
 	 	 	 s t r i n g   i m a g e   =   M i s c . I m a g e   ( t h i s . G e t I c o n E n t i t y   ( i t e m ) ) ;  
 	 	 	 s t r i n g   n a m e   =   i t e m . N a m e ;  
  
 	 	 	 r e t u r n   s t r i n g . C o n c a t   ( i m a g e ,   "       " ,   n a m e ) ;  
 	 	 }  
  
 	 	 p r i v a t e   s t r i n g   G e t I c o n E n t i t y ( C u l t u r e M a p   i t e m )  
 	 	 {  
 	 	 	 S t r u c t u r e d D a t a   d a t a   =   i t e m . G e t C u l t u r e D a t a   ( R e s o u r c e s . D e f a u l t T w o L e t t e r I S O L a n g u a g e N a m e ) ;  
  
 	 	 	 v a r   t y p e C l a s s   =   ( S t r u c t u r e d T y p e C l a s s )   d a t a . G e t V a l u e   ( S u p p o r t . R e s . F i e l d s . R e s o u r c e S t r u c t u r e d T y p e . C l a s s ) ;  
 	 	 	 v a r   f l a g s   =   d a t a . G e t V a l u e O r D e f a u l t < S t r u c t u r e d T y p e F l a g s >   ( S u p p o r t . R e s . F i e l d s . R e s o u r c e S t r u c t u r e d T y p e . F l a g s ) ;  
  
 	 	 	 s t r i n g   i c o n   =   " E n t i t y N o r m a l " ;  
  
 	 	 	 i f   ( t y p e C l a s s   = =   S t r u c t u r e d T y p e C l a s s . I n t e r f a c e )  
 	 	 	 {  
 	 	 	 	 i c o n   =   " E n t i t y I n t e r f a c e " ;  
 	 	 	 }  
  
 	 	 	 i f   ( ( f l a g s   &   S t r u c t u r e d T y p e F l a g s . A b s t r a c t C l a s s )   ! =   0 )  
 	 	 	 {  
 	 	 	 	 i c o n   =   " E n t i t y A b s t r a c t " ;  
 	 	 	 }  
  
 	 	 	 i f   ( ( f l a g s   &   S t r u c t u r e d T y p e F l a g s . G e n e r a t e S c h e m a )   ! =   0 )  
 	 	 	 {  
 	 	 	 	 i c o n   =   " E n t i t y S c h e m a " ;  
 	 	 	 }  
  
 	 	 	 r e t u r n   i c o n ;  
 	 	 }  
  
 	  
 	 	 p r i v a t e   v o i d   D r a g S u b V i e w ( i n t   s r c S u b V i e w ,   i n t   d s t S u b V i e w )  
 	 	 {  
 	 	 	 / / 	 E f f e c t u e   l e   d r a g   &   d r o p   d ' u n e   s o u s - v u e   d a n s   u n e   a u t r e .  
 	 	 	 s t r i n g   h e a d e r   =   s t r i n g . F o r m a t ( R e s . S t r i n g s . E n t i t i e s . Q u e s t i o n . S u b V i e w . B a s e ,   t h i s . n a m e T o S e r i a l i z e ) ;  
  
 	 	 	 L i s t < s t r i n g >   q u e s t i o n s   =   n e w   L i s t < s t r i n g > ( ) ;  
 	 	 	 q u e s t i o n s . A d d ( C o n f i r m a t i o n B u t t o n . F o r m a t C o n t e n t ( R e s . S t r i n g s . E n t i t i e s . Q u e s t i o n . S u b V i e w . Q u i c k . C o p y ,   s t r i n g . F o r m a t ( R e s . S t r i n g s . E n t i t i e s . Q u e s t i o n . S u b V i e w . L o n g . C o p y ,   E n t i t i e s . S u b V i e w N a m e ( s r c S u b V i e w ) ,   E n t i t i e s . S u b V i e w N a m e ( d s t S u b V i e w ) ) ) ) ;  
 	 	 	 q u e s t i o n s . A d d ( C o n f i r m a t i o n B u t t o n . F o r m a t C o n t e n t ( R e s . S t r i n g s . E n t i t i e s . Q u e s t i o n . S u b V i e w . Q u i c k . S w a p ,   s t r i n g . F o r m a t ( R e s . S t r i n g s . E n t i t i e s . Q u e s t i o n . S u b V i e w . L o n g . S w a p ,   E n t i t i e s . S u b V i e w N a m e ( s r c S u b V i e w ) ,   E n t i t i e s . S u b V i e w N a m e ( d s t S u b V i e w ) ) ) ) ;  
  
 	 	 	 v a r   r e s u l t   =   t h i s . d e s i g n e r A p p l i c a t i o n . D i a l o g C o n f i r m a t i o n   ( h e a d e r ,   q u e s t i o n s ,   t r u e ) ;  
  
 	 	 	 i f   ( r e s u l t   = =   E p s i t e c . C o m m o n . D i a l o g s . D i a l o g R e s u l t . C a n c e l )  
 	 	 	 {  
 	 	 	 	 r e t u r n ;  
 	 	 	 }  
  
 	 	 	 i f   ( r e s u l t   = =   E p s i t e c . C o m m o n . D i a l o g s . D i a l o g R e s u l t . A n s w e r 1 )     / /   c o p i e r   ?  
 	 	 	 {  
 	 	 	 	 i f   ( s r c S u b V i e w   = =   t h i s . S u b V i e w )     / /   d r a g   d e   l a   s o u s - v u e   c o u r a n t e   ?  
 	 	 	 	 {  
 	 	 	 	 	 s t r i n g   d a t a   =   t h i s . e d i t o r . S e r i a l i z e ( ) ;  
 	 	 	 	 	 E n t i t i e s . S e t S e r i a l i z e d D a t a ( t h i s . a c c e s s . A c c e s s o r ,   t h i s . C u r r e n t D r u i d ,   d s t S u b V i e w ,   d a t a ) ;  
 	 	 	 	 }  
 	 	 	 	 e l s e   i f   ( d s t S u b V i e w   = =   t h i s . S u b V i e w )     / /   d r a g   d a n s   l a   s o u s - v u e   c o u r a n t e   ?  
 	 	 	 	 {  
 	 	 	 	 	 s t r i n g   d a t a   =   E n t i t i e s . G e t S e r i a l i z e d D a t a ( t h i s . a c c e s s . A c c e s s o r ,   t h i s . C u r r e n t D r u i d ,   s r c S u b V i e w ) ;  
 	 	 	 	 	 E n t i t i e s . S e t S e r i a l i z e d D a t a ( t h i s . a c c e s s . A c c e s s o r ,   t h i s . C u r r e n t D r u i d ,   d s t S u b V i e w ,   d a t a ) ;  
 	 	 	 	 	 t h i s . U p d a t e E d i t ( ) ;  
 	 	 	 	 }  
 	 	 	 	 e l s e     / /   d r a g   d ' u n e   s o u s - v u e   c a c h é e   v e r s   u n e   a u t r e   c a c h é e   ?  
 	 	 	 	 {  
 	 	 	 	 	 s t r i n g   d a t a   =   E n t i t i e s . G e t S e r i a l i z e d D a t a ( t h i s . a c c e s s . A c c e s s o r ,   t h i s . C u r r e n t D r u i d ,   s r c S u b V i e w ) ;  
 	 	 	 	 	 E n t i t i e s . S e t S e r i a l i z e d D a t a ( t h i s . a c c e s s . A c c e s s o r ,   t h i s . C u r r e n t D r u i d ,   d s t S u b V i e w ,   d a t a ) ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 i f   ( r e s u l t   = =   E p s i t e c . C o m m o n . D i a l o g s . D i a l o g R e s u l t . A n s w e r 2 )     / /   p e r m u t e r   ?  
 	 	 	 {  
 	 	 	 	 i f   ( s r c S u b V i e w   = =   t h i s . S u b V i e w )     / /   d r a g   d e   l a   s o u s - v u e   c o u r a n t e   ?  
 	 	 	 	 {  
 	 	 	 	 	 s t r i n g   s r c D a t a   =   t h i s . e d i t o r . S e r i a l i z e ( ) ;  
 	 	 	 	 	 s t r i n g   d s t D a t a   =   E n t i t i e s . G e t S e r i a l i z e d D a t a ( t h i s . a c c e s s . A c c e s s o r ,   t h i s . C u r r e n t D r u i d ,   d s t S u b V i e w ) ;  
 	 	 	 	 	 E n t i t i e s . S e t S e r i a l i z e d D a t a ( t h i s . a c c e s s . A c c e s s o r ,   t h i s . C u r r e n t D r u i d ,   d s t S u b V i e w ,   s r c D a t a ) ;  
 	 	 	 	 	 E n t i t i e s . S e t S e r i a l i z e d D a t a ( t h i s . a c c e s s . A c c e s s o r ,   t h i s . C u r r e n t D r u i d ,   s r c S u b V i e w ,   d s t D a t a ) ;  
 	 	 	 	 }  
 	 	 	 	 e l s e   i f   ( d s t S u b V i e w   = =   t h i s . S u b V i e w )     / /   d r a g   d a n s   l a   s o u s - v u e   c o u r a n t e   ?  
 	 	 	 	 {  
 	 	 	 	 	 s t r i n g   s r c D a t a   =   E n t i t i e s . G e t S e r i a l i z e d D a t a ( t h i s . a c c e s s . A c c e s s o r ,   t h i s . C u r r e n t D r u i d ,   s r c S u b V i e w ) ;  
 	 	 	 	 	 s t r i n g   d s t D a t a   =   t h i s . e d i t o r . S e r i a l i z e ( ) ;  
 	 	 	 	 	 E n t i t i e s . S e t S e r i a l i z e d D a t a ( t h i s . a c c e s s . A c c e s s o r ,   t h i s . C u r r e n t D r u i d ,   d s t S u b V i e w ,   s r c D a t a ) ;  
 	 	 	 	 	 E n t i t i e s . S e t S e r i a l i z e d D a t a ( t h i s . a c c e s s . A c c e s s o r ,   t h i s . C u r r e n t D r u i d ,   s r c S u b V i e w ,   d s t D a t a ) ;  
 	 	 	 	 	 t h i s . U p d a t e E d i t ( ) ;  
 	 	 	 	 }  
 	 	 	 	 e l s e     / /   d r a g   d ' u n e   s o u s - v u e   c a c h é e   v e r s   u n e   a u t r e   c a c h é e   ?  
 	 	 	 	 {  
 	 	 	 	 	 s t r i n g   s r c D a t a   =   E n t i t i e s . G e t S e r i a l i z e d D a t a ( t h i s . a c c e s s . A c c e s s o r ,   t h i s . C u r r e n t D r u i d ,   s r c S u b V i e w ) ;  
 	 	 	 	 	 s t r i n g   d s t D a t a   =   E n t i t i e s . G e t S e r i a l i z e d D a t a ( t h i s . a c c e s s . A c c e s s o r ,   t h i s . C u r r e n t D r u i d ,   d s t S u b V i e w ) ;  
 	 	 	 	 	 E n t i t i e s . S e t S e r i a l i z e d D a t a ( t h i s . a c c e s s . A c c e s s o r ,   t h i s . C u r r e n t D r u i d ,   d s t S u b V i e w ,   s r c D a t a ) ;  
 	 	 	 	 	 E n t i t i e s . S e t S e r i a l i z e d D a t a ( t h i s . a c c e s s . A c c e s s o r ,   t h i s . C u r r e n t D r u i d ,   s r c S u b V i e w ,   d s t D a t a ) ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 t h i s . S u b V i e w   =   d s t S u b V i e w ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   S e r i a l i z e ( )  
 	 	 {  
 	 	 	 / / 	 S é r i a l i s e   l e s   d o n n é e s .  
 	 	 	 i f   ( t h i s . d r u i d T o S e r i a l i z e . I s V a l i d )  
 	 	 	 {  
 	 	 	 	 s t r i n g   d a t a   =   t h i s . e d i t o r . S e r i a l i z e ( ) ;  
 	 	 	 	 E n t i t i e s . S e t S e r i a l i z e d D a t a ( t h i s . a c c e s s . A c c e s s o r ,   t h i s . d r u i d T o S e r i a l i z e ,   t h i s . S u b V i e w ,   d a t a ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   b o o l   D e s e r i a l i z e ( b o o l   o p t i m i z e d )  
 	 	 {  
 	 	 	 / / 	 D é s é r i a l i s e   l e s   d o n n é e s   s é r i a l i s é e s .   R e t o u r n e   f a l s e   s ' i l   n ' e x i s t e   a u c u n e   d o n n é e   s é r i a l i s é e .  
 	 	 	 t h i s . n a m e T o S e r i a l i z e   =   t h i s . C u r r e n t N a m e ;  
 	 	 	 t h i s . d r u i d T o S e r i a l i z e   =   t h i s . C u r r e n t D r u i d ;  
  
 	 	 	 i f   ( E n t i t i e s . s o f t S e r i a l i z e   = =   n u l l )  
 	 	 	 {  
 	 	 	 	 i f   ( o p t i m i z e d )  
 	 	 	 	 {  
 	 	 	 	 	 i f   ( t h i s . d e s e r i a l i z e d D r u i d   = =   t h i s . d r u i d T o S e r i a l i z e   & &   t h i s . d e s e r i a l i z e d S u b V i e w   = =   t h i s . S u b V i e w )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 r e t u r n   t r u e ;  
 	 	 	 	 	 }  
  
 	 	 	 	 	 t h i s . d e s e r i a l i z e d D r u i d   =   t h i s . d r u i d T o S e r i a l i z e ;  
 	 	 	 	 	 t h i s . d e s e r i a l i z e d S u b V i e w   =   t h i s . S u b V i e w ;  
  
 	 	 	 	 	 t h i s . e d i t o r . C l e a r   ( ) ;  
 	 	 	 	 }  
  
 	 	 	 	 s t r i n g   d a t a   =   E n t i t i e s . G e t S e r i a l i z e d D a t a   ( t h i s . a c c e s s . A c c e s s o r ,   t h i s . d r u i d T o S e r i a l i z e ,   t h i s . S u b V i e w ) ;  
 	 	 	 	 i f   ( d a t a   = =   n u l l )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . m o d u l e . A c c e s s E n t i t i e s . C l e a r L o c a l D i r t y ( ) ;  
 	 	 	 	 	 r e t u r n   f a l s e ;  
 	 	 	 	 }  
 	 	 	 	 e l s e  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . e d i t o r . D e s e r i a l i z e ( d a t a ) ;  
 	 	 	 	 	 t h i s . m o d u l e . A c c e s s E n t i t i e s . C l e a r L o c a l D i r t y ( ) ;  
 	 	 	 	 	 r e t u r n   t r u e ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 i f   ( o p t i m i z e d )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . e d i t o r . C l e a r   ( ) ;  
 	 	 	 	 }  
  
 	 	 	 	 i f   ( t h i s . m o d u l e . A c c e s s E n t i t i e s . I s L o c a l D i r t y )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . m o d u l e . A c c e s s E n t i t i e s . S e t L o c a l D i r t y ( ) ;  
 	 	 	 	 }  
 	 	 	 	 e l s e  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . m o d u l e . A c c e s s E n t i t i e s . C l e a r L o c a l D i r t y ( ) ;  
 	 	 	 	 }  
  
 	 	 	 	 t h i s . e d i t o r . D e s e r i a l i z e ( E n t i t i e s . s o f t S e r i a l i z e ) ;  
  
 	 	 	 	 E n t i t i e s . s o f t S e r i a l i z e   =   n u l l ;  
 	 	 	 	 r e t u r n   t r u e ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   s t a t i c   v o i d   S e t S e r i a l i z e d D a t a ( I R e s o u r c e A c c e s s o r   a c c e s s o r ,   D r u i d   d r u i d ,   i n t   s u b V i e w ,   s t r i n g   d a t a )  
 	 	 {  
 	 	 	 / / 	 S é r i a l i s e   d e s   d o n n é e s .   d a t a   v a u t   n u l l   s ' i l   f a u t   e f f a c e r   l e s   d o n n é e s   s é r i a l i s é e s .  
 	 	 	 C u l t u r e M a p   r e s o u r c e   =   a c c e s s o r . C o l l e c t i o n [ d r u i d ] ;  
 	 	 	  
 	 	 	 i f   ( r e s o u r c e   ! =   n u l l )  
 	 	 	 {  
 	 	 	 	 S t r u c t u r e d D a t a   r e c o r d   =   r e s o u r c e . G e t C u l t u r e D a t a ( R e s o u r c e s . D e f a u l t T w o L e t t e r I S O L a n g u a g e N a m e ) ;  
 	 	 	 	 s t r i n g   k e y   =   s u b V i e w . T o S t r i n g ( S y s t e m . G l o b a l i z a t i o n . C u l t u r e I n f o . I n v a r i a n t C u l t u r e ) ;  
 	 	 	 	 D i c t i o n a r y < s t r i n g ,   s t r i n g >   d i c t   =   E n t i t i e s . G e t S e r i a l i z e d L a y o u t s ( r e c o r d ) ;  
 	 	 	 	  
 	 	 	 	 i f   ( s t r i n g . I s N u l l O r E m p t y ( d a t a ) )  
 	 	 	 	 {  
 	 	 	 	 	 i f   ( d i c t . C o n t a i n s K e y ( k e y ) )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 d i c t . R e m o v e ( k e y ) ;  
 	 	 	 	 	 }  
 	 	 	 	 }  
 	 	 	 	 e l s e  
 	 	 	 	 {  
 	 	 	 	 	 / / 	 S u p p r i m e   l ' e n - t ê t e   X M L   < ? x m l . . . ? >   q u i   e s t   i n u t i l e   i c i ;   o n   l e   r e g é n è r e r a  
 	 	 	 	 	 / / 	 a u   b e s o i n   à   l a   d é s é r i a l i s a t i o n   :  
  
 	 	 	 	 	 i f   ( d a t a . S t a r t s W i t h ( E n t i t i e s E d i t o r . X m l . X m l H e a d e r ) )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 d a t a   =   d a t a . S u b s t r i n g ( E n t i t i e s E d i t o r . X m l . X m l H e a d e r . L e n g t h ) ;  
 	 	 	 	 	 }  
  
 	 	 	 	 	 d i c t [ k e y ]   =   d a t a ;  
 	 	 	 	 }  
  
 	 	 	 	 E n t i t i e s . S e t S e r i a l i z e d L a y o u t s ( r e c o r d ,   d i c t ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   s t a t i c   s t r i n g   G e t S e r i a l i z e d D a t a ( I R e s o u r c e A c c e s s o r   a c c e s s o r ,   D r u i d   d r u i d ,   i n t   s u b V i e w )  
 	 	 {  
 	 	 	 / / 	 D é s é r i a l i s e   d e s   d o n n é e s .   R e t o u r n e   n u l l   s ' i l   n ' e x i s t e   a u c u n e   d o n n é e   s é r i a l i s é e .  
 	 	 	 C u l t u r e M a p   r e s o u r c e   =   a c c e s s o r . C o l l e c t i o n [ d r u i d ] ;  
 	 	 	  
 	 	 	 i f   ( r e s o u r c e   ! =   n u l l )  
 	 	 	 {  
 	 	 	 	 S t r u c t u r e d D a t a   r e c o r d   =   r e s o u r c e . G e t C u l t u r e D a t a ( R e s o u r c e s . D e f a u l t T w o L e t t e r I S O L a n g u a g e N a m e ) ;  
 	 	 	 	 s t r i n g   k e y   =   s u b V i e w . T o S t r i n g ( S y s t e m . G l o b a l i z a t i o n . C u l t u r e I n f o . I n v a r i a n t C u l t u r e ) ;  
 	 	 	 	 D i c t i o n a r y < s t r i n g ,   s t r i n g >   d i c t   =   E n t i t i e s . G e t S e r i a l i z e d L a y o u t s ( r e c o r d ) ;  
 	 	 	 	  
 	 	 	 	 i f   ( d i c t . C o n t a i n s K e y ( k e y ) )  
 	 	 	 	 {  
 	 	 	 	 	 s t r i n g   d a t a   =   d i c t [ k e y ] ;  
  
 	 	 	 	 	 / / 	 S i   l e s   d o n n é e s   o n t   é t é   p u r g é e s   d e   l e u r   e n - t ê t e   < ? x m l   . . . ? > ,   a l o r s   o n  
 	 	 	 	 	 / / 	 l e u r   e n   r e m e t   u n   a r t i f i c i e l l e m e n t   :  
 	 	 	 	 	 i f   ( d a t a . S t a r t s W i t h ( " < ? x m l " ) )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 r e t u r n   d a t a ;  
 	 	 	 	 	 }  
 	 	 	 	 	 e l s e  
 	 	 	 	 	 {  
 	 	 	 	 	 	 r e t u r n   E n t i t i e s E d i t o r . X m l . X m l H e a d e r   +   d a t a ;  
 	 	 	 	 	 }  
 	 	 	 	 }  
 	 	 	 }  
 	 	 	 r e t u r n   n u l l ;  
 	 	 }  
  
 	 	 p r i v a t e   s t a t i c   D i c t i o n a r y < s t r i n g ,   s t r i n g >   G e t S e r i a l i z e d L a y o u t s ( S t r u c t u r e d D a t a   r e c o r d )  
 	 	 {  
 	 	 	 D i c t i o n a r y < s t r i n g ,   s t r i n g >   d i c t   =   n e w   D i c t i o n a r y < s t r i n g ,   s t r i n g > ( ) ;  
 	 	 	 s t r i n g   d a t a   =   r e c o r d . G e t V a l u e ( S u p p o r t . R e s . F i e l d s . R e s o u r c e S t r u c t u r e d T y p e . S e r i a l i z e d D e s i g n e r L a y o u t s )   a s   s t r i n g ;  
  
 	 	 	 s t r i n g   o p e n E l e m e n t P r e f i x   =   " < " + E n t i t i e s E d i t o r . X m l . L a y o u t ;  
 	 	 	 s t r i n g   c l o s e E l e m e n t             =   " < / " + E n t i t i e s E d i t o r . X m l . L a y o u t + " > " ;  
 	 	 	  
 	 	 	 w h i l e   ( ! s t r i n g . I s N u l l O r E m p t y ( d a t a ) )  
 	 	 	 {  
 	 	 	 	 S y s t e m . D i a g n o s t i c s . D e b u g . A s s e r t ( d a t a . S t a r t s W i t h ( o p e n E l e m e n t P r e f i x ) ) ;  
  
 	 	 	 	 / / 	 	 	 	 	 	 	 	 	 	 / / 	 < L a y o u t   i d = " 1 " > . . . < / L a y o u t > < L a y o u t   i d = " 2 " > . . .  
 	 	 	 	 i n t   p o s 1   =   d a t a . I n d e x O f ( ' " ' ) + 1 ; 	 	 	 / /                             ^ :   :     :                 :  
 	 	 	 	 i n t   p o s 2   =   d a t a . I n d e x O f ( ' " ' ,   p o s 1 ) ; 	 	 / / 	                           ^   :     :                 :  
 	 	 	 	 i n t   p o s 3   =   d a t a . I n d e x O f ( ' > ' ) + 1 ; 	 	 	 / / 	                               ^     :                 :  
 	 	 	 	 i n t   p o s 4   =   d a t a . I n d e x O f ( c l o s e E l e m e n t ) ; 	 / / 	                                     ^                 :  
 	 	 	 	 i n t   p o s 5   =   p o s 4   +   c l o s e E l e m e n t . L e n g t h ; 	 / / 	                                                       ^  
 	 	 	 	  
 	 	 	 	 s t r i n g   i d       =   d a t a . S u b s t r i n g ( p o s 1 ,   p o s 2 - p o s 1 ) ;  
 	 	 	 	 s t r i n g   n o d e   =   d a t a . S u b s t r i n g ( p o s 3 ,   p o s 4 - p o s 3 ) ;  
  
 	 	 	 	 d i c t [ i d ]   =   n o d e ;  
  
 	 	 	 	 d a t a   =   d a t a . S u b s t r i n g ( p o s 5 ) ;  
 	 	 	 }  
 	 	 	  
 	 	 	 r e t u r n   d i c t ;  
 	 	 }  
  
 	 	 p r i v a t e   s t a t i c   v o i d   S e t S e r i a l i z e d L a y o u t s ( S t r u c t u r e d D a t a   r e c o r d ,   D i c t i o n a r y < s t r i n g ,   s t r i n g >   d i c t )  
 	 	 {  
 	 	 	 S y s t e m . T e x t . S t r i n g B u i l d e r   b u f f e r   =   n e w   S y s t e m . T e x t . S t r i n g B u i l d e r ( ) ;  
  
 	 	 	 f o r e a c h   ( K e y V a l u e P a i r < s t r i n g ,   s t r i n g >   p a i r   i n   d i c t )  
 	 	 	 {  
 	 	 	 	 b u f f e r . A p p e n d ( " < " ) ;  
 	 	 	 	 b u f f e r . A p p e n d ( E n t i t i e s E d i t o r . X m l . L a y o u t ) ;  
 	 	 	 	 b u f f e r . A p p e n d ( @ "   i d = " " " ) ;  
 	 	 	 	 b u f f e r . A p p e n d ( p a i r . K e y ) ;  
 	 	 	 	 b u f f e r . A p p e n d ( @ " " " > " ) ;  
  
 	 	 	 	 b u f f e r . A p p e n d ( p a i r . V a l u e ) ;  
  
 	 	 	 	 b u f f e r . A p p e n d ( " < / " ) ;  
 	 	 	 	 b u f f e r . A p p e n d ( E n t i t i e s E d i t o r . X m l . L a y o u t ) ;  
 	 	 	 	 b u f f e r . A p p e n d ( " > " ) ;  
 	 	 	 }  
  
 	 	 	 r e c o r d . S e t V a l u e ( S u p p o r t . R e s . F i e l d s . R e s o u r c e S t r u c t u r e d T y p e . S e r i a l i z e d D e s i g n e r L a y o u t s ,   b u f f e r . T o S t r i n g ( ) ) ;  
 	 	 }  
  
  
 	 	 p u b l i c   D r u i d   C u r r e n t D r u i d  
 	 	 {  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 C u l t u r e M a p   i t e m   =   t h i s . a c c e s s . C o l l e c t i o n V i e w . C u r r e n t I t e m   a s   C u l t u r e M a p ;  
 	 	 	 	 i f   ( i t e m   = =   n u l l )  
 	 	 	 	 {  
 	 	 	 	 	 r e t u r n   D r u i d . E m p t y ;  
 	 	 	 	 }  
 	 	 	 	 e l s e  
 	 	 	 	 {  
 	 	 	 	 	 r e t u r n   i t e m . I d ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   s t r i n g   C u r r e n t N a m e  
 	 	 {  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 C u l t u r e M a p   i t e m   =   t h i s . a c c e s s . C o l l e c t i o n V i e w . C u r r e n t I t e m   a s   C u l t u r e M a p ;  
 	 	 	 	 i f   ( i t e m   = =   n u l l )  
 	 	 	 	 {  
 	 	 	 	 	 r e t u r n   n u l l ;  
 	 	 	 	 }  
 	 	 	 	 e l s e  
 	 	 	 	 {  
 	 	 	 	 	 r e t u r n   i t e m . N a m e ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   i n t   G e t S u b V i e w ( o b j e c t   w i d g e t )  
 	 	 {  
 	 	 	 / / 	 R e t o u r n e   l e   r a n g   d ' u n e   s o u s - v u e   c o r r e s p o n d a n t   à   u n   w i d g e t .  
 	 	 	 i f   ( w i d g e t   = =   t h i s . b u t t o n S u b V i e w A )  
 	 	 	 {  
 	 	 	 	 r e t u r n   0 ;  
 	 	 	 }  
  
 	 	 	 i f   ( w i d g e t   = =   t h i s . b u t t o n S u b V i e w B )  
 	 	 	 {  
 	 	 	 	 r e t u r n   1 ;  
 	 	 	 }  
  
 	 	 	 i f   ( w i d g e t   = =   t h i s . b u t t o n S u b V i e w C )  
 	 	 	 {  
 	 	 	 	 r e t u r n   2 ;  
 	 	 	 }  
  
 	 	 	 i f   ( w i d g e t   = =   t h i s . b u t t o n S u b V i e w T )  
 	 	 	 {  
 	 	 	 	 r e t u r n   3 ;  
 	 	 	 }  
  
 	 	 	 r e t u r n   - 1 ;  
 	 	 }  
  
  
 	 	 p r i v a t e   v o i d   H a n d l e E d i t o r S i z e C h a n g e d ( o b j e c t   s e n d e r ,   D e p e n d e n c y P r o p e r t y C h a n g e d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   l o r s q u e   l a   t a i l l e   d e   l a   f e n ê t r e   d e   l ' é d i t e u r   c h a n g e .  
 	 	 	 t h i s . U p d a t e S c r o l l e r ( ) ;  
 	 	 	 t h i s . U p d a t e Z o o m ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e E d i t o r A r e a S i z e C h a n g e d ( o b j e c t   s e n d e r )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   l o r s q u e   l e s   d i m e n s i o n s   d e   l a   z o n e   d e   t r a v a i l   o n t   c h a n g é .  
 	 	 	 t h i s . A r e a S i z e   =   t h i s . e d i t o r . A r e a S i z e ;  
 	 	 	 t h i s . U p d a t e Z o o m ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e E d i t o r A r e a O f f s e t C h a n g e d ( o b j e c t   s e n d e r )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   l o r s q u e   l ' o f f s e t   d e   l a   z o n e   d e   t r a v a i l   a   c h a n g é .  
 	 	 	 P o i n t   o f f s e t   =   t h i s . e d i t o r . A r e a O f f s e t ;  
  
 	 	 	 i f   ( t h i s . h s c r o l l e r . E n a b l e )  
 	 	 	 {  
 	 	 	 	 o f f s e t . X   =   S y s t e m . M a t h . M a x ( o f f s e t . X ,   ( d o u b l e )   t h i s . h s c r o l l e r . M i n V a l u e / t h i s . Z o o m ) ;  
 	 	 	 	 o f f s e t . X   =   S y s t e m . M a t h . M i n ( o f f s e t . X ,   ( d o u b l e )   t h i s . h s c r o l l e r . M a x V a l u e / t h i s . Z o o m ) ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 o f f s e t . X   =   0 ;  
 	 	 	 }  
  
 	 	 	 i f   ( t h i s . v s c r o l l e r . E n a b l e )  
 	 	 	 {  
 	 	 	 	 o f f s e t . Y   =   S y s t e m . M a t h . M a x ( o f f s e t . Y ,   ( d o u b l e )   t h i s . v s c r o l l e r . M i n V a l u e / t h i s . Z o o m ) ;  
 	 	 	 	 o f f s e t . Y   =   S y s t e m . M a t h . M i n ( o f f s e t . Y ,   ( d o u b l e )   t h i s . v s c r o l l e r . M a x V a l u e / t h i s . Z o o m ) ;  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 o f f s e t . Y   =   0 ;  
 	 	 	 }  
  
 	 	 	 t h i s . e d i t o r . A r e a O f f s e t   =   o f f s e t ;  
  
 	 	 	 t h i s . h s c r o l l e r . V a l u e   =   ( d e c i m a l )   ( o f f s e t . X * t h i s . Z o o m ) ;  
 	 	 	 t h i s . v s c r o l l e r . V a l u e   =   ( d e c i m a l )   ( o f f s e t . Y * t h i s . Z o o m ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e S c r o l l e r V a l u e C h a n g e d ( o b j e c t   s e n d e r )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   l o r s q u ' u n   a s c e n s e u r   a   é t é   b o u g é .  
 	 	 	 d o u b l e   o x   =   0 ;  
 	 	 	 i f   ( t h i s . h s c r o l l e r . I s E n a b l e d )  
 	 	 	 {  
 	 	 	 	 o x   =   ( d o u b l e )   t h i s . h s c r o l l e r . V a l u e / t h i s . Z o o m ;  
 	 	 	 }  
  
 	 	 	 d o u b l e   o y   =   0 ;  
 	 	 	 i f   ( t h i s . v s c r o l l e r . I s E n a b l e d )  
 	 	 	 {  
 	 	 	 	 o y   =   ( d o u b l e )   t h i s . v s c r o l l e r . V a l u e / t h i s . Z o o m ;  
 	 	 	 }  
  
 	 	 	 t h i s . e d i t o r . A r e a O f f s e t   =   n e w   P o i n t ( o x ,   o y ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e B u t t o n S u b V i e w C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   l o r s q u ' u n   b o u t o n   d e   v u e   ( A ,   B ,   C   o u   T )   e s t   c l i q u é .  
 	 	 	 t h i s . S u b V i e w   =   t h i s . G e t S u b V i e w ( s e n d e r ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e B u t t o n S u b V i e w D r a g S t a r t i n g ( o b j e c t   s e n d e r )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   l o r s q u ' u n   b o u t o n   d e   v u e   ( A ,   B ,   C   o u   T )   c o m m e n c e r   à   ê t r e   d r a g g é   s u r   u n   a u t r e .  
 	 	 	 t h i s . d r a g S t a r t i n g   =   t h i s . G e t S u b V i e w ( s e n d e r ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e B u t t o n S u b V i e w D r a g E n d i n g ( o b j e c t   s e n d e r )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   l o r s q u ' u n   b o u t o n   d e   v u e   ( A ,   B ,   C   o u   T )   a   é t é   d r a g g é   s u r   u n   a u t r e .  
 	 	 	 i n t   d r a g E n d i n g   =   t h i s . G e t S u b V i e w ( s e n d e r ) ;  
 	 	 	 i f   ( t h i s . d r a g S t a r t i n g   ! =   - 1   & &   d r a g E n d i n g   ! =   - 1 )  
 	 	 	 {  
 	 	 	 	 t h i s . D r a g S u b V i e w ( t h i s . d r a g S t a r t i n g ,   d r a g E n d i n g ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e B u t t o n Z o o m C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   l o r s q u ' u n   b o u t o n   d e   z o o m   p r é d é f i n i   e s t   c l i q u é .  
 	 	 	 i f   ( s e n d e r   = =   t h i s . b u t t o n Z o o m P a g e )  
 	 	 	 {  
 	 	 	 	 E n t i t i e s . i s Z o o m P a g e   =   t r u e ;  
 	 	 	 	 t h i s . Z o o m   =   0 ;  
 	 	 	 }  
  
 	 	 	 i f   ( s e n d e r   = =   t h i s . b u t t o n Z o o m M i n )  
 	 	 	 {  
 	 	 	 	 E n t i t i e s . i s Z o o m P a g e   =   f a l s e ;  
 	 	 	 	 t h i s . Z o o m   =   E n t i t i e s . z o o m M i n ;  
 	 	 	 }  
  
 	 	 	 i f   ( s e n d e r   = =   t h i s . b u t t o n Z o o m D e f a u l t )  
 	 	 	 {  
 	 	 	 	 E n t i t i e s . i s Z o o m P a g e   =   f a l s e ;  
 	 	 	 	 t h i s . Z o o m   =   E n t i t i e s . z o o m D e f a u l t ;  
 	 	 	 }  
 	 	 	  
 	 	 	 i f   ( s e n d e r   = =   t h i s . b u t t o n Z o o m M a x )  
 	 	 	 {  
 	 	 	 	 E n t i t i e s . i s Z o o m P a g e   =   f a l s e ;  
 	 	 	 	 t h i s . Z o o m   =   E n t i t i e s . z o o m M a x ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e F i e l d Z o o m C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   l o r s q u e   l e   c h a m p   d u   z o o m   a   é t é   c l i q u é .  
 	 	 	 S t a t u s F i e l d   s f   =   s e n d e r   a s   S t a t u s F i e l d ;  
 	 	 	 i f   ( s f   = =   n u l l )     r e t u r n ;  
 	 	 	 V M e n u   m e n u   =   E n t i t i e s E d i t o r . Z o o m M e n u . C r e a t e Z o o m M e n u ( E n t i t i e s . z o o m D e f a u l t ,   t h i s . Z o o m ,   t h i s . Z o o m P a g e ,   n u l l ) ;  
 	 	 	 m e n u . H o s t   =   s f . W i n d o w ;  
 	 	 	 T e x t F i e l d C o m b o . A d j u s t C o m b o S i z e ( s f ,   m e n u ,   f a l s e ) ;  
 	 	 	 m e n u . S h o w A s C o m b o L i s t ( s f ,   P o i n t . Z e r o ,   s f ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e S l i d e r Z o o m V a l u e C h a n g e d ( o b j e c t   s e n d e r )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   l o r s q u e   l e   s l i d e r   d u   z o o m   a   é t é   b o u g é .  
 	 	 	 i f   ( t h i s . i g n o r e C h a n g e )  
 	 	 	 {  
 	 	 	 	 r e t u r n ;  
 	 	 	 }  
  
 	 	 	 H S l i d e r   s l i d e r   =   s e n d e r   a s   H S l i d e r ;  
 	 	 	 E n t i t i e s . i s Z o o m P a g e   =   f a l s e ;  
 	 	 	 t h i s . Z o o m   =   ( d o u b l e )   s l i d e r . V a l u e ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e E d i t o r Z o o m C h a n g e d ( o b j e c t   s e n d e r )  
 	 	 {  
 	 	 	 / / 	 A p p e l é   l o r s q u e   l e   z o o m   a   c h a n g é   d e p u i s   l ' é d i t e u r .  
 	 	 	 E n t i t i e s . i s Z o o m P a g e   =   f a l s e ;  
 	 	 	 t h i s . Z o o m   =   t h i s . e d i t o r . Z o o m ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e R e s e t B u t t o n C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 A b s t r a c t B u t t o n   b u t t o n   =   s e n d e r   a s   A b s t r a c t B u t t o n ;  
  
 	 	 	 i f   ( b u t t o n   = =   t h i s . g r o u p T o o l b a r . R e s e t B u t t o n )  
 	 	 	 {  
 	 	 	 	 S u p p o r t . R e s o u r c e A c c e s s o r s . S t r u c t u r e d T y p e R e s o u r c e A c c e s s o r   a c c e s s o r   =   t h i s . a c c e s s . A c c e s s o r   a s   S u p p o r t . R e s o u r c e A c c e s s o r s . S t r u c t u r e d T y p e R e s o u r c e A c c e s s o r ;  
 	 	 	 	 C u l t u r e M a p   i t e m   =   t h i s . a c c e s s . C o l l e c t i o n V i e w . C u r r e n t I t e m   a s   C u l t u r e M a p ;  
 	 	 	 	 S t r u c t u r e d D a t a   d a t a   =   i t e m . G e t C u l t u r e D a t a ( R e s o u r c e s . D e f a u l t T w o L e t t e r I S O L a n g u a g e N a m e ) ;  
 	 	 	 	 a c c e s s o r . R e s e t T o O r i g i n a l V a l u e ( i t e m ,   d a t a ,   S u p p o r t . R e s . F i e l d s . R e s o u r c e S t r u c t u r e d T y p e . F i e l d s ) ;  
 	 	 	 	 a c c e s s o r . R e s e t T o O r i g i n a l V a l u e ( i t e m ,   d a t a ,   S u p p o r t . R e s . F i e l d s . R e s o u r c e S t r u c t u r e d T y p e . I n t e r f a c e I d s ) ;  
 	 	 	 	 a c c e s s o r . R e s e t T o O r i g i n a l V a l u e ( i t e m ,   d a t a ,   S u p p o r t . R e s . F i e l d s . R e s o u r c e S t r u c t u r e d T y p e . S e r i a l i z e d D e s i g n e r L a y o u t s ) ;  
 	 	 	 	 a c c e s s o r . R e s e t T o O r i g i n a l V a l u e ( i t e m ,   d a t a ,   S u p p o r t . R e s . F i e l d s . R e s o u r c e S t r u c t u r e d T y p e . C l a s s ) ;  
 	 	 	 	 a c c e s s o r . R e s e t T o O r i g i n a l V a l u e ( i t e m ,   d a t a ,   S u p p o r t . R e s . F i e l d s . R e s o u r c e S t r u c t u r e d T y p e . B a s e T y p e ) ;  
 	 	 	 	 a c c e s s o r . R e s e t T o O r i g i n a l V a l u e ( i t e m ,   d a t a ,   S u p p o r t . R e s . F i e l d s . R e s o u r c e S t r u c t u r e d T y p e . D e f a u l t L i f e t i m e E x p e c t a n c y ) ;  
 	 	 	 	 a c c e s s o r . R e s e t T o O r i g i n a l V a l u e ( i t e m ,   d a t a ,   S u p p o r t . R e s . F i e l d s . R e s o u r c e S t r u c t u r e d T y p e . F l a g s ) ;  
 	 	 	 }  
  
 	 	 	 t h i s . U p d a t e E d i t ( ) ;  
 	 	 	 t h i s . m o d u l e . A c c e s s E n t i t i e s . S e t L o c a l D i r t y ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e B u t t o n G r i d C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . e d i t o r . G r i d   =   ! t h i s . e d i t o r . G r i d ;  
 	 	 	 t h i s . U p d a t e G r i d   ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e B u t t o n A d d E n t i t y C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 D r u i d   d r u i d   =   D r u i d . E m p t y ;  
 	 	 	 b o o l   i s N u l l a b l e   =   f a l s e ;  
 	 	 	 M o d u l e   m o d u l e   =   t h i s . e d i t o r . M o d u l e ;  
 	 	 	 S t r u c t u r e d T y p e C l a s s   t y p e C l a s s   =   S t r u c t u r e d T y p e C l a s s . E n t i t y ;  
  
 	 	 	 v a r   r e s u l t   =   m o d u l e . D e s i g n e r A p p l i c a t i o n . D l g R e s o u r c e S e l e c t o r ( D i a l o g s . R e s o u r c e S e l e c t o r D i a l o g . O p e r a t i o n . E n t i t i e s ,   m o d u l e ,   R e s o u r c e A c c e s s . T y p e . E n t i t i e s ,   r e f   t y p e C l a s s ,   r e f   d r u i d ,   r e f   i s N u l l a b l e ,   n u l l ,   D r u i d . E m p t y ) ;  
 	 	 	  
 	 	 	 i f   ( r e s u l t   ! =   C o m m o n . D i a l o g s . D i a l o g R e s u l t . Y e s )  
 	 	 	 {  
 	 	 	 	 r e t u r n ;  
 	 	 	 }  
  
 	 	 	 / / 	 A j o u t e   u n e   n o u v e l l e   b o î t e .  
 	 	 	 m o d u l e   =   t h i s . d e s i g n e r A p p l i c a t i o n . S e a r c h M o d u l e   ( d r u i d ) ;  
  
 	 	 	 i f   ( m o d u l e   ! =   n u l l )  
 	 	 	 {  
 	 	 	 	 v a r   c u l t u r e M a p   =   m o d u l e . A c c e s s E n t i t i e s . A c c e s s o r . C o l l e c t i o n [ d r u i d ] ;  
  
 	 	 	 	 i f   ( c u l t u r e M a p   ! =   n u l l )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . e d i t o r . R o o t B o x . O p e n B o x   ( c u l t u r e M a p ) ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e B u t t o n S a v e B i t m a p C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 s t r i n g   p a t h ;  
 	 	 	 E n t i t i e s E d i t o r . B i t m a p P a r a m e t e r s   b i t m a p P a r a m e t e r s ;  
  
 	 	 	 i f   ( t h i s . S a v e B i t m a p D i a l o g   ( o u t   p a t h ,   o u t   b i t m a p P a r a m e t e r s ) )  
 	 	 	 {  
 	 	 	 	 t h i s . S a v e B i t m a p   ( p a t h ,   b i t m a p P a r a m e t e r s ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e B u t t o n S a v e A l l B i t m a p s C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 v a r   e n t i t y S a m p l e s   =   t h i s . G e t S a v e B i t m a p S a m p l e s   ( ) ;  
  
 	 	 	 / / 	 R e p r e n d   l e s   p a r a m è t r e s   s é r i a l i s é s .  
 	 	 	 v a r   m o d u l e N a m e   =   t h i s . m o d u l e . M o d u l e I d . N a m e ;  
 	 	 	 v a r   d a t a   =   t h i s . d e s i g n e r A p p l i c a t i o n . S e t t i n g s . G e t S a v e A l l B i t m a p s D a t a   ( m o d u l e N a m e ) ;  
  
 	 	 	 v a r   s e l e c t e d E n t i t y N a m e s   =   n e w   L i s t < s t r i n g >   ( ) ;  
 	 	 	 s t r i n g   f o l d e r ,   e x t e n s i o n ;  
 	 	 	 E n t i t i e s E d i t o r . B i t m a p P a r a m e t e r s   b i t m a p P a r a m e t e r s ;  
 	 	 	 E n t i t i e s . S e t S a v e A l l B i t m a p s S e r i a l i z e D a t a   ( d a t a ,   s e l e c t e d E n t i t y N a m e s ,   o u t   f o l d e r ,   o u t   e x t e n s i o n ,   o u t   b i t m a p P a r a m e t e r s ) ;  
  
 	 	 	 / / 	 D e m a n d e   l e s   c h o i x   à   l ' u t i l i s a t e u r .  
 	 	 	 v a r   r e s u l t   =   t h i s . d e s i g n e r A p p l i c a t i o n . D l g S a v e A l l B i t m a p s   ( e n t i t y S a m p l e s ,   s e l e c t e d E n t i t y N a m e s ,   r e f   f o l d e r ,   r e f   e x t e n s i o n ,   r e f   b i t m a p P a r a m e t e r s ) ;  
  
 	 	 	 i f   ( r e s u l t   = =   S y s t e m . W i n d o w s . F o r m s . D i a l o g R e s u l t . O K )     / /   g é n é r e r   ?  
 	 	 	 {  
 	 	 	 	 i n t   c o u n t e r ;  
 	 	 	 	 v a r   e r r   =   t h i s . S a v e A l l B i t m a p s   ( s e l e c t e d E n t i t y N a m e s ,   f o l d e r ,   e x t e n s i o n ,   b i t m a p P a r a m e t e r s ,   o u t   c o u n t e r ) ;  
  
 	 	 	 	 i f   ( ! s t r i n g . I s N u l l O r E m p t y   ( e r r ) )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . d e s i g n e r A p p l i c a t i o n . D i a l o g M e s s a g e   ( s t r i n g . F o r m a t   ( e r r ,   f o l d e r ) ) ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 i f   ( r e s u l t   = =   S y s t e m . W i n d o w s . F o r m s . D i a l o g R e s u l t . O K   | |     / /   g é n é r e r   ?  
 	 	 	 	 r e s u l t   = =   S y s t e m . W i n d o w s . F o r m s . D i a l o g R e s u l t . Y e s )       / /   a p p l i q u e r   ?  
 	 	 	 {  
 	 	 	 	 / / 	 S é r i a l i s e   l e s   p a r a m è t r e s .  
 	 	 	 	 d a t a   =   E n t i t i e s . G e t S a v e A l l B i t m a p s S e r i a l i z e D a t a   ( s e l e c t e d E n t i t y N a m e s ,   f o l d e r ,   e x t e n s i o n ,   b i t m a p P a r a m e t e r s ) ;  
 	 	 	 	 t h i s . d e s i g n e r A p p l i c a t i o n . S e t t i n g s . S e t S a v e A l l B i t m a p s D a t a   ( m o d u l e N a m e ,   d a t a ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   L i s t < E n t i t i e s E d i t o r . E n t i t y S a m p l e >   G e t S a v e B i t m a p S a m p l e s ( )  
 	 	 {  
 	 	 	 / / 	 C o n s t r u i t   l a   l i s t e   d e   t o u s   l e s   é c h a n t i l l o n s   d ' e n t i t é s .  
 	 	 	 v a r   e n t i t y S a m p l e s   =   n e w   L i s t < E n t i t i e s E d i t o r . E n t i t y S a m p l e >   ( ) ;  
  
 	 	 	 i n t   c u r r e n t   =   t h i s . a c c e s s . A c c e s s I n d e x ;  
  
 	 	 	 f o r   ( i n t   i   =   0 ;   i   <   t h i s . a c c e s s . C o l l e c t i o n V i e w . C o u n t ;   i + + )  
 	 	 	 {  
 	 	 	 	 t h i s . a c c e s s . A c c e s s I n d e x   =   i ;  
 	 	 	 	 C u l t u r e M a p   i t e m   =   t h i s . a c c e s s . C o l l e c t i o n V i e w . C u r r e n t I t e m   a s   C u l t u r e M a p ;  
  
 	 	 	 	 t h i s . D e s e r i a l i z e   ( f a l s e ) ;  
  
 	 	 	 	 v a r   s a m p l e   =   n e w   E n t i t i e s E d i t o r . E n t i t y S a m p l e   ( i t e m . N a m e ,   t h i s . e d i t o r . R o o t B o x . S t r u c t u r e d T y p e F l a g s ,   t h i s . e d i t o r . B o x C o u n t ) ;  
 	 	 	 	 e n t i t y S a m p l e s . A d d   ( s a m p l e ) ;  
 	 	 	 }  
  
 	 	 	 t h i s . a c c e s s . A c c e s s I n d e x   =   c u r r e n t ;  
  
 	 	 	 / / ? e n t i t y S a m p l e s . S o r t   ( ) ;  
 	 	 	 r e t u r n   e n t i t y S a m p l e s ;  
 	 	 }  
  
 	 	 p r i v a t e   b o o l   S a v e B i t m a p D i a l o g ( o u t   s t r i n g   p a t h ,   o u t   E n t i t i e s E d i t o r . B i t m a p P a r a m e t e r s   b i t m a p P a r a m e t e r s )  
 	 	 {  
 	 	 	 v a r   d i a l o g   =   n e w   D i a l o g s . F i l e S a v e B i t m a p D i a l o g   ( t h i s . d e s i g n e r A p p l i c a t i o n ) ;  
 	 	 	 d i a l o g . B i t m a p S i z e   =   t h i s . e d i t o r . A r e a S i z e ;  
 	 	 	 d i a l o g . S h o w D i a l o g   ( ) ;  
  
 	 	 	 i f   ( d i a l o g . R e s u l t   ! =   C o m m o n . D i a l o g s . D i a l o g R e s u l t . A c c e p t )  
 	 	 	 {  
 	 	 	 	 p a t h   =   n u l l ;  
 	 	 	 	 b i t m a p P a r a m e t e r s   =   n u l l ;  
 	 	 	 	 r e t u r n   f a l s e ;  
 	 	 	 }  
  
 	 	 	 d i a l o g . P a t h M e m o r i z e   ( ) ;  
  
 	 	 	 p a t h   =   d i a l o g . F i l e N a m e ;  
 	 	 	 b i t m a p P a r a m e t e r s   =   d i a l o g . B i t m a p P a r a m e t e r s ;  
 	 	 	 r e t u r n   t r u e ;  
 	 	 }  
  
 	 	 p u b l i c   i n t   S a v e A l l B i t m a p s ( )  
 	 	 {  
 	 	 	 / / 	 E n r e g i s t r e   t o u t e s   l e s   i m a g e s   a v e c   l e s   d e r n i e r s   p a r a m è t r e s   u t i l i s é s .  
 	 	 	 v a r   m o d u l e N a m e   =   t h i s . m o d u l e . M o d u l e I d . N a m e ;  
 	 	 	 v a r   d a t a   =   t h i s . d e s i g n e r A p p l i c a t i o n . S e t t i n g s . G e t S a v e A l l B i t m a p s D a t a   ( m o d u l e N a m e ) ;  
  
 	 	 	 v a r   s e l e c t e d E n t i t y N a m e s   =   n e w   L i s t < s t r i n g >   ( ) ;  
 	 	 	 s t r i n g   f o l d e r ,   e x t e n s i o n ;  
 	 	 	 E n t i t i e s E d i t o r . B i t m a p P a r a m e t e r s   b i t m a p P a r a m e t e r s ;  
 	 	 	 E n t i t i e s . S e t S a v e A l l B i t m a p s S e r i a l i z e D a t a   ( d a t a ,   s e l e c t e d E n t i t y N a m e s ,   o u t   f o l d e r ,   o u t   e x t e n s i o n ,   o u t   b i t m a p P a r a m e t e r s ) ;  
  
 	 	 	 i n t   c o u n t e r ;  
 	 	 	 t h i s . S a v e A l l B i t m a p s   ( s e l e c t e d E n t i t y N a m e s ,   f o l d e r ,   e x t e n s i o n ,   b i t m a p P a r a m e t e r s ,   o u t   c o u n t e r ) ;  
  
 	 	 	 r e t u r n   c o u n t e r ;  
 	 	 }  
  
 	 	 p r i v a t e   s t r i n g   S a v e A l l B i t m a p s ( L i s t < s t r i n g >   e n t i t y N a m e s ,   s t r i n g   f o l d e r ,   s t r i n g   e x t e n s i o n ,   E n t i t i e s E d i t o r . B i t m a p P a r a m e t e r s   b i t m a p P a r a m e t e r s ,   o u t   i n t   c o u n t e r )  
 	 	 {  
 	 	 	 / / 	 E n r e g i s t r e   t o u t e s   l e s   i m a g e s   a v e c   l e s   p a r a m è t r e s   d o n n é s .  
 	 	 	 c o u n t e r   =   0 ;  
  
 	 	 	 i f   ( e n t i t y N a m e s . C o u n t   = =   0   | |   s t r i n g . I s N u l l O r E m p t y   ( f o l d e r )   | |   s t r i n g . I s N u l l O r E m p t y   ( e x t e n s i o n ) )  
 	 	 	 {  
 	 	 	 	 r e t u r n   n u l l ;     / /   o k  
 	 	 	 }  
  
 	 	 	 i f   ( ! S y s t e m . I O . D i r e c t o r y . E x i s t s   ( f o l d e r ) )  
 	 	 	 {  
 	 	 	 	 r e t u r n   s t r i n g . F o r m a t   ( " L e   d o s s i e r   \ " { 0 } \ "   n ' e x i s t e   p a s . " ,   f o l d e r ) ;  
 	 	 	 }  
  
 	 	 	 s t r i n g   n s   =   t h i s . m o d u l e . M o d u l e I n f o . S o u r c e N a m e s p a c e E n t i t i e s ;  
 	 	 	 i f   ( ! s t r i n g . I s N u l l O r E m p t y   ( n s ) )  
 	 	 	 {  
 	 	 	 	 n s   =   s t r i n g . C o n c a t   ( n s ,   " . " ) ;     / /   n a m e   < -   n a m e s p a c e   d u   m o d u l e  
 	 	 	 }  
  
 	 	 	 i n t   c u r r e n t   =   t h i s . a c c e s s . A c c e s s I n d e x ;  
  
 	 	 	 f o r   ( i n t   i   =   0 ;   i   <   t h i s . a c c e s s . C o l l e c t i o n V i e w . C o u n t ;   i + + )  
 	 	 	 {  
 	 	 	 	 t h i s . a c c e s s . A c c e s s I n d e x   =   i ;  
 	 	 	 	 C u l t u r e M a p   i t e m   =   t h i s . a c c e s s . C o l l e c t i o n V i e w . C u r r e n t I t e m   a s   C u l t u r e M a p ;  
  
 	 	 	 	 i f   ( e n t i t y N a m e s . C o n t a i n s   ( i t e m . N a m e ) )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . D e s e r i a l i z e   ( f a l s e ) ;  
  
 	 	 	 	 	 s t r i n g   p a t h   =   S y s t e m . I O . P a t h . C o m b i n e   ( f o l d e r ,   s t r i n g . C o n c a t   ( n s ,   i t e m . N a m e ,   e x t e n s i o n ) ) ;  
 	 	 	 	 	 t h i s . S a v e B i t m a p   ( p a t h ,   b i t m a p P a r a m e t e r s ) ;  
  
 	 	 	 	 	 c o u n t e r + + ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 t h i s . a c c e s s . A c c e s s I n d e x   =   c u r r e n t ;  
 	 	 	 t h i s . U p d a t e E d i t   ( ) ;  
  
 	 	 	 r e t u r n   n u l l ;     / /   o k  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   S a v e B i t m a p ( s t r i n g   p a t h ,   E n t i t i e s E d i t o r . B i t m a p P a r a m e t e r s   b i t m a p P a r a m e t e r s )  
 	 	 {  
 	 	 	 G r a p h i c s   g r a p h i c s   =   n e w   G r a p h i c s   ( ) ;  
  
 	 	 	 v a r   z o o m   =   b i t m a p P a r a m e t e r s . Z o o m ;  
 	 	 	 v a r   g e n e r a t e C a r t r i d g e   =   b i t m a p P a r a m e t e r s . G e n e r a t e U s e r C a r t r i d g e   | |   b i t m a p P a r a m e t e r s . G e n e r a t e D a t e C a r t r i d g e   | |   b i t m a p P a r a m e t e r s . G e n e r a t e S a m p l e s C a r t r i d g e ;  
  
 	 	 	 v a r   c a r t r i d g e S i z e   =   t h i s . e d i t o r . C a r t r i d g e S i z e   ( b i t m a p P a r a m e t e r s ) ;  
 	 	 	 i f   ( c a r t r i d g e S i z e . I s E m p t y )  
 	 	 	 {  
 	 	 	 	 g e n e r a t e C a r t r i d g e   =   f a l s e ;  
 	 	 	 }  
  
 	 	 	 i n t   c a r t r i d g e W i d t h     =   ( i n t )   ( g e n e r a t e C a r t r i d g e   ?   c a r t r i d g e S i z e . W i d t h     :   0 ) ;  
 	 	 	 i n t   c a r t r i d g e H e i g h t   =   ( i n t )   ( g e n e r a t e C a r t r i d g e   ?   c a r t r i d g e S i z e . H e i g h t   :   0 ) ;  
  
 	 	 	 i n t   d x   =   ( i n t )   t h i s . e d i t o r . A r e a S i z e . W i d t h ;  
 	 	 	 i n t   d y   =   ( i n t )   t h i s . e d i t o r . A r e a S i z e . H e i g h t ;  
  
 	 	 	 i f   ( g e n e r a t e C a r t r i d g e )  
 	 	 	 {  
 	 	 	 	 d x   =   S y s t e m . M a t h . M a x   ( d x ,   c a r t r i d g e W i d t h + 1 ) ;  
 	 	 	 	 d y   + =   c a r t r i d g e H e i g h t ;  
 	 	 	 }  
  
 	 	 	 g r a p h i c s . A l l o c a t e P i x m a p   ( ) ;  
 	 	 	 g r a p h i c s . S e t P i x m a p S i z e   ( ( i n t )   ( d x * z o o m ) ,   ( i n t )   ( d y * z o o m ) ) ;  
 	 	 	 g r a p h i c s . T r a n s f o r m   =   g r a p h i c s . T r a n s f o r m . M u l t i p l y B y   ( T r a n s f o r m . C r e a t e T r a n s l a t i o n T r a n s f o r m   ( 0 ,   - d y ) ) ;  
 	 	 	 g r a p h i c s . T r a n s f o r m   =   g r a p h i c s . T r a n s f o r m . M u l t i p l y B y   ( T r a n s f o r m . C r e a t e S c a l e T r a n s f o r m   ( z o o m ,   - z o o m ) ) ;  
  
 	 	 	 g r a p h i c s . T r a n s f o r m   =   g r a p h i c s . T r a n s f o r m . M u l t i p l y B y   ( T r a n s f o r m . C r e a t e T r a n s l a t i o n T r a n s f o r m   ( 0 ,   - c a r t r i d g e H e i g h t * z o o m ) ) ;  
 	 	 	 t h i s . e d i t o r . E n a b l e D i m m e d   =   f a l s e ;  
 	 	 	 t h i s . e d i t o r . P a i n t O b j e c t s   ( g r a p h i c s ) ;  
 	 	 	 t h i s . e d i t o r . E n a b l e D i m m e d   =   t r u e ;  
 	 	 	 g r a p h i c s . T r a n s f o r m   =   g r a p h i c s . T r a n s f o r m . M u l t i p l y B y   ( T r a n s f o r m . C r e a t e T r a n s l a t i o n T r a n s f o r m   ( 0 ,   c a r t r i d g e H e i g h t * z o o m ) ) ;  
  
 	 	 	 i f   ( g e n e r a t e C a r t r i d g e )  
 	 	 	 {  
 	 	 	 	 t h i s . e d i t o r . P a i n t C a r t r i d g e   ( g r a p h i c s ,   b i t m a p P a r a m e t e r s ) ;  
 	 	 	 }  
  
 	 	 	 v a r   b i t m a p   =   B i t m a p . F r o m P i x m a p   ( g r a p h i c s . P i x m a p )   a s   B i t m a p ;  
 	 	 	 b y t e [ ]   d a t a   =   n u l l ;  
  
 	 	 	 s w i t c h   ( S y s t e m . I O . P a t h . G e t E x t e n s i o n   ( p a t h ) . T o L o w e r I n v a r i a n t   ( ) )  
 	 	 	 {  
 	 	 	 	 c a s e   " . p n g " :  
 	 	 	 	 	 d a t a   =   b i t m a p . S a v e   ( I m a g e F o r m a t . P n g ,   2 4 ) ;  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   " . t i f " :  
 	 	 	 	 	 d a t a   =   b i t m a p . S a v e   ( I m a g e F o r m a t . T i f f ,   2 4 ,   1 0 0 ,   I m a g e C o m p r e s s i o n . L z w ) ;  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   " . j p g " :  
 	 	 	 	 	 d a t a   =   b i t m a p . S a v e   ( I m a g e F o r m a t . J p e g ,   2 4 ,   7 0 ,   I m a g e C o m p r e s s i o n . N o n e ) ;  
 	 	 	 	 	 b r e a k ;  
  
 	 	 	 	 c a s e   " . b m p " :  
 	 	 	 	 	 d a t a   =   b i t m a p . S a v e   ( I m a g e F o r m a t . B m p ,   2 4 ) ;  
 	 	 	 	 	 b r e a k ;  
 	 	 	 }  
  
 	 	 	 i f   ( d a t a   ! =   n u l l )  
 	 	 	 {  
 	 	 	 	 t r y  
 	 	 	 	 {  
 	 	 	 	 	 S y s t e m . I O . F i l e . D e l e t e   ( p a t h ) ;  
 	 	 	 	 }  
 	 	 	 	 c a t c h  
 	 	 	 	 {  
 	 	 	 	 }  
  
 	 	 	 	 t r y  
 	 	 	 	 {  
 	 	 	 	 	 u s i n g   ( S y s t e m . I O . F i l e S t r e a m   s t r e a m   =   n e w   S y s t e m . I O . F i l e S t r e a m   ( p a t h ,   S y s t e m . I O . F i l e M o d e . C r e a t e N e w ) )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 s t r e a m . W r i t e   ( d a t a ,   0 ,   d a t a . L e n g t h ) ;  
 	 	 	 	 	 }  
 	 	 	 	 }  
 	 	 	 	 c a t c h  
 	 	 	 	 {  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 g r a p h i c s . D i s p o s e   ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   s t a t i c   s t r i n g   G e t S a v e A l l B i t m a p s S e r i a l i z e D a t a ( L i s t < s t r i n g >   s e l e c t e d E n t i t y N a m e s ,   s t r i n g   f o l d e r ,   s t r i n g   e x t e n s i o n ,   E n t i t i e s E d i t o r . B i t m a p P a r a m e t e r s   b i t m a p P a r a m e t e r s )  
 	 	 {  
 	 	 	 v a r   l i s t   =   n e w   L i s t < s t r i n g >   ( ) ;  
  
 	 	 	 l i s t . A d d   ( s t r i n g . J o i n   ( " æ%" ,   s e l e c t e d E n t i t y N a m e s ) ) ;  
 	 	 	 l i s t . A d d   ( f o l d e r ) ;  
 	 	 	 l i s t . A d d   ( e x t e n s i o n ) ;  
 	 	 	 l i s t . A d d   ( b i t m a p P a r a m e t e r s . G e t S e r i a l i z e D a t a   ( ) ) ;  
  
 	 	 	 r e t u r n   s t r i n g . J o i n   ( " Ê%" ,   l i s t ) ;  
 	 	 }  
  
 	 	 p r i v a t e   s t a t i c   v o i d   S e t S a v e A l l B i t m a p s S e r i a l i z e D a t a ( s t r i n g   d a t a ,   L i s t < s t r i n g >   s e l e c t e d E n t i t y N a m e s ,   o u t   s t r i n g   f o l d e r ,   o u t   s t r i n g   e x t e n s i o n ,   o u t   E n t i t i e s E d i t o r . B i t m a p P a r a m e t e r s   b i t m a p P a r a m e t e r s )  
 	 	 {  
 	 	 	 s e l e c t e d E n t i t y N a m e s . C l e a r   ( ) ;  
 	 	 	 f o l d e r   =   n u l l ;  
 	 	 	 e x t e n s i o n   =   n u l l ;  
 	 	 	 b i t m a p P a r a m e t e r s   =   n e w   E n t i t i e s E d i t o r . B i t m a p P a r a m e t e r s   ( ) ;  
  
 	 	 	 i f   ( ! s t r i n g . I s N u l l O r E m p t y   ( d a t a ) )  
 	 	 	 {  
 	 	 	 	 v a r   p a r t s   =   d a t a . S p l i t   ( ' Ê%' ) ;  
  
 	 	 	 	 i f   ( p a r t s . L e n g t h   >   0 )  
 	 	 	 	 {  
 	 	 	 	 	 v a r   n a m e s   =   p a r t s [ 0 ] . S p l i t   ( ' æ%' ) ;  
 	 	 	 	 	 f o r e a c h   ( v a r   n a m e   i n   n a m e s )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 i f   ( ! s t r i n g . I s N u l l O r E m p t y   ( n a m e ) )  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 s e l e c t e d E n t i t y N a m e s . A d d   ( n a m e ) ;  
 	 	 	 	 	 	 }  
 	 	 	 	 	 }  
 	 	 	 	 }  
  
 	 	 	 	 i f   ( p a r t s . L e n g t h   >   1 )  
 	 	 	 	 {  
 	 	 	 	 	 f o l d e r   =   p a r t s [ 1 ] ;  
 	 	 	 	 }  
  
 	 	 	 	 i f   ( p a r t s . L e n g t h   >   2 )  
 	 	 	 	 {  
 	 	 	 	 	 e x t e n s i o n   =   p a r t s [ 2 ] ;  
 	 	 	 	 }  
  
 	 	 	 	 i f   ( p a r t s . L e n g t h   >   3 )  
 	 	 	 	 {  
 	 	 	 	 	 b i t m a p P a r a m e t e r s . S e t S e r i a l i z e D a t a   ( p a r t s [ 3 ] ) ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 i f   ( s t r i n g . I s N u l l O r E m p t y   ( e x t e n s i o n ) )  
 	 	 	 {  
 	 	 	 	 e x t e n s i o n   =   " . p n g " ;  
 	 	 	 }  
 	 	 }  
  
  
 	 	 p u b l i c   s t a t i c   r e a d o n l y   d o u b l e 	 	 	 z o o m M i n   =   0 . 2 ;  
 	 	 p u b l i c   s t a t i c   r e a d o n l y   d o u b l e 	 	 	 z o o m M a x   =   2 . 0 ;  
 	 	 p r i v a t e   s t a t i c   r e a d o n l y   d o u b l e 	 	 	 z o o m D e f a u l t   =   1 . 0 ;  
  
 	 	 p r i v a t e   s t a t i c   i n t 	 	 	 	 	 	 s u b V i e w   =   0 ;  
 	 	 p r i v a t e   s t a t i c   d o u b l e 	 	 	 	 	 z o o m   =   E n t i t i e s . z o o m D e f a u l t ;  
 	 	 p r i v a t e   s t a t i c   b o o l 	 	 	 	 	 	 i s Z o o m P a g e   =   f a l s e ;  
 	 	 p r i v a t e   s t a t i c   s t r i n g 	 	 	 	 	 s o f t S e r i a l i z e   =   n u l l ;  
  
 	 	 p r i v a t e   H S p l i t t e r 	 	 	 	 	 	 h s p l i t t e r ;  
 	 	 p r i v a t e   E n t i t i e s E d i t o r . E d i t o r 	 	 	 e d i t o r ;  
 	 	 p r i v a t e   V S c r o l l e r 	 	 	 	 	 	 v s c r o l l e r ;  
 	 	 p r i v a t e   H S c r o l l e r 	 	 	 	 	 	 h s c r o l l e r ;  
 	 	 p r i v a t e   S i z e 	 	 	 	 	 	 	 a r e a S i z e ;  
 	 	 p r i v a t e   M y W i d g e t s . R e s e t B o x 	 	 	 	 g r o u p T o o l b a r ;  
 	 	 p r i v a t e   H T o o l B a r 	 	 	 	 	 	 t o o l b a r ;  
 	 	 p r i v a t e   M y W i d g e t s . E n t i t y S u b V i e w 	 	 	 b u t t o n S u b V i e w A ;  
 	 	 p r i v a t e   M y W i d g e t s . E n t i t y S u b V i e w 	 	 	 b u t t o n S u b V i e w B ;  
 	 	 p r i v a t e   M y W i d g e t s . E n t i t y S u b V i e w 	 	 	 b u t t o n S u b V i e w C ;  
 	 	 p r i v a t e   M y W i d g e t s . E n t i t y S u b V i e w 	 	 	 b u t t o n S u b V i e w T ;  
 	 	 p r i v a t e   I c o n B u t t o n 	 	 	 	 	 	 b u t t o n Z o o m P a g e ;  
 	 	 p r i v a t e   I c o n B u t t o n 	 	 	 	 	 	 b u t t o n Z o o m M i n ;  
 	 	 p r i v a t e   I c o n B u t t o n 	 	 	 	 	 	 b u t t o n Z o o m D e f a u l t ;  
 	 	 p r i v a t e   I c o n B u t t o n 	 	 	 	 	 	 b u t t o n Z o o m M a x ;  
 	 	 p r i v a t e   S t a t u s F i e l d 	 	 	 	 	 	 f i e l d Z o o m ;  
 	 	 p r i v a t e   H S l i d e r 	 	 	 	 	 	 	 s l i d e r Z o o m ;  
 	 	 p r i v a t e   I c o n B u t t o n 	 	 	 	 	 	 b u t t o n G r i d ;  
 	 	 p r i v a t e   I c o n B u t t o n 	 	 	 	 	 	 b u t t o n A d d E n t i t y ;  
 	 	 p r i v a t e   I c o n B u t t o n 	 	 	 	 	 	 b u t t o n S a v e B i t m a p ;  
 	 	 p r i v a t e   I c o n B u t t o n 	 	 	 	 	 	 b u t t o n S a v e A l l B i t m a p s ;  
 	 	 p r i v a t e   i n t 	 	 	 	 	 	 	 	 d r a g S t a r t i n g ;  
 	 	 p r i v a t e   D r u i d 	 	 	 	 	 	 	 d e s e r i a l i z e d D r u i d ;  
 	 	 p r i v a t e   i n t 	 	 	 	 	 	 	 	 d e s e r i a l i z e d S u b V i e w ;  
 	 	 p r i v a t e   D r u i d 	 	 	 	 	 	 	 d r u i d T o S e r i a l i z e ;  
 	 	 p r i v a t e   s t r i n g 	 	 	 	 	 	 	 n a m e T o S e r i a l i z e ;  
 	 	 p r i v a t e   G l y p h B u t t o n 	 	 	 	 	 	 s h o w H i d e T o p F r a m e B u t t o n ;  
 	 	 p r i v a t e   b o o l 	 	 	 	 	 	 	 s h o w T o p F r a m e ;  
 	 	 p r i v a t e   F r a m e B o x 	 	 	 	 	 	 e d i t o r G r o u p ;  
 	 }  
 }  
 