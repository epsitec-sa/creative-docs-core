ÿþu s i n g   E p s i t e c . C o m m o n . T y p e s ;  
  
 u s i n g   N U n i t . F r a m e w o r k ;  
  
 u s i n g   S y s t e m . C o l l e c t i o n s . G e n e r i c ;  
 u s i n g   E p s i t e c . C o m m o n . S u p p o r t ;  
 u s i n g   E p s i t e c . C o m m o n . D r a w i n g ;  
 u s i n g   E p s i t e c . C o m m o n . U I ;  
 u s i n g   E p s i t e c . C o m m o n . W i d g e t s . A d o r n e r s ;  
 u s i n g   E p s i t e c . C o m m o n . W i d g e t s ;  
  
 n a m e s p a c e   E p s i t e c . C o m m o n . T e s t s . S u p p o r t  
 {  
 	 [ T e s t F i x t u r e ]  
 	 p u b l i c   c l a s s   R e s o u r c e A c c e s s o r T e s t  
 	 {  
 	 	 [ S e t U p ]  
 	 	 p u b l i c   v o i d   I n i t i a l i z e ( )  
 	 	 {  
 	 	 	 E p s i t e c . C o m m o n . W i d g e t s . W i d g e t . I n i t i a l i z e   ( ) ;  
  
 	 	 	 t h i s . m a n a g e r   =   n e w   R e s o u r c e M a n a g e r   ( t y p e o f   ( R e s o u r c e A c c e s s o r T e s t ) ) ;  
 	 	 	 t h i s . m a n a g e r . D e f i n e D e f a u l t M o d u l e N a m e   ( " T e s t " ) ;  
  
 	 	 	 G l o b a l s . P r o p e r t i e s . S e t P r o p e r t y   ( E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e A c c e s s o r s . A b s t r a c t R e s o u r c e A c c e s s o r . D e v e l o p e r I d P r o p e r t y N a m e ,   0 ) ;  
 	 	 }  
 	 	  
 	 	 [ T e s t ]  
 	 	 p u b l i c   v o i d   A u t o m a t e d T e s t E n v i r o n m e n t ( )  
 	 	 {  
 	 	 	 E p s i t e c . C o m m o n . W i d g e t s . W i n d o w . R u n n i n g I n A u t o m a t e d T e s t E n v i r o n m e n t   =   t r u e ;  
 	 	 }  
  
 	 	 [ T e s t ]  
 	 	 p u b l i c   v o i d   C h e c k B a s i c T y p e s ( )  
 	 	 {  
 	 	 	 A s s e r t . I s N o t N u l l   ( E p s i t e c . C o m m o n . T y p e s . D r u i d T y p e . D e f a u l t ) ;  
  
 	 	 	 S t r u c t u r e d T y p e   t y p e S t r u c t   =   E p s i t e c . C o m m o n . S u p p o r t . R e s . T y p e s . R e s o u r c e S t r u c t u r e d T y p e ;  
 	 	 	 S t r u c t u r e d T y p e   t y p e F i e l d     =   E p s i t e c . C o m m o n . S u p p o r t . R e s . T y p e s . F i e l d ;  
 / / - 	 	 	 T y p e s . C o l l e c t i o n T y p e   t y p e F i e l d s   =   E p s i t e c . C o m m o n . S u p p o r t . R e s . T y p e s . F i e l d C o l l e c t i o n ;  
 	 	 	  
 	 	 	 E n u m T y p e   t y p e F i e l d R e l a t i o n       =   E p s i t e c . C o m m o n . T y p e s . R e s . T y p e s . F i e l d R e l a t i o n ;  
 	 	 	 E n u m T y p e   t y p e F i e l d M e m b e r s h i p   =   E p s i t e c . C o m m o n . T y p e s . R e s . T y p e s . F i e l d M e m b e r s h i p ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( t y p e F i e l d ,   t y p e S t r u c t . G e t F i e l d   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e S t r u c t u r e d T y p e . F i e l d s . T o S t r i n g   ( ) ) . T y p e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( F i e l d R e l a t i o n . C o l l e c t i o n ,   t y p e S t r u c t . G e t F i e l d   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e S t r u c t u r e d T y p e . F i e l d s . T o S t r i n g   ( ) ) . R e l a t i o n ) ;  
 / / - 	 	 	 A s s e r t . A r e E q u a l   ( t y p e F i e l d ,   t y p e F i e l d s . I t e m T y p e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( t y p e o f   ( F i e l d R e l a t i o n ) ,   t y p e F i e l d R e l a t i o n . S y s t e m T y p e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( t y p e o f   ( F i e l d M e m b e r s h i p ) ,   t y p e F i e l d M e m b e r s h i p . S y s t e m T y p e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( t y p e F i e l d R e l a t i o n . S y s t e m T y p e ,   t y p e F i e l d . G e t F i e l d   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . F i e l d . R e l a t i o n . T o S t r i n g   ( ) ) . T y p e . S y s t e m T y p e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( t y p e F i e l d M e m b e r s h i p . S y s t e m T y p e ,   t y p e F i e l d . G e t F i e l d   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . F i e l d . M e m b e r s h i p . T o S t r i n g   ( ) ) . T y p e . S y s t e m T y p e ) ;  
 	 	 }  
  
 	 	 [ T e s t ]  
 	 	 p u b l i c   v o i d   C h e c k C a p t i o n A c c e s s o r ( )  
 	 	 {  
 	 	 	 E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e A c c e s s o r s . C a p t i o n R e s o u r c e A c c e s s o r   a c c e s s o r   =   n e w   E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e A c c e s s o r s . C a p t i o n R e s o u r c e A c c e s s o r   ( ) ;  
  
 	 	 	 A s s e r t . I s F a l s e   ( a c c e s s o r . C o n t a i n s C h a n g e s ) ;  
  
 	 	 	 a c c e s s o r . L o a d   ( t h i s . m a n a g e r ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( 2 ,   a c c e s s o r . C o l l e c t i o n . C o u n t ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( D r u i d . P a r s e   ( " [ 4 0 0 2 ] " ) ,   a c c e s s o r . C o l l e c t i o n [ D r u i d . P a r s e   ( " [ 4 0 0 2 ] " ) ] . I d ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " P a t t e r n A n g l e " ,   a c c e s s o r . C o l l e c t i o n [ D r u i d . P a r s e   ( " [ 4 0 0 2 ] " ) ] . N a m e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " P a t t e r n   a n g l e   e x p r e s s e d   i n   d e g r e e s . " ,   a c c e s s o r . C o l l e c t i o n [ D r u i d . P a r s e   ( " [ 4 0 0 2 ] " ) ] . G e t C u l t u r e D a t a   ( " 0 0 " ) . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e C a p t i o n . D e s c r i p t i o n ) ) ;  
  
 	 	 	 S t r u c t u r e d D a t a   d a t a 1   =   a c c e s s o r . C o l l e c t i o n [ " P a t t e r n A n g l e " ] . G e t C u l t u r e D a t a   ( " f r " ) ;  
 	 	 	 S t r u c t u r e d D a t a   d a t a 2   =   a c c e s s o r . C o l l e c t i o n [ " P a t t e r n A n g l e " ] . G e t C u l t u r e D a t a   ( " f r " ) ;  
  
 	 	 	 A s s e r t . A r e S a m e   ( d a t a 1 ,   d a t a 2 ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " A n g l e   d e   r o t a t i o n   d e   l a   t r a m e ,   e x p r i m é   e n   d e g r é s . " ,   d a t a 1 . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e C a p t i o n . D e s c r i p t i o n ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( 3 ,   ( d a t a 1 . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e C a p t i o n . L a b e l s )   a s   I L i s t < s t r i n g > ) . C o u n t ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " A " ,   ( d a t a 1 . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e C a p t i o n . L a b e l s )   a s   I L i s t < s t r i n g > ) [ 0 ] ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " A n g l e " ,   ( d a t a 1 . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e C a p t i o n . L a b e l s )   a s   I L i s t < s t r i n g > ) [ 1 ] ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " A n g l e   d e   l a   t r a m e " ,   ( d a t a 1 . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e C a p t i o n . L a b e l s )   a s   I L i s t < s t r i n g > ) [ 2 ] ) ;  
 	 	 	 A s s e r t . I s T r u e   ( d a t a 1 . I s V a l u e L o c k e d   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e C a p t i o n . L a b e l s ) ) ;  
 	 	 	 A s s e r t . I s F a l s e   ( a c c e s s o r . C o n t a i n s C h a n g e s ) ;  
  
 	 	 	 d a t a 1   =   a c c e s s o r . C o l l e c t i o n [ " P a t t e r n A n g l e " ] . G e t C u l t u r e D a t a   ( " d e " ) ;  
 	 	 	 d a t a 2   =   a c c e s s o r . C o l l e c t i o n [ " P a t t e r n A n g l e " ] . G e t C u l t u r e D a t a   ( " d e " ) ;  
  
 	 	 	 A s s e r t . I s N o t N u l l   ( d a t a 1 ) ;  
 	 	 	 A s s e r t . A r e S a m e   ( d a t a 1 ,   d a t a 2 ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( 0 ,   ( ( I L i s t < s t r i n g > ) d a t a 1 . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e C a p t i o n . L a b e l s ) ) . C o u n t ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( U n d e f i n e d V a l u e . V a l u e ,   d a t a 1 . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e C a p t i o n . D e s c r i p t i o n ) ) ;  
 	 	 	 A s s e r t . I s F a l s e   ( a c c e s s o r . C o n t a i n s C h a n g e s ) ;  
  
 	 	 	 d a t a 1   =   a c c e s s o r . C o l l e c t i o n [ " P a t t e r n A n g l e " ] . G e t C u l t u r e D a t a   ( " f r " ) ;  
 	 	 	 d a t a 1 . S e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e C a p t i o n . D e s c r i p t i o n ,   " A n g l e   d e   l a   h a c h u r e " ) ;  
 	 	 	 d a t a 2 . S e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e C a p t i o n . D e s c r i p t i o n ,   " S c h r a f f u r w i n k e l " ) ;  
  
 	 	 	 A s s e r t . I s T r u e   ( a c c e s s o r . C o n t a i n s C h a n g e s ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( 1 ,   a c c e s s o r . P e r s i s t C h a n g e s   ( ) ) ;  
 	 	 	 A s s e r t . I s F a l s e   ( a c c e s s o r . C o n t a i n s C h a n g e s ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( " A n g l e   d e   l a   h a c h u r e " ,   t h i s . m a n a g e r . G e t C a p t i o n   ( D r u i d . P a r s e   ( " [ 4 0 0 2 ] " ) ,   R e s o u r c e L e v e l . M e r g e d ,   E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e s . F i n d C u l t u r e I n f o   ( " f r " ) ) . D e s c r i p t i o n ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " S c h r a f f u r w i n k e l " ,   t h i s . m a n a g e r . G e t C a p t i o n   ( D r u i d . P a r s e   ( " [ 4 0 0 2 ] " ) ,   R e s o u r c e L e v e l . M e r g e d ,   E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e s . F i n d C u l t u r e I n f o   ( " d e " ) ) . D e s c r i p t i o n ) ;  
  
 	 	 	 I L i s t < s t r i n g >   l a b e l s   =   d a t a 1 . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e C a p t i o n . L a b e l s )   a s   I L i s t < s t r i n g > ;  
  
 	 	 	 l a b e l s . R e m o v e A t   ( 2 ) ;  
  
 	 	 	 A s s e r t . I s T r u e   ( a c c e s s o r . C o n t a i n s C h a n g e s ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( 1 ,   a c c e s s o r . P e r s i s t C h a n g e s   ( ) ) ;  
 	 	 	 A s s e r t . I s F a l s e   ( a c c e s s o r . C o n t a i n s C h a n g e s ) ;  
  
 	 	 	 l a b e l s [ 0 ]   =   " A . " ;  
  
 	 	 	 A s s e r t . I s T r u e   ( a c c e s s o r . C o n t a i n s C h a n g e s ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( 1 ,   a c c e s s o r . P e r s i s t C h a n g e s   ( ) ) ;  
 	 	 	 A s s e r t . I s F a l s e   ( a c c e s s o r . C o n t a i n s C h a n g e s ) ;  
  
 	 	 	 C u l t u r e M a p   m a p   =   a c c e s s o r . C r e a t e I t e m   ( ) ;  
  
 	 	 	 A s s e r t . I s N o t N u l l   ( m a p ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( D r u i d . P a r s e   ( " [ 4 0 0 C ] " ) ,   m a p . I d ) ;  
 	 	 	 A s s e r t . I s N u l l   ( a c c e s s o r . C o l l e c t i o n [ m a p . I d ] ) ;  
  
 	 	 	 a c c e s s o r . C o l l e c t i o n . A d d   ( m a p ) ;  
 	 	 	 A s s e r t . I s T r u e   ( a c c e s s o r . C o n t a i n s C h a n g e s ) ;  
  
 	 	 	 m a p . N a m e   =   " N e w I t e m " ;  
 	 	 	 m a p . G e t C u l t u r e D a t a   ( " 0 0 " ) . S e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e C a p t i o n . D e s c r i p t i o n ,   " N e w   v a l u e " ) ;  
 	 	 	 m a p . G e t C u l t u r e D a t a   ( " f r " ) . S e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e C a p t i o n . D e s c r i p t i o n ,   " N o u v e l l e   v a l e u r " ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( 1 ,   a c c e s s o r . P e r s i s t C h a n g e s   ( ) ) ;  
 	 	 	 A s s e r t . I s F a l s e   ( a c c e s s o r . C o n t a i n s C h a n g e s ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( " N e w   v a l u e " ,   t h i s . m a n a g e r . G e t C a p t i o n   ( D r u i d . P a r s e   ( " [ 4 0 0 C ] " ) ,   R e s o u r c e L e v e l . D e f a u l t ) . D e s c r i p t i o n ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " N o u v e l l e   v a l e u r " ,   t h i s . m a n a g e r . G e t C a p t i o n   ( D r u i d . P a r s e   ( " [ 4 0 0 C ] " ) ,   R e s o u r c e L e v e l . M e r g e d ,   E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e s . F i n d C u l t u r e I n f o   ( " f r " ) ) . D e s c r i p t i o n ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " N e w I t e m " ,   t h i s . m a n a g e r . G e t C a p t i o n   ( D r u i d . P a r s e   ( " [ 4 0 0 C ] " ) ,   R e s o u r c e L e v e l . D e f a u l t ) . N a m e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " N e w I t e m " ,   t h i s . m a n a g e r . G e t C a p t i o n   ( D r u i d . P a r s e   ( " [ 4 0 0 C ] " ) ,   R e s o u r c e L e v e l . M e r g e d ,   E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e s . F i n d C u l t u r e I n f o   ( " f r " ) ) . N a m e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " C a p . N e w I t e m " ,   t h i s . m a n a g e r . G e t B u n d l e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e s . C a p t i o n s B u n d l e N a m e ,   R e s o u r c e L e v e l . D e f a u l t ) [ D r u i d . P a r s e   ( " [ 4 0 0 C ] " ) ] . N a m e ) ;  
 	 	 	 A s s e r t . I s T r u e   ( s t r i n g . I s N u l l O r E m p t y   ( t h i s . m a n a g e r . G e t B u n d l e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e s . C a p t i o n s B u n d l e N a m e ,   R e s o u r c e L e v e l . L o c a l i z e d ,   E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e s . F i n d C u l t u r e I n f o   ( " f r " ) ) [ D r u i d . P a r s e   ( " [ 4 0 0 C ] " ) ] . N a m e ) ) ;  
  
 	 	 	 m a p . G e t C u l t u r e D a t a   ( " f r " ) . S e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e C a p t i o n . D e s c r i p t i o n ,   U n d e f i n e d V a l u e . V a l u e ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( 1 ,   a c c e s s o r . P e r s i s t C h a n g e s   ( ) ) ;  
 	 	 	 A s s e r t . I s F a l s e   ( a c c e s s o r . C o n t a i n s C h a n g e s ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( " N e w   v a l u e " ,   t h i s . m a n a g e r . G e t C a p t i o n   ( D r u i d . P a r s e   ( " [ 4 0 0 C ] " ) ,   R e s o u r c e L e v e l . D e f a u l t ) . D e s c r i p t i o n ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " N e w   v a l u e " ,   t h i s . m a n a g e r . G e t C a p t i o n   ( D r u i d . P a r s e   ( " [ 4 0 0 C ] " ) ,   R e s o u r c e L e v e l . M e r g e d ,   E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e s . F i n d C u l t u r e I n f o   ( " f r " ) ) . D e s c r i p t i o n ) ;  
  
 	 	 	 a c c e s s o r . C o l l e c t i o n . R e m o v e   ( m a p ) ;  
 	 	 	 A s s e r t . I s T r u e   ( a c c e s s o r . C o n t a i n s C h a n g e s ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( 1 ,   a c c e s s o r . P e r s i s t C h a n g e s   ( ) ) ;  
 	 	 	 A s s e r t . I s F a l s e   ( a c c e s s o r . C o n t a i n s C h a n g e s ) ;  
  
 	 	 	 t h i s . m a n a g e r . C l e a r C a p t i o n C a c h e   ( D r u i d . P a r s e   ( " [ 4 0 0 C ] " ) ,   R e s o u r c e L e v e l . A l l ,   E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e s . F i n d C u l t u r e I n f o   ( " f r " ) ) ;  
  
 	 	 	 A s s e r t . I s N u l l   ( t h i s . m a n a g e r . G e t C a p t i o n   ( D r u i d . P a r s e   ( " [ 4 0 0 C ] " ) ,   R e s o u r c e L e v e l . D e f a u l t ) ) ;  
 	 	 	 A s s e r t . I s N u l l   ( t h i s . m a n a g e r . G e t C a p t i o n   ( D r u i d . P a r s e   ( " [ 4 0 0 C ] " ) ,   R e s o u r c e L e v e l . M e r g e d ,   E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e s . F i n d C u l t u r e I n f o   ( " f r " ) ) ) ;  
 	 	 }  
  
 	 	 [ T e s t ]  
 	 	 p u b l i c   v o i d   C h e c k C a p t i o n A c c e s s o r R e v e r t ( )  
 	 	 {  
 	 	 	 E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e A c c e s s o r s . C a p t i o n R e s o u r c e A c c e s s o r   a c c e s s o r   =   n e w   E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e A c c e s s o r s . C a p t i o n R e s o u r c e A c c e s s o r   ( ) ;  
 	 	 	  
 	 	 	 a c c e s s o r . L o a d   ( t h i s . m a n a g e r ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( 2 ,   a c c e s s o r . C o l l e c t i o n . C o u n t ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( D r u i d . P a r s e   ( " [ 4 0 0 2 ] " ) ,   a c c e s s o r . C o l l e c t i o n [ D r u i d . P a r s e   ( " [ 4 0 0 2 ] " ) ] . I d ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " P a t t e r n A n g l e " ,   a c c e s s o r . C o l l e c t i o n [ D r u i d . P a r s e   ( " [ 4 0 0 2 ] " ) ] . N a m e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " P a t t e r n   a n g l e   e x p r e s s e d   i n   d e g r e e s . " ,   a c c e s s o r . C o l l e c t i o n [ D r u i d . P a r s e   ( " [ 4 0 0 2 ] " ) ] . G e t C u l t u r e D a t a   ( " 0 0 " ) . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e C a p t i o n . D e s c r i p t i o n ) ) ;  
  
 	 	 	 S t r u c t u r e d D a t a   d a t a 1 ;  
 	 	 	 S t r u c t u r e d D a t a   d a t a 2 ;  
  
 	 	 	 d a t a 1   =   a c c e s s o r . C o l l e c t i o n [ " P a t t e r n A n g l e " ] . G e t C u l t u r e D a t a   ( " f r " ) ;  
 	 	 	 d a t a 2   =   a c c e s s o r . C o l l e c t i o n [ " P a t t e r n A n g l e " ] . G e t C u l t u r e D a t a   ( " d e " ) ;  
 	 	 	  
 	 	 	 d a t a 1 . S e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e C a p t i o n . D e s c r i p t i o n ,   " A n g l e   d e   l a   h a c h u r e " ) ;  
 	 	 	 d a t a 2 . S e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e C a p t i o n . D e s c r i p t i o n ,   " S c h r a f f u r w i n k e l " ) ;  
  
 	 	 	 A s s e r t . I s T r u e   ( a c c e s s o r . C o n t a i n s C h a n g e s ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( 1 ,   a c c e s s o r . R e v e r t C h a n g e s   ( ) ) ;  
 	 	 	 A s s e r t . I s F a l s e   ( a c c e s s o r . C o n t a i n s C h a n g e s ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( " A n g l e   d e   r o t a t i o n   d e   l a   t r a m e ,   e x p r i m é   e n   d e g r é s . " ,   t h i s . m a n a g e r . G e t C a p t i o n   ( D r u i d . P a r s e   ( " [ 4 0 0 2 ] " ) ,   R e s o u r c e L e v e l . M e r g e d ,   E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e s . F i n d C u l t u r e I n f o   ( " f r " ) ) . D e s c r i p t i o n ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " P a t t e r n   a n g l e   e x p r e s s e d   i n   d e g r e e s . " ,   t h i s . m a n a g e r . G e t C a p t i o n   ( D r u i d . P a r s e   ( " [ 4 0 0 2 ] " ) ,   R e s o u r c e L e v e l . M e r g e d ,   E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e s . F i n d C u l t u r e I n f o   ( " d e " ) ) . D e s c r i p t i o n ) ;  
  
 	 	 	 I L i s t < s t r i n g >   l a b e l s ;  
  
 	 	 	 l a b e l s   =   d a t a 1 . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e C a p t i o n . L a b e l s )   a s   I L i s t < s t r i n g > ;  
 	 	 	 l a b e l s . R e m o v e A t   ( 2 ) ;  
  
 	 	 	 A s s e r t . I s T r u e   ( a c c e s s o r . C o n t a i n s C h a n g e s ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( 1 ,   a c c e s s o r . R e v e r t C h a n g e s   ( ) ) ;  
 	 	 	 A s s e r t . I s F a l s e   ( a c c e s s o r . C o n t a i n s C h a n g e s ) ;  
 	 	 	 d a t a 1   =   a c c e s s o r . C o l l e c t i o n [ " P a t t e r n A n g l e " ] . G e t C u l t u r e D a t a   ( " f r " ) ;  
 	 	 	 l a b e l s   =   d a t a 1 . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e C a p t i o n . L a b e l s )   a s   I L i s t < s t r i n g > ;  
 	 	 	 A s s e r t . A r e E q u a l   ( 3 ,   ( d a t a 1 . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e C a p t i o n . L a b e l s )   a s   I L i s t < s t r i n g > ) . C o u n t ) ;  
 	 	 	  
 	 	 	 l a b e l s [ 0 ]   =   " A . " ;  
  
 	 	 	 A s s e r t . I s T r u e   ( a c c e s s o r . C o n t a i n s C h a n g e s ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( 1 ,   a c c e s s o r . R e v e r t C h a n g e s   ( ) ) ;  
 	 	 	 A s s e r t . I s F a l s e   ( a c c e s s o r . C o n t a i n s C h a n g e s ) ;  
 	 	 	 d a t a 1   =   a c c e s s o r . C o l l e c t i o n [ " P a t t e r n A n g l e " ] . G e t C u l t u r e D a t a   ( " f r " ) ;  
 	 	 	 l a b e l s   =   d a t a 1 . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e C a p t i o n . L a b e l s )   a s   I L i s t < s t r i n g > ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " A " ,   ( d a t a 1 . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e C a p t i o n . L a b e l s )   a s   I L i s t < s t r i n g > ) [ 0 ] ) ;  
 	 	 	  
 	 	 	 C u l t u r e M a p   m a p   =   a c c e s s o r . C r e a t e I t e m   ( ) ;  
  
 	 	 	 A s s e r t . I s N o t N u l l   ( m a p ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( D r u i d . P a r s e   ( " [ 4 0 0 C ] " ) ,   m a p . I d ) ;  
 	 	 	 A s s e r t . I s N u l l   ( a c c e s s o r . C o l l e c t i o n [ m a p . I d ] ) ;  
  
 	 	 	 a c c e s s o r . C o l l e c t i o n . A d d   ( m a p ) ;  
 	 	 	 A s s e r t . I s T r u e   ( a c c e s s o r . C o n t a i n s C h a n g e s ) ;  
  
 	 	 	 m a p . N a m e   =   " N e w I t e m " ;  
 	 	 	 m a p . G e t C u l t u r e D a t a   ( " 0 0 " ) . S e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e C a p t i o n . D e s c r i p t i o n ,   " N e w   v a l u e " ) ;  
 	 	 	 m a p . G e t C u l t u r e D a t a   ( " f r " ) . S e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e C a p t i o n . D e s c r i p t i o n ,   " N o u v e l l e   v a l e u r " ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( 1 ,   a c c e s s o r . R e v e r t C h a n g e s   ( ) ) ;  
 	 	 	 A s s e r t . I s F a l s e   ( a c c e s s o r . C o n t a i n s C h a n g e s ) ;  
 	 	 	 A s s e r t . I s N u l l   ( a c c e s s o r . C o l l e c t i o n [ m a p . I d ] ) ;  
 	 	 }  
  
 	 	 [ T e s t ]  
 	 	 p u b l i c   v o i d   C h e c k C o m m a n d A c c e s s o r ( )  
 	 	 {  
 	 	 	 E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e A c c e s s o r s . C o m m a n d R e s o u r c e A c c e s s o r   a c c e s s o r   =   n e w   E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e A c c e s s o r s . C o m m a n d R e s o u r c e A c c e s s o r   ( ) ;  
  
 	 	 	 A s s e r t . I s F a l s e   ( a c c e s s o r . C o n t a i n s C h a n g e s ) ;  
  
 	 	 	 a c c e s s o r . L o a d   ( t h i s . m a n a g e r ) ;  
 	 	 	  
 	 	 	 A s s e r t . A r e E q u a l   ( 1 ,   a c c e s s o r . C o l l e c t i o n . C o u n t ) ;  
 	 	 }  
  
 	 	 [ T e s t ]  
 	 	 p u b l i c   v o i d   C h e c k S t r u c t u r e d T y p e A c c e s s o r ( )  
 	 	 {  
 	 	 	 E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e A c c e s s o r s . S t r u c t u r e d T y p e R e s o u r c e A c c e s s o r   a c c e s s o r   =   n e w   E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e A c c e s s o r s . S t r u c t u r e d T y p e R e s o u r c e A c c e s s o r   ( ) ;  
  
 	 	 	 A s s e r t . I s F a l s e   ( a c c e s s o r . C o n t a i n s C h a n g e s ) ;  
  
 	 	 	 a c c e s s o r . L o a d   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . M a n a g e r ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( 2 2 ,   a c c e s s o r . C o l l e c t i o n . C o u n t ) ;  
  
 	 	 	 C u l t u r e M a p   m a p   =   a c c e s s o r . C o l l e c t i o n [ E p s i t e c . C o m m o n . S u p p o r t . R e s . T y p e s . R e s o u r c e S t r u c t u r e d T y p e . C a p t i o n I d ] ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( " R e s o u r c e S t r u c t u r e d T y p e " ,   m a p . N a m e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " T y p . S t r u c t u r e d T y p e . R e s o u r c e S t r u c t u r e d T y p e " ,   E p s i t e c . C o m m o n . S u p p o r t . R e s . M a n a g e r . G e t B u n d l e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e s . C a p t i o n s B u n d l e N a m e ,   R e s o u r c e L e v e l . D e f a u l t ) [ E p s i t e c . C o m m o n . S u p p o r t . R e s . T y p e s . R e s o u r c e S t r u c t u r e d T y p e . C a p t i o n I d ] . N a m e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " F l d . R e s o u r c e S t r u c t u r e d T y p e . F i e l d s " ,   E p s i t e c . C o m m o n . S u p p o r t . R e s . M a n a g e r . G e t B u n d l e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e s . C a p t i o n s B u n d l e N a m e ,   R e s o u r c e L e v e l . D e f a u l t ) [ E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e S t r u c t u r e d T y p e . F i e l d s ] . N a m e ) ;  
  
 	 	 	 S t r u c t u r e d D a t a                 d a t a               =   m a p . G e t C u l t u r e D a t a   ( E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e s . D e f a u l t T w o L e t t e r I S O L a n g u a g e N a m e ) ;  
 	 	 	 S t r u c t u r e d T y p e C l a s s       t y p e C l a s s     =   ( S t r u c t u r e d T y p e C l a s s )   d a t a . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e S t r u c t u r e d T y p e . C l a s s ) ;  
 	 	 	 D r u i d                                   b a s e T y p e I d   =   ( D r u i d )   d a t a . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e S t r u c t u r e d T y p e . B a s e T y p e ) ;  
 	 	 	 I L i s t < S t r u c t u r e d D a t a >   f i e l d s           =   d a t a . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e S t r u c t u r e d T y p e . F i e l d s )   a s   I L i s t < S t r u c t u r e d D a t a > ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( 1 5 ,   f i e l d s . C o u n t ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e B a s e . M o d i f i c a t i o n I d ,   f i e l d s [ 0 ] . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . F i e l d . C a p t i o n I d ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e B a s e . C o m m e n t ,                 f i e l d s [ 1 ] . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . F i e l d . C a p t i o n I d ) ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( F i e l d S o u r c e . V a l u e ,   f i e l d s [ 0 ] . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . F i e l d . S o u r c e ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " " ,   f i e l d s [ 0 ] . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . F i e l d . E x p r e s s i o n ) ) ;  
 	 	 	  
 	 	 	 / / 	 C h e c k   t h a t   d e f i n i n g   t y p e   i d   p r o p e r l y   d e f i n e d   ( ! )   b a s e d   o n   w h e r e   t h e  
 	 	 	 / / 	 f i e l d   o r i g i n a t e s   :  
 	 	 	  
 	 	 	 A s s e r t . A r e E q u a l   ( D r u i d . P a r s e   ( " [ 7 0 0 5 ] " ) ,   f i e l d s [ 0 ] . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . F i e l d . D e f i n i n g T y p e I d ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( D r u i d . P a r s e   ( " [ 7 0 0 5 ] " ) ,     f i e l d s [ 2 ] . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . F i e l d . D e f i n i n g T y p e I d ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( D r u i d . P a r s e   ( " [ 7 0 0 5 ] " ) ,     f i e l d s [ 7 ] . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . F i e l d . D e f i n i n g T y p e I d ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( D r u i d . E m p t y ,   f i e l d s [ 1 3 ] . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . F i e l d . D e f i n i n g T y p e I d ) ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( D r u i d . P a r s e   ( " [ 7 0 0 B 1 ] " ) ,   f i e l d s [ 0 ] . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . F i e l d . D e e p D e f i n i n g T y p e I d ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( D r u i d . P a r s e   ( " [ 7 0 0 6 ] " ) ,   f i e l d s [ 2 ] . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . F i e l d . D e e p D e f i n i n g T y p e I d ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( D r u i d . P a r s e   ( " [ 7 0 0 5 ] " ) ,   f i e l d s [ 7 ] . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . F i e l d . D e e p D e f i n i n g T y p e I d ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( D r u i d . E m p t y ,   f i e l d s [ 1 3 ] . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . F i e l d . D e e p D e f i n i n g T y p e I d ) ) ;  
  
 	 	 	 m a p . N a m e   =   " R e s o u r c e E n t i t y T y p e " ;  
 	 	 	 f i e l d s [ 1 1 ] . S e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . F i e l d . S o u r c e ,   F i e l d S o u r c e . E x p r e s s i o n ) ;  
 	 	 	 f i e l d s [ 1 1 ] . S e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . F i e l d . E x p r e s s i o n ,   " f o o " ) ;  
  
 	 	 	 a c c e s s o r . P e r s i s t C h a n g e s   ( ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( " R e s o u r c e E n t i t y T y p e " ,   m a p . N a m e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " T y p . S t r u c t u r e d T y p e . R e s o u r c e E n t i t y T y p e " ,   E p s i t e c . C o m m o n . S u p p o r t . R e s . M a n a g e r . G e t B u n d l e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e s . C a p t i o n s B u n d l e N a m e ,   R e s o u r c e L e v e l . D e f a u l t ) [ E p s i t e c . C o m m o n . S u p p o r t . R e s . T y p e s . R e s o u r c e S t r u c t u r e d T y p e . C a p t i o n I d ] . N a m e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " F l d . R e s o u r c e E n t i t y T y p e . F i e l d s " ,   E p s i t e c . C o m m o n . S u p p o r t . R e s . M a n a g e r . G e t B u n d l e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e s . C a p t i o n s B u n d l e N a m e ,   R e s o u r c e L e v e l . D e f a u l t ) [ E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e S t r u c t u r e d T y p e . F i e l d s ] . N a m e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " R e s o u r c e E n t i t y T y p e . F i e l d s " ,   a c c e s s o r . F i e l d A c c e s s o r . C o l l e c t i o n [ E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e S t r u c t u r e d T y p e . F i e l d s ] . T o S t r i n g   ( ) ) ;  
  
 	 	 	 C a p t i o n   c a p t i o n   =   E p s i t e c . C o m m o n . S u p p o r t . R e s . M a n a g e r . G e t C a p t i o n   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . T y p e s . R e s o u r c e S t r u c t u r e d T y p e . C a p t i o n I d ,   R e s o u r c e L e v e l . D e f a u l t ) ;  
 	 	 	 S t r u c t u r e d T y p e   t y p e   =   T y p e R o s e t t a . C r e a t e T y p e O b j e c t   ( c a p t i o n ,   f a l s e )   a s   S t r u c t u r e d T y p e ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( " R e s o u r c e E n t i t y T y p e " ,   c a p t i o n . N a m e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( F i e l d S o u r c e . E x p r e s s i o n ,   t y p e . F i e l d s [ E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e S t r u c t u r e d T y p e . C l a s s . T o S t r i n g   ( ) ] . S o u r c e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " f o o " ,   t y p e . F i e l d s [ E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e S t r u c t u r e d T y p e . C l a s s . T o S t r i n g   ( ) ] . E x p r e s s i o n ) ;  
 	 	 	  
 	 	 	 m a p . N a m e   =   " R e s o u r c e S t r u c t u r e d T y p e " ;  
 	 	 	 a c c e s s o r . P e r s i s t C h a n g e s   ( ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( " R e s o u r c e S t r u c t u r e d T y p e " ,   m a p . N a m e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " T y p . S t r u c t u r e d T y p e . R e s o u r c e S t r u c t u r e d T y p e " ,   E p s i t e c . C o m m o n . S u p p o r t . R e s . M a n a g e r . G e t B u n d l e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e s . C a p t i o n s B u n d l e N a m e ,   R e s o u r c e L e v e l . D e f a u l t ) [ E p s i t e c . C o m m o n . S u p p o r t . R e s . T y p e s . R e s o u r c e S t r u c t u r e d T y p e . C a p t i o n I d ] . N a m e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " F l d . R e s o u r c e S t r u c t u r e d T y p e . F i e l d s " ,   E p s i t e c . C o m m o n . S u p p o r t . R e s . M a n a g e r . G e t B u n d l e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e s . C a p t i o n s B u n d l e N a m e ,   R e s o u r c e L e v e l . D e f a u l t ) [ E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e S t r u c t u r e d T y p e . F i e l d s ] . N a m e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " R e s o u r c e S t r u c t u r e d T y p e . F i e l d s " ,   a c c e s s o r . F i e l d A c c e s s o r . C o l l e c t i o n [ E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e S t r u c t u r e d T y p e . F i e l d s ] . T o S t r i n g   ( ) ) ;  
  
 	 	 	 C u l t u r e M a p   f i e l d s I t e m ;  
 	 	 	  
 	 	 	 f i e l d s I t e m   =   a c c e s s o r . F i e l d A c c e s s o r . C o l l e c t i o n [ E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e S t r u c t u r e d T y p e . F i e l d s ] ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( " F i e l d s " ,   f i e l d s I t e m . N a m e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " R e s o u r c e S t r u c t u r e d T y p e . F i e l d s " ,   f i e l d s I t e m . T o S t r i n g   ( ) ) ;  
 	 	 	  
 	 	 	 f i e l d s I t e m   =   a c c e s s o r . C r e a t e F i e l d I t e m   ( m a p ) ;  
  
 	 	 	 f i e l d s I t e m . N a m e   =   " X " ;  
  
 	 	 	 a c c e s s o r . F i e l d A c c e s s o r . C o l l e c t i o n . A d d   ( f i e l d s I t e m ) ;  
 	 	 	 a c c e s s o r . F i e l d A c c e s s o r . P e r s i s t C h a n g e s   ( ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( " F l d . R e s o u r c e S t r u c t u r e d T y p e . X " ,   E p s i t e c . C o m m o n . S u p p o r t . R e s . M a n a g e r . G e t B u n d l e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e s . C a p t i o n s B u n d l e N a m e ,   R e s o u r c e L e v e l . D e f a u l t ) [ f i e l d s I t e m . I d ] . N a m e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " R e s o u r c e S t r u c t u r e d T y p e . X " ,   a c c e s s o r . F i e l d A c c e s s o r . C o l l e c t i o n [ f i e l d s I t e m . I d ] . T o S t r i n g   ( ) ) ;  
  
 	 	 	 a c c e s s o r . F i e l d A c c e s s o r . C o l l e c t i o n . R e m o v e   ( f i e l d s I t e m ) ;  
 	 	 	 a c c e s s o r . F i e l d A c c e s s o r . P e r s i s t C h a n g e s   ( ) ;  
  
 	 	 	 A s s e r t . I s T r u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . M a n a g e r . G e t B u n d l e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e s . C a p t i o n s B u n d l e N a m e ,   R e s o u r c e L e v e l . D e f a u l t ) [ f i e l d s I t e m . I d ] . I s E m p t y ) ;  
  
 	 	 	 I L i s t < S t r u c t u r e d D a t a >   i n t e r f a c e I d s   =   d a t a . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e S t r u c t u r e d T y p e . I n t e r f a c e I d s )   a s   I L i s t < S t r u c t u r e d D a t a > ;  
  
 	 	 	 A s s e r t . I s N o t N u l l   ( i n t e r f a c e I d s ) ;  
  
 	 	 	 I D a t a B r o k e r   b r o k e r   =   a c c e s s o r . G e t D a t a B r o k e r   ( d a t a ,   E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e S t r u c t u r e d T y p e . I n t e r f a c e I d s . T o S t r i n g   ( ) ) ;  
 	 	 	 S t r u c t u r e d D a t a   i n t e r f a c e I d D a t a   =   b r o k e r . C r e a t e D a t a   ( m a p ) ;  
  
 	 	 	 i n t e r f a c e I d D a t a . S e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . I n t e r f a c e I d . C a p t i o n I d ,   D r u i d . P a r s e   ( " [ 7 0 0 I 2 ] " ) ) ;  
  
 	 	 	 i n t e r f a c e I d s . A d d   ( i n t e r f a c e I d D a t a ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( 1 7 ,   f i e l d s . C o u n t ) ;  
 	 	 }  
  
  
 	 	 [ T e s t ]  
 	 	 p u b l i c   v o i d   C h e c k S t r u c t u r e d T y p e A c c e s s o r 2 ( )  
 	 	 {  
 	 	 	 E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e A c c e s s o r s . S t r u c t u r e d T y p e R e s o u r c e A c c e s s o r   a c c e s s o r   =   n e w   E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e A c c e s s o r s . S t r u c t u r e d T y p e R e s o u r c e A c c e s s o r   ( ) ;  
 	 	 	 a c c e s s o r . L o a d   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . M a n a g e r ) ;  
  
 	 	 	 C u l t u r e M a p   m a p   =   a c c e s s o r . C o l l e c t i o n [ n u l l / * E p s i t e c . C o m m o n . S u p p o r t . R e s . T y p e s . T e s t I n t e r f a c e U s e r . C a p t i o n I d * / ] ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( " T e s t I n t e r f a c e U s e r " ,   m a p . N a m e ) ;  
  
 	 	 	 S t r u c t u r e d D a t a   d a t a   =   m a p . G e t C u l t u r e D a t a   ( E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e s . D e f a u l t T w o L e t t e r I S O L a n g u a g e N a m e ) ;  
 	 	 	 S t r u c t u r e d T y p e C l a s s   t y p e C l a s s   =   ( S t r u c t u r e d T y p e C l a s s )   d a t a . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e S t r u c t u r e d T y p e . C l a s s ) ;  
 	 	 	 D r u i d   b a s e T y p e I d   =   ( D r u i d )   d a t a . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e S t r u c t u r e d T y p e . B a s e T y p e ) ;  
 	 	 	 I L i s t < S t r u c t u r e d D a t a >   f i e l d s   =   d a t a . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e S t r u c t u r e d T y p e . F i e l d s )   a s   I L i s t < S t r u c t u r e d D a t a > ;  
 	 	 	 I L i s t < S t r u c t u r e d D a t a >   i n t e r f a c e I d s   =   d a t a . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e S t r u c t u r e d T y p e . I n t e r f a c e I d s )   a s   I L i s t < S t r u c t u r e d D a t a > ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( 3 ,   f i e l d s . C o u n t ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( 1 ,   i n t e r f a c e I d s . C o u n t ) ;  
 	 	 	  
 	 	 	 A s s e r t . A r e E q u a l   ( D r u i d . E m p t y ,   b a s e T y p e I d ) ;  
 	 	 	 / / A s s e r t . A r e E q u a l   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . T y p e s . T e s t I n t e r f a c e . C a p t i o n I d ,   ( D r u i d )   i n t e r f a c e I d s [ 0 ] . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . I n t e r f a c e I d . C a p t i o n I d ) ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( " [ 7 0 0 J 2 ] " ,   f i e l d s [ 0 ] . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . F i e l d . C a p t i o n I d ) . T o S t r i n g   ( ) ) ; 	 / / 	 f r o m   i n t e r f a c e ,   " N a m e "   - -   f i e l d   r e d e f i n e d   b y   T e s t I n t e r f a c e U s e r  
 	 	 	 A s s e r t . A r e E q u a l   ( " [ 7 0 1 2 ] " ,   f i e l d s [ 1 ] . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . F i e l d . C a p t i o n I d ) . T o S t r i n g   ( ) ) ; 	 / / 	 f r o m   i n t e r f a c e ,   " R e s o u r c e "  
 	 	 	 A s s e r t . A r e E q u a l   ( " [ 7 0 1 4 ] " ,   f i e l d s [ 2 ] . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . F i e l d . C a p t i o n I d ) . T o S t r i n g   ( ) ) ; 	 / / 	 l o c a l l y   d e f i n e d ,   " E x t e n s i o n 1 "  
  
 	 	 	 A s s e r t . A r e E q u a l   ( F i e l d S o u r c e . E x p r e s s i o n ,   ( F i e l d S o u r c e )   f i e l d s [ 0 ] . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . F i e l d . S o u r c e ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( F i e l d S o u r c e . E x p r e s s i o n ,   ( F i e l d S o u r c e )   f i e l d s [ 1 ] . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . F i e l d . S o u r c e ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( F i e l d S o u r c e . E x p r e s s i o n ,   ( F i e l d S o u r c e )   f i e l d s [ 2 ] . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . F i e l d . S o u r c e ) ) ;  
  
 	 	 	 / / A s s e r t . A r e E q u a l   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . T y p e s . T e s t I n t e r f a c e . C a p t i o n I d ,   ( D r u i d )   f i e l d s [ 0 ] . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . F i e l d . D e e p D e f i n i n g T y p e I d ) ) ;  
 	 	 	 / / A s s e r t . A r e E q u a l   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . T y p e s . T e s t I n t e r f a c e . C a p t i o n I d ,   ( D r u i d )   f i e l d s [ 1 ] . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . F i e l d . D e e p D e f i n i n g T y p e I d ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( D r u i d . E m p t y ,                                               ( D r u i d )   f i e l d s [ 2 ] . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . F i e l d . D e e p D e f i n i n g T y p e I d ) ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( F i e l d M e m b e r s h i p . L o c a l ,   ( F i e l d M e m b e r s h i p )   f i e l d s [ 0 ] . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . F i e l d . M e m b e r s h i p ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( F i e l d M e m b e r s h i p . L o c a l ,   ( F i e l d M e m b e r s h i p )   f i e l d s [ 1 ] . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . F i e l d . M e m b e r s h i p ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( F i e l d M e m b e r s h i p . L o c a l ,   ( F i e l d M e m b e r s h i p )   f i e l d s [ 2 ] . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . F i e l d . M e m b e r s h i p ) ) ;  
  
 	 	 	 / / A s s e r t . A r e E q u a l   ( F i e l d M e m b e r s h i p . L o c a l O v e r r i d e ,   E p s i t e c . C o m m o n . S u p p o r t . R e s . T y p e s . T e s t I n t e r f a c e U s e r . F i e l d s [ " [ 7 0 0 J 2 ] " ] . M e m b e r s h i p ) ;  
 	 	 	 / / A s s e r t . A r e E q u a l   ( F i e l d M e m b e r s h i p . L o c a l ,                   E p s i t e c . C o m m o n . S u p p o r t . R e s . T y p e s . T e s t I n t e r f a c e U s e r . F i e l d s [ " [ 7 0 1 2 ] " ] . M e m b e r s h i p ) ;  
 	 	 	 / / A s s e r t . A r e E q u a l   ( F i e l d M e m b e r s h i p . L o c a l ,                   E p s i t e c . C o m m o n . S u p p o r t . R e s . T y p e s . T e s t I n t e r f a c e U s e r . F i e l d s [ " [ 7 0 1 4 ] " ] . M e m b e r s h i p ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( " »/ c # \ r \ n x   = >   s t r i n g . E m p t y " ,             ( s t r i n g )   f i e l d s [ 0 ] . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . F i e l d . E x p r e s s i o n ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " »/ c # \ r \ n x   = >   x . N a m e . T o U p p e r   ( ) " ,   ( s t r i n g )   f i e l d s [ 2 ] . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . F i e l d . E x p r e s s i o n ) ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( f a l s e ,                                       f i e l d s [ 0 ] . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . F i e l d . I s I n t e r f a c e D e f i n i t i o n ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( t r u e ,                                         f i e l d s [ 1 ] . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . F i e l d . I s I n t e r f a c e D e f i n i t i o n ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( U n d e f i n e d V a l u e . V a l u e ,   f i e l d s [ 2 ] . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . F i e l d . I s I n t e r f a c e D e f i n i t i o n ) ) ;  
 	 	 }  
  
 	 	 [ T e s t ]  
 	 	 p u b l i c   v o i d   C h e c k A n y T y p e A c c e s s o r ( )  
 	 	 {  
 	 	 	 E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e A c c e s s o r s . A n y T y p e R e s o u r c e A c c e s s o r   a c c e s s o r   =   n e w   E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e A c c e s s o r s . A n y T y p e R e s o u r c e A c c e s s o r   ( ) ;  
  
 	 	 	 A s s e r t . I s F a l s e   ( a c c e s s o r . C o n t a i n s C h a n g e s ) ;  
  
 	 	 	 a c c e s s o r . L o a d   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . M a n a g e r ) ;  
  
  
 	 	 	 S y s t e m . C o n s o l e . O u t . W r i t e L i n e   ( " { 0 }   r e s o u r c e s   f o u n d " ,   a c c e s s o r . C o l l e c t i o n . C o u n t ) ;  
  
 	 	 	 f o r e a c h   ( C u l t u r e M a p   i t e m   i n   a c c e s s o r . C o l l e c t i o n )  
 	 	 	 {  
 	 	 	 	 S t r u c t u r e d D a t a   d a t a   =   i t e m . G e t C u l t u r e D a t a   ( E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e s . D e f a u l t T w o L e t t e r I S O L a n g u a g e N a m e ) ;  
 	 	 	 	 s t r i n g [ ]   i d s   =   C o l l e c t i o n . T o A r r a y   ( d a t a . G e t V a l u e I d s   ( ) ) ;  
 	 	 	 	 T y p e C o d e   c o d e   =   ( T y p e C o d e )   d a t a . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e B a s e T y p e . T y p e C o d e ) ;  
  
 	 	 	 	 S y s t e m . C o n s o l e . O u t . W r i t e L i n e   ( "     { 2 } ,   { 0 }   - - >   { 1 } " ,   i t e m . N a m e ,   i t e m . I d ,   c o d e ) ;  
  
 	 	 	 	 f o r e a c h   ( s t r i n g   i d   i n   i d s )  
 	 	 	 	 {  
 	 	 	 	 	 S y s t e m . C o n s o l e . O u t . W r i t e L i n e   ( "         { 0 } :   { 1 } " ,   i d ,   d a t a . G e t V a l u e   ( i d ) ) ;  
 	 	 	 	 }  
 	 	 	 }  
  
 	 	 	 / / 	 C h e c k   I n t e g e r  
 	 	 	  
 	 	 	 C u l t u r e M a p   n e w I t e m   =   a c c e s s o r . C r e a t e I t e m   ( ) ;  
 	 	 	 S t r u c t u r e d D a t a   n e w D a t a   =   n e w I t e m . G e t C u l t u r e D a t a   ( E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e s . D e f a u l t T w o L e t t e r I S O L a n g u a g e N a m e ) ;  
 	 	 	 n e w I t e m . N a m e   =   " A n y T y p e A c c e s s o r I n t e g e r 1 " ;  
  
 	 	 	 n e w D a t a . S e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e B a s e T y p e . T y p e C o d e ,   T y p e C o d e . I n t e g e r ) ;  
 	 	 	 n e w D a t a . S e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e N u m e r i c T y p e . S m a l l S t e p ,   1 M ) ;  
 	 	 	 n e w D a t a . S e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e N u m e r i c T y p e . L a r g e S t e p ,   1 0 M ) ;  
 	 	 	 n e w D a t a . S e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e N u m e r i c T y p e . R a n g e ,   n e w   D e c i m a l R a n g e   ( 0 ,   9 9 9 ,   1 ) ) ;  
  
 	 	 	 a c c e s s o r . C o l l e c t i o n . A d d   ( n e w I t e m ) ;  
 	 	 	 a c c e s s o r . P e r s i s t C h a n g e s   ( ) ;  
  
 	 	 	 C a p t i o n   c a p t i o n   =   a c c e s s o r . R e s o u r c e M a n a g e r . G e t C a p t i o n   ( n e w I t e m . I d ,   R e s o u r c e L e v e l . D e f a u l t ) ;  
 	 	 	 I n t e g e r T y p e   i n t T y p e   =   T y p e R o s e t t a . C r e a t e T y p e O b j e c t   ( c a p t i o n ,   f a l s e )   a s   I n t e g e r T y p e ;  
  
 	 	 	 A s s e r t . I s N o t N u l l   ( i n t T y p e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( 1 M ,   i n t T y p e . S m a l l S t e p ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( 1 0 M ,   i n t T y p e . L a r g e S t e p ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( 9 9 9 M ,   i n t T y p e . R a n g e . M a x i m u m ) ;  
 	 	 	  
 	 	 	 / / 	 C h e c k   D a t e T i m e  
 	 	 	  
 	 	 	 n e w I t e m   =   a c c e s s o r . C r e a t e I t e m   ( ) ;  
 	 	 	 n e w D a t a   =   n e w I t e m . G e t C u l t u r e D a t a   ( E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e s . D e f a u l t T w o L e t t e r I S O L a n g u a g e N a m e ) ;  
 	 	 	 n e w I t e m . N a m e   =   " A n y T y p e A c c e s s o r D a t e T i m e 1 " ;  
  
 	 	 	 n e w D a t a . S e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e B a s e T y p e . T y p e C o d e ,   T y p e C o d e . D a t e T i m e ) ;  
 	 	 	 n e w D a t a . S e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e D a t e T i m e T y p e . R e s o l u t i o n ,   T i m e R e s o l u t i o n . M i n u t e s ) ;  
 	 	 	 n e w D a t a . S e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e D a t e T i m e T y p e . M i n i m u m D a t e ,   n e w   D a t e   ( 2 0 0 0 ,   0 6 ,   1 0 ) ) ;  
 	 	 	 n e w D a t a . S e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e D a t e T i m e T y p e . T i m e S t e p ,   n e w   S y s t e m . T i m e S p a n   ( 0 ,   1 5 ,   0 ) ) ;  
 	 	 	  
 	 	 	 a c c e s s o r . C o l l e c t i o n . A d d   ( n e w I t e m ) ;  
 	 	 	 a c c e s s o r . P e r s i s t C h a n g e s   ( ) ;  
  
 	 	 	 c a p t i o n   =   a c c e s s o r . R e s o u r c e M a n a g e r . G e t C a p t i o n   ( n e w I t e m . I d ,   R e s o u r c e L e v e l . D e f a u l t ) ;  
 	 	 	 D a t e T i m e T y p e   d t T y p e   =   T y p e R o s e t t a . C r e a t e T y p e O b j e c t   ( c a p t i o n ,   f a l s e )   a s   D a t e T i m e T y p e ;  
  
 	 	 	 A s s e r t . I s N o t N u l l   ( d t T y p e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( T i m e R e s o l u t i o n . M i n u t e s ,   d t T y p e . R e s o l u t i o n ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( 2 0 0 0 ,   d t T y p e . M i n i m u m D a t e . Y e a r ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( 6 ,   d t T y p e . M i n i m u m D a t e . M o n t h ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( 1 0 ,   d t T y p e . M i n i m u m D a t e . D a y ) ;  
 	 	 	 A s s e r t . I s T r u e   ( d t T y p e . M a x i m u m D a t e . I s N u l l ) ;  
 	 	 	 A s s e r t . I s T r u e   ( d t T y p e . M i n i m u m T i m e . I s N u l l ) ;  
 	 	 	 A s s e r t . I s T r u e   ( d t T y p e . M a x i m u m T i m e . I s N u l l ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( 1 5 ,   d t T y p e . T i m e S t e p . T o t a l M i n u t e s ) ;  
  
 	 	 	 / / 	 C h e c k   O t h e r  
 	 	 	  
 	 	 	 n e w I t e m   =   a c c e s s o r . C r e a t e I t e m   ( ) ;  
 	 	 	 n e w D a t a   =   n e w I t e m . G e t C u l t u r e D a t a   ( E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e s . D e f a u l t T w o L e t t e r I S O L a n g u a g e N a m e ) ;  
 	 	 	 n e w I t e m . N a m e   =   " A n y T y p e A c c e s s o r O t h e r 1 " ;  
  
 	 	 	 n e w D a t a . S e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e B a s e T y p e . T y p e C o d e ,   T y p e C o d e . O t h e r ) ;  
 	 	 	 n e w D a t a . S e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e O t h e r T y p e . S y s t e m T y p e ,   t y p e o f   ( c h a r ) ) ;  
  
 	 	 	 a c c e s s o r . C o l l e c t i o n . A d d   ( n e w I t e m ) ;  
 	 	 	 a c c e s s o r . P e r s i s t C h a n g e s   ( ) ;  
  
 	 	 	 c a p t i o n   =   a c c e s s o r . R e s o u r c e M a n a g e r . G e t C a p t i o n   ( n e w I t e m . I d ,   R e s o u r c e L e v e l . D e f a u l t ) ;  
 	 	 	 O t h e r T y p e   o t h e r T y p e   =   T y p e R o s e t t a . C r e a t e T y p e O b j e c t   ( c a p t i o n ,   f a l s e )   a s   O t h e r T y p e ;  
  
 	 	 	 A s s e r t . I s N o t N u l l   ( o t h e r T y p e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( t y p e o f   ( c h a r ) . N a m e ,   o t h e r T y p e . S y s t e m T y p e . N a m e ) ;  
  
 	 	 	 / / 	 C h e c k   S t r i n g  
  
 	 	 	 n e w I t e m   =   a c c e s s o r . C r e a t e I t e m   ( ) ;  
 	 	 	 n e w D a t a   =   n e w I t e m . G e t C u l t u r e D a t a   ( E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e s . D e f a u l t T w o L e t t e r I S O L a n g u a g e N a m e ) ;  
 	 	 	 n e w I t e m . N a m e   =   " A n y T y p e A c c e s s o r S t r i n g 1 " ;  
  
 	 	 	 n e w D a t a . S e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e B a s e T y p e . T y p e C o d e ,   T y p e C o d e . S t r i n g ) ;  
 	 	 	 n e w D a t a . S e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e S t r i n g T y p e . U s e M u l t i l i n g u a l S t o r a g e ,   t r u e ) ;  
 	 	 	 n e w D a t a . S e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e S t r i n g T y p e . M i n i m u m L e n g t h ,   1 ) ;  
  
 	 	 	 a c c e s s o r . C o l l e c t i o n . A d d   ( n e w I t e m ) ;  
 	 	 	 a c c e s s o r . P e r s i s t C h a n g e s   ( ) ;  
  
 	 	 	 c a p t i o n   =   a c c e s s o r . R e s o u r c e M a n a g e r . G e t C a p t i o n   ( n e w I t e m . I d ,   R e s o u r c e L e v e l . D e f a u l t ) ;  
 	 	 	 S t r i n g T y p e   s t r i n g T y p e   =   T y p e R o s e t t a . C r e a t e T y p e O b j e c t   ( c a p t i o n ,   f a l s e )   a s   S t r i n g T y p e ;  
  
 	 	 	 A s s e r t . I s N o t N u l l   ( s t r i n g T y p e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( t r u e ,   s t r i n g T y p e . U s e M u l t i l i n g u a l S t o r a g e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( 1 ,   s t r i n g T y p e . M i n i m u m L e n g t h ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( 1 0 0 0 0 0 0 ,   s t r i n g T y p e . M a x i m u m L e n g t h ) ;  
  
 	 	 	 / / 	 C h e c k   C o l l e c t i o n  
  
 	 	 	 n e w I t e m   =   a c c e s s o r . C r e a t e I t e m   ( ) ;  
 	 	 	 n e w D a t a   =   n e w I t e m . G e t C u l t u r e D a t a   ( E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e s . D e f a u l t T w o L e t t e r I S O L a n g u a g e N a m e ) ;  
 	 	 	 n e w I t e m . N a m e   =   " A n y T y p e A c c e s s o r C o l l e c t i o n 1 " ;  
  
 	 	 	 n e w D a t a . S e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e B a s e T y p e . T y p e C o d e ,   T y p e C o d e . C o l l e c t i o n ) ;  
 	 	 	 n e w D a t a . S e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e C o l l e c t i o n T y p e . I t e m T y p e ,   I n t e g e r T y p e . D e f a u l t . C a p t i o n I d ) ;  
  
 	 	 	 a c c e s s o r . C o l l e c t i o n . A d d   ( n e w I t e m ) ;  
 	 	 	 a c c e s s o r . P e r s i s t C h a n g e s   ( ) ;  
  
 	 	 	 c a p t i o n   =   a c c e s s o r . R e s o u r c e M a n a g e r . G e t C a p t i o n   ( n e w I t e m . I d ,   R e s o u r c e L e v e l . D e f a u l t ) ;  
 	 	 	 C o l l e c t i o n T y p e   c o l T y p e   =   T y p e R o s e t t a . C r e a t e T y p e O b j e c t   ( c a p t i o n ,   f a l s e )   a s   C o l l e c t i o n T y p e ;  
  
 	 	 	 A s s e r t . I s N o t N u l l   ( c o l T y p e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( I n t e g e r T y p e . D e f a u l t . C a p t i o n I d ,   c o l T y p e . I t e m T y p e . C a p t i o n I d ) ;  
  
 	 	 	 / / 	 C h e c k   E n u m e r a t i o n  
  
 	 	 	 C u l t u r e M a p   m a p   =   a c c e s s o r . C o l l e c t i o n [ E p s i t e c . C o m m o n . T y p e s . R e s . T y p e s . B i n d i n g M o d e . C a p t i o n I d ] ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( " B i n d i n g M o d e " ,   m a p . N a m e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " T y p . B i n d i n g M o d e " ,   a c c e s s o r . R e s o u r c e M a n a g e r . G e t B u n d l e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e s . C a p t i o n s B u n d l e N a m e ,   R e s o u r c e L e v e l . D e f a u l t ) [ E p s i t e c . C o m m o n . T y p e s . R e s . T y p e s . B i n d i n g M o d e . C a p t i o n I d ] . N a m e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " V a l . B i n d i n g M o d e . N o n e " ,   a c c e s s o r . R e s o u r c e M a n a g e r . G e t B u n d l e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e s . C a p t i o n s B u n d l e N a m e ,   R e s o u r c e L e v e l . D e f a u l t ) [ E p s i t e c . C o m m o n . T y p e s . R e s . V a l u e s . B i n d i n g M o d e . N o n e . I d ] . N a m e ) ;  
  
 	 	 	 A s s e r t . I s F a l s e   ( a c c e s s o r . C r e a t e M i s s i n g V a l u e I t e m s   ( m a p ) ) ;  
  
 	 	 	 S t r u c t u r e d D a t a   e n u m D a t a   =   m a p . G e t C u l t u r e D a t a   ( E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e s . D e f a u l t T w o L e t t e r I S O L a n g u a g e N a m e ) ;  
 	 	 	 I L i s t < S t r u c t u r e d D a t a >   e n u m V a l u e s   =   e n u m D a t a . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e E n u m T y p e . V a l u e s )   a s   I L i s t < S t r u c t u r e d D a t a > ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( 5 ,   e n u m V a l u e s . C o u n t ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( t y p e o f   ( B i n d i n g M o d e ) ,   e n u m D a t a . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e E n u m T y p e . S y s t e m T y p e )   a s   S y s t e m . T y p e ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( E p s i t e c . C o m m o n . T y p e s . R e s . V a l u e s . B i n d i n g M o d e . N o n e . I d ,   e n u m V a l u e s [ 0 ] . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . E n u m V a l u e . C a p t i o n I d ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( E p s i t e c . C o m m o n . T y p e s . R e s . V a l u e s . B i n d i n g M o d e . T w o W a y . I d ,   e n u m V a l u e s [ 4 ] . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . E n u m V a l u e . C a p t i o n I d ) ) ;  
  
 	 	 	 m a p . N a m e   =   " F o o " ;  
 	 	 	 a c c e s s o r . P e r s i s t C h a n g e s   ( ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( " F o o " ,   m a p . N a m e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " T y p . F o o " ,   a c c e s s o r . R e s o u r c e M a n a g e r . G e t B u n d l e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e s . C a p t i o n s B u n d l e N a m e ,   R e s o u r c e L e v e l . D e f a u l t ) [ E p s i t e c . C o m m o n . T y p e s . R e s . T y p e s . B i n d i n g M o d e . C a p t i o n I d ] . N a m e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " V a l . F o o . N o n e " ,   a c c e s s o r . R e s o u r c e M a n a g e r . G e t B u n d l e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e s . C a p t i o n s B u n d l e N a m e ,   R e s o u r c e L e v e l . D e f a u l t ) [ E p s i t e c . C o m m o n . T y p e s . R e s . V a l u e s . B i n d i n g M o d e . N o n e . I d ] . N a m e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " F o o . N o n e " ,   a c c e s s o r . V a l u e A c c e s s o r . C o l l e c t i o n [ E p s i t e c . C o m m o n . T y p e s . R e s . V a l u e s . B i n d i n g M o d e . N o n e . I d ] . T o S t r i n g   ( ) ) ;  
  
 	 	 	 m a p . N a m e   =   " B i n d i n g M o d e " ;  
 	 	 	 a c c e s s o r . P e r s i s t C h a n g e s   ( ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( " B i n d i n g M o d e " ,   m a p . N a m e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " T y p . B i n d i n g M o d e " ,   a c c e s s o r . R e s o u r c e M a n a g e r . G e t B u n d l e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e s . C a p t i o n s B u n d l e N a m e ,   R e s o u r c e L e v e l . D e f a u l t ) [ E p s i t e c . C o m m o n . T y p e s . R e s . T y p e s . B i n d i n g M o d e . C a p t i o n I d ] . N a m e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " V a l . B i n d i n g M o d e . N o n e " ,   a c c e s s o r . R e s o u r c e M a n a g e r . G e t B u n d l e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e s . C a p t i o n s B u n d l e N a m e ,   R e s o u r c e L e v e l . D e f a u l t ) [ E p s i t e c . C o m m o n . T y p e s . R e s . V a l u e s . B i n d i n g M o d e . N o n e . I d ] . N a m e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " B i n d i n g M o d e . N o n e " ,   a c c e s s o r . V a l u e A c c e s s o r . C o l l e c t i o n [ E p s i t e c . C o m m o n . T y p e s . R e s . V a l u e s . B i n d i n g M o d e . N o n e . I d ] . T o S t r i n g   ( ) ) ;  
  
 	 	 	 m a p   =   a c c e s s o r . C r e a t e I t e m   ( ) ;  
 	 	 	 m a p . N a m e   =   " T e s t . M y T e s t E n u m " ;  
 	 	 	 e n u m D a t a   =   m a p . G e t C u l t u r e D a t a   ( E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e s . D e f a u l t T w o L e t t e r I S O L a n g u a g e N a m e ) ;  
 	 	 	 e n u m D a t a . S e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e B a s e T y p e . T y p e C o d e ,   T y p e C o d e . E n u m ) ;  
 	 	 	 e n u m D a t a . S e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e E n u m T y p e . S y s t e m T y p e ,   t y p e o f   ( M y T e s t E n u m ) ) ;  
  
 	 	 	 a c c e s s o r . C o l l e c t i o n . A d d   ( m a p ) ;  
  
 	 	 	 i n t   c o u n t   =   a c c e s s o r . V a l u e A c c e s s o r . C o l l e c t i o n . C o u n t ;  
  
 	 	 	 A s s e r t . I s T r u e   ( a c c e s s o r . C r e a t e M i s s i n g V a l u e I t e m s   ( m a p ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( c o u n t + 3 ,   a c c e s s o r . V a l u e A c c e s s o r . C o l l e c t i o n . C o u n t ) ;  
 	 	 	  
 	 	 	 a c c e s s o r . P e r s i s t C h a n g e s   ( ) ;  
 	 	 }  
  
 	 	 [ T e s t ]  
 	 	 p u b l i c   v o i d   C h e c k P a n e l A c c e s s o r ( )  
 	 	 {  
 	 	 	 E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e A c c e s s o r s . P a n e l R e s o u r c e A c c e s s o r   a c c e s s o r   =   n e w   E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e A c c e s s o r s . P a n e l R e s o u r c e A c c e s s o r   ( ) ;  
 	 	 	 R e s o u r c e M o d u l e I d   m o d u l e   =   n e w   R e s o u r c e M o d u l e I d   ( " C r e s u s . T e s t s " ,   @ " S : \ E p s i t e c . C r e s u s \ A p p . C r e s u s D o c u m e n t s \ R e s o u r c e s \ C r e s u s . T e s t s " ,   5 0 0 ,   R e s o u r c e M o d u l e L a y e r . S y s t e m ) ;  
 	 	 	 R e s o u r c e M a n a g e r   m a n a g e r   =   n e w   R e s o u r c e M a n a g e r   ( n e w   R e s o u r c e M a n a g e r P o o l   ( ) ,   m o d u l e ) ;  
 	 	 	 m a n a g e r . D e f i n e D e f a u l t M o d u l e N a m e   ( " C r e s u s . T e s t s " ) ;  
  
  
 	 	 	 a c c e s s o r . L o a d   ( m a n a g e r ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( 8 ,   a c c e s s o r . C o l l e c t i o n . C o u n t ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " T e s t A v e c H e r i t a g e " ,   a c c e s s o r . C o l l e c t i o n [ 5 ] . N a m e ) ;  
  
 	 	 	 f o r e a c h   ( C u l t u r e M a p   i t e m   i n   a c c e s s o r . C o l l e c t i o n )  
 	 	 	 {  
 	 	 	 	 S y s t e m . C o n s o l e . O u t . W r i t e L i n e   ( " { 0 } :   { 1 } " ,   i t e m . I d ,   i t e m . N a m e ) ;  
 	 	 	 }  
  
 	 	 	 S t r u c t u r e d D a t a   d a t a   =   a c c e s s o r . C o l l e c t i o n [ 5 ] . G e t C u l t u r e D a t a   ( E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e s . D e f a u l t T w o L e t t e r I S O L a n g u a g e N a m e ) ;  
 	 	 	 s t r i n g   x m l   =   d a t a . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e P a n e l . X m l S o u r c e )   a s   s t r i n g ;  
  
 	 	 	 A s s e r t . I s N o t N u l l   ( x m l ) ;  
  
 	 	 	 S y s t e m . C o n s o l e . O u t . W r i t e L i n e   ( x m l ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( " 2 0 0 ; 2 0 0 " ,   d a t a . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e P a n e l . D e f a u l t S i z e ) ) ;  
 	 	 	 A s s e r t . I s T r u e   ( x m l . S t a r t s W i t h   ( " < p a n e l " ) ) ;  
 	 	 	 A s s e r t . I s T r u e   ( x m l . E n d s W i t h   ( " < / p a n e l > " ) ) ;  
  
 	 	 	 C u l t u r e M a p   i t e m 1   =   a c c e s s o r . C o l l e c t i o n [ 1 ] ;  
 	 	 	 C u l t u r e M a p   i t e m 2   =   a c c e s s o r . C o l l e c t i o n [ 2 ] ;  
 	 	 	 C u l t u r e M a p   n e w I t e m   =   a c c e s s o r . C r e a t e I t e m   ( ) ;  
  
 	 	 	 n e w I t e m . N a m e   =   " F o o B a r " ;  
  
 	 	 	 R e s o u r c e B u n d l e B a t c h S a v e r   s a v e r   =   n e w   R e s o u r c e B u n d l e B a t c h S a v e r   ( ) ;  
  
 	 	 	 a c c e s s o r . C o l l e c t i o n . R e m o v e A t   ( 2 ) ;  
 	 	 	 a c c e s s o r . C o l l e c t i o n . R e m o v e A t   ( 1 ) ;  
 	 	 	 a c c e s s o r . C o l l e c t i o n . I n s e r t   ( 1 ,   i t e m 1 ) ;  
 	 	 	 a c c e s s o r . C o l l e c t i o n . I n s e r t   ( 1 ,   i t e m 2 ) ;  
 	 	 	 a c c e s s o r . C o l l e c t i o n . A d d   ( n e w I t e m ) ;  
  
 	 	 	 A s s e r t . I s T r u e   ( a c c e s s o r . C o n t a i n s C h a n g e s ) ;  
 	 	 	 a c c e s s o r . P e r s i s t C h a n g e s   ( ) ;  
 	 	 	 A s s e r t . I s F a l s e   ( a c c e s s o r . C o n t a i n s C h a n g e s ) ;  
  
 	 	 	 a c c e s s o r . S a v e   ( s a v e r . D e l a y S a v e ) ;  
 	 	 	 s a v e r . E x e c u t e   ( ) ;  
  
 	 	 	 a c c e s s o r . C o l l e c t i o n . R e m o v e A t   ( 2 ) ;  
 	 	 	 a c c e s s o r . C o l l e c t i o n . R e m o v e A t   ( 1 ) ;  
 	 	 	 a c c e s s o r . C o l l e c t i o n . I n s e r t   ( 1 ,   i t e m 1 ) ;  
 	 	 	 a c c e s s o r . C o l l e c t i o n . I n s e r t   ( 2 ,   i t e m 2 ) ;  
  
 	 	 	 a c c e s s o r . C o l l e c t i o n . R e m o v e   ( n e w I t e m ) ;  
 	 	 	 a c c e s s o r . P e r s i s t C h a n g e s   ( ) ;  
  
 	 	 	 a c c e s s o r . S a v e   ( s a v e r . D e l a y S a v e ) ;  
 	 	 	 s a v e r . E x e c u t e   ( ) ;  
 	 	 }  
  
  
 	 	 p u b l i c   e n u m   M y T e s t E n u m  
 	 	 {  
 	 	 	 N o n e ,  
  
 	 	 	 F o o ,  
 	 	 	 B a r ,  
 	 	 	  
 	 	 	 [ H i d d e n ]  
 	 	 	 O t h e r  
 	 	 }  
  
 	 	 [ T e s t ]  
 	 	 p u b l i c   v o i d   C h e c k M e t a d a t a ( )  
 	 	 {  
 	 	 	 E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e A c c e s s o r s . S t r i n g R e s o u r c e A c c e s s o r     s t r i n g A c c e s s o r     =   n e w   E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e A c c e s s o r s . S t r i n g R e s o u r c e A c c e s s o r   ( ) ;  
 	 	 	 E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e A c c e s s o r s . C a p t i o n R e s o u r c e A c c e s s o r   c a p t i o n A c c e s s o r   =   n e w   E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e A c c e s s o r s . C a p t i o n R e s o u r c e A c c e s s o r   ( ) ;  
 	 	 	 E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e A c c e s s o r s . C o m m a n d R e s o u r c e A c c e s s o r   c o m m a n d A c c e s s o r   =   n e w   E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e A c c e s s o r s . C o m m a n d R e s o u r c e A c c e s s o r   ( ) ;  
 	 	 	 E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e A c c e s s o r s . S t r u c t u r e d T y p e R e s o u r c e A c c e s s o r   s t r u c t A c c e s s o r   =   n e w   E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e A c c e s s o r s . S t r u c t u r e d T y p e R e s o u r c e A c c e s s o r   ( ) ;  
  
 	 	 	 s t r i n g A c c e s s o r . L o a d   ( t h i s . m a n a g e r ) ;  
 	 	 	 c a p t i o n A c c e s s o r . L o a d   ( t h i s . m a n a g e r ) ;  
 	 	 	 c o m m a n d A c c e s s o r . L o a d   ( t h i s . m a n a g e r ) ;  
 	 	 	 s t r u c t A c c e s s o r . L o a d   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . M a n a g e r ) ;  
  
 	 	 	 S y s t e m . C o n s o l e . O u t . W r i t e L i n e   ( " S t r i n g s : " ) ;  
 	 	 	 t h i s . D u m p C u l t u r e M a p   ( s t r i n g A c c e s s o r . C o l l e c t i o n [ 0 ] ) ;  
 	 	 	 S y s t e m . C o n s o l e . O u t . W r i t e L i n e   ( " C a p t i o n s : " ) ;  
 	 	 	 t h i s . D u m p C u l t u r e M a p   ( c a p t i o n A c c e s s o r . C o l l e c t i o n [ 0 ] ) ;  
 	 	 	 S y s t e m . C o n s o l e . O u t . W r i t e L i n e   ( " C o m m a n d s : " ) ;  
 	 	 	 t h i s . D u m p C u l t u r e M a p   ( c o m m a n d A c c e s s o r . C o l l e c t i o n [ 0 ] ) ;  
 	 	 	 S y s t e m . C o n s o l e . O u t . W r i t e L i n e   ( " S t r u c t u r e d   T y p e s : " ) ;  
 	 	 	 t h i s . D u m p C u l t u r e M a p   ( s t r u c t A c c e s s o r . C o l l e c t i o n [ 0 ] ) ;  
 	 	 }  
  
 	 	 [ T e s t ]  
 	 	 p u b l i c   v o i d   C h e c k S t r i n g A c c e s s o r ( )  
 	 	 {  
 	 	 	 E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e A c c e s s o r s . S t r i n g R e s o u r c e A c c e s s o r   a c c e s s o r   =   n e w   E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e A c c e s s o r s . S t r i n g R e s o u r c e A c c e s s o r   ( ) ;  
  
 	 	 	 A s s e r t . I s F a l s e   ( a c c e s s o r . C o n t a i n s C h a n g e s ) ;  
  
 	 	 	 a c c e s s o r . L o a d   ( t h i s . m a n a g e r ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( 9 ,   a c c e s s o r . C o l l e c t i o n . C o u n t ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( D r u i d . P a r s e   ( " [ 4 0 0 2 ] " ) ,   a c c e s s o r . C o l l e c t i o n [ D r u i d . P a r s e   ( " [ 4 0 0 2 ] " ) ] . I d ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " T e x t   A " ,   a c c e s s o r . C o l l e c t i o n [ D r u i d . P a r s e   ( " [ 4 0 0 2 ] " ) ] . G e t C u l t u r e D a t a   ( " 0 0 " ) . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e S t r i n g . T e x t ) ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( D r u i d . P a r s e   ( " [ 4 0 0 6 ] " ) ,   a c c e s s o r . C o l l e c t i o n [ D r u i d . P a r s e   ( " [ 4 0 0 6 ] " ) ] . I d ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " T e x t 1 " ,   a c c e s s o r . C o l l e c t i o n [ D r u i d . P a r s e   ( " [ 4 0 0 6 ] " ) ] . N a m e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " H e l l o ,   w o r l d " ,   a c c e s s o r . C o l l e c t i o n [ " T e x t 1 " ] . G e t C u l t u r e D a t a   ( " 0 0 " ) . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e S t r i n g . T e x t ) ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( D r u i d . P a r s e   ( " [ 4 0 0 8 ] " ) ,   a c c e s s o r . C o l l e c t i o n [ D r u i d . P a r s e   ( " [ 4 0 0 8 ] " ) ] . I d ) ;  
 	 	 	 A s s e r t . I s N u l l   ( a c c e s s o r . C o l l e c t i o n [ D r u i d . P a r s e   ( " [ 4 0 0 8 ] " ) ] . G e t C u l t u r e D a t a   ( " 0 0 " ) . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e S t r i n g . T e x t ) ) ;  
  
 	 	 	 S t r u c t u r e d D a t a   d a t a 1   =   a c c e s s o r . C o l l e c t i o n [ " T e x t 1 " ] . G e t C u l t u r e D a t a   ( " f r " ) ;  
 	 	 	 S t r u c t u r e d D a t a   d a t a 2   =   a c c e s s o r . C o l l e c t i o n [ " T e x t 1 " ] . G e t C u l t u r e D a t a   ( " f r " ) ;  
  
 	 	 	 A s s e r t . A r e S a m e   ( d a t a 1 ,   d a t a 2 ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " B o n j o u r " ,   d a t a 1 . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e S t r i n g . T e x t ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( 0 ,   d a t a 1 . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e B a s e . M o d i f i c a t i o n I d ) ) ;  
 	 	 	 A s s e r t . I s F a l s e   ( a c c e s s o r . C o n t a i n s C h a n g e s ) ;  
  
 	 	 	 d a t a 1   =   a c c e s s o r . C o l l e c t i o n [ " T e x t 1 " ] . G e t C u l t u r e D a t a   ( " d e " ) ;  
 	 	 	 d a t a 2   =   a c c e s s o r . C o l l e c t i o n [ " T e x t 1 " ] . G e t C u l t u r e D a t a   ( " d e " ) ;  
  
 	 	 	 A s s e r t . I s N o t N u l l   ( d a t a 1 ) ;  
 	 	 	 A s s e r t . A r e S a m e   ( d a t a 1 ,   d a t a 2 ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( U n d e f i n e d V a l u e . V a l u e ,   d a t a 1 . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e S t r i n g . T e x t ) ) ;  
 	 	 	 A s s e r t . I s F a l s e   ( a c c e s s o r . C o n t a i n s C h a n g e s ) ;  
 	 	 	 A s s e r t . I s T r u e   ( d a t a 1 . I s E m p t y ) ;  
  
 	 	 	 d a t a 1   =   a c c e s s o r . C o l l e c t i o n [ " T e x t 1 " ] . G e t C u l t u r e D a t a   ( " f r " ) ;  
 	 	 	 d a t a 1 . S e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e S t r i n g . T e x t ,   " B o n j o u r   t o u t   l e   m o n d e " ) ;  
 	 	 	 d a t a 2 . S e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e S t r i n g . T e x t ,   " H a l l o ,   W e l t " ) ;  
 	 	 	 d a t a 2 . S e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e B a s e . M o d i f i c a t i o n I d ,   1 ) ;  
  
 	 	 	 A s s e r t . I s T r u e   ( a c c e s s o r . C o n t a i n s C h a n g e s ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( 1 ,   a c c e s s o r . P e r s i s t C h a n g e s   ( ) ) ;  
 	 	 	 A s s e r t . I s F a l s e   ( a c c e s s o r . C o n t a i n s C h a n g e s ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( " B o n j o u r   t o u t   l e   m o n d e " ,   t h i s . m a n a g e r . G e t T e x t   ( D r u i d . P a r s e   ( " [ 4 0 0 6 ] " ) ,   R e s o u r c e L e v e l . L o c a l i z e d ,   E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e s . F i n d C u l t u r e I n f o   ( " f r " ) ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " H a l l o ,   W e l t " ,   t h i s . m a n a g e r . G e t T e x t   ( D r u i d . P a r s e   ( " [ 4 0 0 6 ] " ) ,   R e s o u r c e L e v e l . L o c a l i z e d ,   E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e s . F i n d C u l t u r e I n f o   ( " d e " ) ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( 1 ,   t h i s . m a n a g e r . G e t B u n d l e   ( " S t r i n g s " ,   R e s o u r c e L e v e l . L o c a l i z e d ,   E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e s . F i n d C u l t u r e I n f o   ( " d e " ) ) [ D r u i d . P a r s e   ( " [ 4 0 0 6 ] " ) ] . M o d i f i c a t i o n I d ) ;  
  
 	 	 	 C u l t u r e M a p   m a p   =   a c c e s s o r . C r e a t e I t e m   ( ) ;  
  
 	 	 	 A s s e r t . I s N o t N u l l   ( m a p ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( D r u i d . P a r s e   ( " [ 4 0 0 9 ] " ) ,   m a p . I d ) ;  
 	 	 	 A s s e r t . I s N u l l   ( a c c e s s o r . C o l l e c t i o n [ m a p . I d ] ) ;  
  
 	 	 	 a c c e s s o r . C o l l e c t i o n . A d d   ( m a p ) ;  
 	 	 	 A s s e r t . I s T r u e   ( a c c e s s o r . C o n t a i n s C h a n g e s ) ;  
  
 	 	 	 m a p . N a m e   =   " N e w I t e m " ;  
 	 	 	 m a p . G e t C u l t u r e D a t a   ( " 0 0 " ) . S e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e S t r i n g . T e x t ,   " N e w   v a l u e " ) ;  
 	 	 	 m a p . G e t C u l t u r e D a t a   ( " f r " ) . S e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e S t r i n g . T e x t ,   " N o u v e l l e   v a l e u r " ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( 1 ,   a c c e s s o r . P e r s i s t C h a n g e s   ( ) ) ;  
 	 	 	 A s s e r t . I s F a l s e   ( a c c e s s o r . C o n t a i n s C h a n g e s ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( " N e w   v a l u e " ,   t h i s . m a n a g e r . G e t T e x t   ( D r u i d . P a r s e   ( " [ 4 0 0 9 ] " ) ,   R e s o u r c e L e v e l . D e f a u l t ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " N o u v e l l e   v a l e u r " ,   t h i s . m a n a g e r . G e t T e x t   ( D r u i d . P a r s e   ( " [ 4 0 0 9 ] " ) ,   R e s o u r c e L e v e l . M e r g e d ,   E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e s . F i n d C u l t u r e I n f o   ( " f r " ) ) ) ;  
  
 	 	 	 m a p . G e t C u l t u r e D a t a   ( " f r " ) . S e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e S t r i n g . T e x t ,   U n d e f i n e d V a l u e . V a l u e ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( 1 ,   a c c e s s o r . P e r s i s t C h a n g e s   ( ) ) ;  
 	 	 	 A s s e r t . I s F a l s e   ( a c c e s s o r . C o n t a i n s C h a n g e s ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( " N e w   v a l u e " ,   t h i s . m a n a g e r . G e t T e x t   ( D r u i d . P a r s e   ( " [ 4 0 0 9 ] " ) ,   R e s o u r c e L e v e l . D e f a u l t ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " N e w   v a l u e " ,   t h i s . m a n a g e r . G e t T e x t   ( D r u i d . P a r s e   ( " [ 4 0 0 9 ] " ) ,   R e s o u r c e L e v e l . M e r g e d ,   E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e s . F i n d C u l t u r e I n f o   ( " f r " ) ) ) ;  
  
 	 	 	 a c c e s s o r . C o l l e c t i o n . R e m o v e   ( m a p ) ;  
 	 	 	 A s s e r t . I s T r u e   ( a c c e s s o r . C o n t a i n s C h a n g e s ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( 1 ,   a c c e s s o r . P e r s i s t C h a n g e s   ( ) ) ;  
 	 	 	 A s s e r t . I s F a l s e   ( a c c e s s o r . C o n t a i n s C h a n g e s ) ;  
  
 	 	 	 A s s e r t . I s N u l l   ( t h i s . m a n a g e r . G e t T e x t   ( D r u i d . P a r s e   ( " [ 4 0 0 9 ] " ) ,   R e s o u r c e L e v e l . D e f a u l t ) ) ;  
 	 	 	 A s s e r t . I s N u l l   ( t h i s . m a n a g e r . G e t T e x t   ( D r u i d . P a r s e   ( " [ 4 0 0 9 ] " ) ,   R e s o u r c e L e v e l . L o c a l i z e d ,   E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e s . F i n d C u l t u r e I n f o   ( " f r " ) ) ) ;  
 	 	 }  
  
 	 	 [ T e s t ]  
 	 	 p u b l i c   v o i d   C h e c k U I ( )  
 	 	 {  
 	 	 	 R e s o u r c e M a n a g e r   m a n a g e r   =   n e w   R e s o u r c e M a n a g e r   ( t y p e o f   ( R e s o u r c e A c c e s s o r T e s t ) ) ;  
 	 	 	 m a n a g e r . D e f i n e D e f a u l t M o d u l e N a m e   ( " C o m m o n . D o c u m e n t " ) ;  
  
 	 	 	 E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e A c c e s s o r s . S t r i n g R e s o u r c e A c c e s s o r   s t r i n g A c c e s s o r   =   n e w   E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e A c c e s s o r s . S t r i n g R e s o u r c e A c c e s s o r   ( ) ;  
 	 	 	 s t r i n g A c c e s s o r . L o a d   ( m a n a g e r ) ;  
  
 	 	 	 I R e s o u r c e A c c e s s o r   a c c e s s o r   =   s t r i n g A c c e s s o r ;  
  
 	 	 	 S t r u c t u r e d T y p e   c u l t u r e M a p T y p e   =   n e w   S t r u c t u r e d T y p e   ( ) ;  
 	 	 	 c u l t u r e M a p T y p e . F i e l d s . A d d   ( " N a m e " ,   S t r i n g T y p e . N a t i v e D e f a u l t ) ;  
  
 	 	 	 C o l l e c t i o n V i e w   c o l l e c t i o n V i e w   =   n e w   C o l l e c t i o n V i e w   ( a c c e s s o r . C o l l e c t i o n ) ;  
  
 	 	 	 F a c t o r y . S e t A c t i v e   ( " L o o k R o y a l e " ) ;  
 	 	 	 W i n d o w   w i n d o w   =   n e w   W i n d o w   ( ) ;  
 	 	 	 w i n d o w . T e x t   =   " C h e c k U I " ;  
 	 	 	 w i n d o w . C l i e n t S i z e   =   n e w   S i z e   ( 3 0 0 ,   5 0 0 ) ;  
  
 	 	 	 I t e m T a b l e   t a b l e   =   n e w   I t e m T a b l e   ( w i n d o w . R o o t ) ;  
 	 	 	 t a b l e . D o c k   =   D o c k S t y l e . F i l l ;  
 	 	 	  
 	 	 	 t a b l e . S o u r c e T y p e   =   c u l t u r e M a p T y p e ;  
 	 	 	 t a b l e . I t e m s   =   c o l l e c t i o n V i e w ;  
 	 	 	 t a b l e . C o l u m n s . A d d   ( n e w   E p s i t e c . C o m m o n . U I . I t e m T a b l e C o l u m n   ( " N a m e " ) ) ;  
 	 	 	 t a b l e . H o r i z o n t a l S c r o l l M o d e   =   E p s i t e c . C o m m o n . U I . I t e m T a b l e S c r o l l M o d e . N o n e ;  
 	 	 	 t a b l e . V e r t i c a l S c r o l l M o d e   =   E p s i t e c . C o m m o n . U I . I t e m T a b l e S c r o l l M o d e . I t e m B a s e d ;  
 	 	 	 t a b l e . H e a d e r V i s i b i l i t y   =   f a l s e ;  
 	 	 	 t a b l e . F r a m e V i s i b i l i t y   =   f a l s e ;  
 	 	 	 t a b l e . I t e m P a n e l . L a y o u t   =   E p s i t e c . C o m m o n . U I . I t e m P a n e l L a y o u t . V e r t i c a l L i s t ;  
 	 	 	 t a b l e . I t e m P a n e l . I t e m S e l e c t i o n M o d e   =   E p s i t e c . C o m m o n . U I . I t e m P a n e l S e l e c t i o n M o d e . E x a c t l y O n e ;  
 	 	 	 t a b l e . M a r g i n s   =   n e w   M a r g i n s   ( 4 ,   1 ,   4 ,   2 ) ;  
  
 	 	 	 t a b l e . S i z e C h a n g e d   + =   t h i s . H a n d l e T a b l e S i z e C h a n g e d ;  
  
 	 	 	 T e x t F i e l d M u l t i   f i e l d   =   n e w   E p s i t e c . C o m m o n . W i d g e t s . T e x t F i e l d M u l t i   ( w i n d o w . R o o t ) ;  
 	 	 	 f i e l d . D o c k   =   D o c k S t y l e . B o t t o m ;  
 	 	 	 f i e l d . P r e f e r r e d H e i g h t   =   6 0 ;  
 	 	 	 f i e l d . M a r g i n s   =   n e w   M a r g i n s   ( 4 ,   0 ,   2 ,   4 ) ;  
 	 	 	  
 	 	 	 H S p l i t t e r   s p l i t t e r   =   n e w   E p s i t e c . C o m m o n . W i d g e t s . H S p l i t t e r   ( w i n d o w . R o o t ) ;  
 	 	 	 s p l i t t e r . D o c k   =   D o c k S t y l e . B o t t o m ;  
 	 	 	 s p l i t t e r . P r e f e r r e d H e i g h t   =   8 ;  
  
 	 	 	 / / 	 C r i t è r e   d e   t r i   :   s e l o n   l e   n o m   ( o n   n ' a   p a s   v r a i m e n t   l e   c h o i x ,   v u   l a   d é f i n i t i o n  
 	 	 	 / / 	 d e   C u l t u r e M a p )  
 	 	 	  
 	 	 	 c o l l e c t i o n V i e w . S o r t D e s c r i p t i o n s . C l e a r   ( ) ;  
 	 	 	 c o l l e c t i o n V i e w . S o r t D e s c r i p t i o n s . A d d   ( n e w   E p s i t e c . C o m m o n . T y p e s . S o r t D e s c r i p t i o n   ( " N a m e " ) ) ;  
  
 	 	 	 / / 	 F i l t r e   u n i q u e m e n t   l e s   i t e m s   q u i   o n t   u n   " b "   d a n s   l e u r   n o m   :  
  
 	 	 	 c o l l e c t i o n V i e w . F i l t e r   + =  
 	 	 	 	 d e l e g a t e   ( o b j e c t   o b j )  
 	 	 	 	 {  
 	 	 	 	 	 C u l t u r e M a p   i t e m   =   o b j   a s   C u l t u r e M a p ;  
 	 	 	 	 	  
 	 	 	 	 	 i f   ( i t e m . N a m e . C o n t a i n s   ( " b " ) )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 r e t u r n   t r u e ;  
 	 	 	 	 	 }  
 	 	 	 	 	 e l s e  
 	 	 	 	 	 {  
 	 	 	 	 	 	 r e t u r n   f a l s e ;  
 	 	 	 	 	 }  
 	 	 	 	 } ;  
  
 	 	 	 t a b l e . I t e m P a n e l . S e l e c t i o n C h a n g e d   + =  
 	 	 	 	 d e l e g a t e  
 	 	 	 	 {  
 	 	 	 	 	 C u l t u r e M a p   i t e m   =   c o l l e c t i o n V i e w . C u r r e n t I t e m   a s   C u l t u r e M a p ;  
 	 	 	 	 	 S y s t e m . T e x t . S t r i n g B u i l d e r   b u f f e r   =   n e w   S y s t e m . T e x t . S t r i n g B u i l d e r   ( ) ;  
 	 	 	 	 	 s t r i n g [ ]   c u l t u r e s   =   n e w   s t r i n g [ ]   {   " 0 0 " ,   " f r " ,   " e n " ,   " d e "   } ;  
 	 	 	 	 	  
 	 	 	 	 	 f o r e a c h   ( s t r i n g   c u l t u r e   i n   c u l t u r e s )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 S t r u c t u r e d D a t a   d a t a   =   i t e m . G e t C u l t u r e D a t a   ( c u l t u r e ) ;  
 	 	 	 	 	 	 s t r i n g   t e x t   =   d a t a . G e t V a l u e   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . F i e l d s . R e s o u r c e S t r i n g . T e x t )   a s   s t r i n g ;  
 	 	 	 	 	 	 i f   ( t e x t   ! =   n u l l )  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 b u f f e r . A p p e n d   ( c u l t u r e ) ;  
 	 	 	 	 	 	 	 b u f f e r . A p p e n d   ( " :   " ) ;  
 	 	 	 	 	 	 	 b u f f e r . A p p e n d   ( T e x t L a y o u t . C o n v e r t T o T a g g e d T e x t   ( t e x t ) ) ;  
 	 	 	 	 	 	 	 b u f f e r . A p p e n d   ( " < b r / > " ) ;  
 	 	 	 	 	 	 }  
 	 	 	 	 	 }  
 	 	 	 	 	 f i e l d . T e x t   =   b u f f e r . T o S t r i n g   ( ) ;  
 	 	 	 	 } ;  
  
 	 	 	 w i n d o w . S h o w   ( ) ;  
 	 	 	 W i n d o w . R u n I n T e s t E n v i r o n m e n t   ( w i n d o w ) ;  
 	 	 }  
  
 	 	 v o i d   H a n d l e T a b l e S i z e C h a n g e d ( o b j e c t   s e n d e r ,   E p s i t e c . C o m m o n . T y p e s . D e p e n d e n c y P r o p e r t y C h a n g e d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 I t e m T a b l e   t a b l e   =   ( I t e m T a b l e )   s e n d e r ;  
 	 	 	 S i z e   s i z e   =   ( S i z e )   e . N e w V a l u e ;  
  
 	 	 	 d o u b l e   w i d t h   =   s i z e . W i d t h   -   t a b l e . G e t P a n e l P a d d i n g   ( ) . W i d t h ;  
 	 	 	 t a b l e . C o l u m n H e a d e r . S e t C o l u m n W i d t h   ( 0 ,   w i d t h ) ;  
  
 	 	 	 t a b l e . I t e m P a n e l . I t e m V i e w D e f a u l t S i z e   =   n e w   E p s i t e c . C o m m o n . D r a w i n g . S i z e   ( w i d t h ,   2 0 ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   D u m p C u l t u r e M a p ( C u l t u r e M a p   m a p )  
 	 	 {  
 	 	 	 f o r e a c h   ( s t r i n g   c u l t u r e   i n   m a p . G e t D e f i n e d C u l t u r e s   ( ) )  
 	 	 	 {  
 	 	 	 	 S t r u c t u r e d D a t a     d a t a   =   m a p . G e t C u l t u r e D a t a   ( c u l t u r e ) ;  
 	 	 	 	 I S t r u c t u r e d T y p e   t y p e   =   d a t a . S t r u c t u r e d T y p e ;  
  
 	 	 	 	 t h i s . D u m p S t r u c t u r e d D a t a   ( " " ,   d a t a ,   t y p e ) ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   D u m p S t r u c t u r e d D a t a ( s t r i n g   i n d e n t ,   S t r u c t u r e d D a t a   d a t a ,   I S t r u c t u r e d T y p e   t y p e )  
 	 	 {  
 	 	 	 i f   ( d a t a   = =   n u l l )  
 	 	 	 {  
 	 	 	 	 r e t u r n ;  
 	 	 	 }  
  
 	 	 	 f o r e a c h   ( s t r i n g   f i e l d I d   i n   t y p e . G e t F i e l d I d s   ( ) )  
 	 	 	 {  
 	 	 	 	 S t r u c t u r e d T y p e F i e l d   f i e l d   =   t y p e . G e t F i e l d   ( f i e l d I d ) ;  
 	 	 	 	 C a p t i o n   c a p t i o n   =   t h i s . m a n a g e r . G e t C a p t i o n   ( f i e l d . C a p t i o n I d ) ;  
  
 	 	 	 	 S y s t e m . C o n s o l e . O u t . W r i t e L i n e   ( " { 4 } { 0 }   ( { 1 } )   :   t y p e   =   { 2 } ,   d a t a   =   { 3 } ,   r e l a t i o n   =   { 5 } ,   { 6 } " ,   f i e l d I d ,   ( c a p t i o n   = =   n u l l )   ?   " < ? > "   :   c a p t i o n . N a m e ,   ( f i e l d . T y p e   = =   n u l l )   ?   " < n u l l > "   :   f i e l d . T y p e . N a m e ,   d a t a . G e t V a l u e   ( f i e l d I d ) ,   i n d e n t ,   f i e l d . R e l a t i o n ,   f i e l d . M e m b e r s h i p ) ;  
  
 	 	 	 	 i f   ( ( f i e l d . T y p e   i s   I S t r u c t u r e d T y p e )   & &  
 	 	 	 	 	 ( f i e l d . R e l a t i o n   ! =   F i e l d R e l a t i o n . C o l l e c t i o n ) )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . D u m p S t r u c t u r e d D a t a   ( "     "   +   i n d e n t ,   d a t a . G e t V a l u e   ( f i e l d I d )   a s   S t r u c t u r e d D a t a ,   f i e l d . T y p e   a s   I S t r u c t u r e d T y p e ) ;  
 	 	 	 	 }  
 	 	 	 	 e l s e   i f   ( f i e l d . T y p e   i s   C o l l e c t i o n T y p e )  
 	 	 	 	 {  
 	 	 	 	 	 C o l l e c t i o n T y p e   c o l l e c t i o n T y p e   =   f i e l d . T y p e   a s   C o l l e c t i o n T y p e ;  
  
 	 	 	 	 	 i f   ( c o l l e c t i o n T y p e . I t e m T y p e   i s   I S t r u c t u r e d T y p e )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 S y s t e m . C o l l e c t i o n s . I L i s t   c o l l e c t i o n   =   d a t a . G e t V a l u e   ( f i e l d I d )   a s   S y s t e m . C o l l e c t i o n s . I L i s t ;  
 	 	 	 	 	 	 S t r u c t u r e d D a t a   i t e m 0 ;  
 	 	 	 	 	 	  
 	 	 	 	 	 	 i f   ( c o l l e c t i o n . C o u n t   >   0 )  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 i t e m 0   =   c o l l e c t i o n [ 0 ]   a s   S t r u c t u r e d D a t a ;  
 	 	 	 	 	 	 }  
 	 	 	 	 	 	 e l s e  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 i t e m 0   =   n e w   S t r u c t u r e d D a t a   ( c o l l e c t i o n T y p e . I t e m T y p e   a s   I S t r u c t u r e d T y p e ) ;  
 	 	 	 	 	 	 }  
  
 	 	 	 	 	 	 t h i s . D u m p S t r u c t u r e d D a t a   ( " *   "   +   i n d e n t ,   i t e m 0 ,   c o l l e c t i o n T y p e . I t e m T y p e   a s   I S t r u c t u r e d T y p e ) ;  
 	 	 	 	 	 }  
 	 	 	 	 }  
 	 	 	 	 e l s e   i f   ( f i e l d . R e l a t i o n   = =   F i e l d R e l a t i o n . C o l l e c t i o n )  
 	 	 	 	 {  
 	 	 	 	 	 A b s t r a c t T y p e   c o l l e c t i o n T y p e   =   f i e l d . T y p e   a s   A b s t r a c t T y p e ;  
  
 	 	 	 	 	 i f   ( c o l l e c t i o n T y p e   i s   I S t r u c t u r e d T y p e )  
 	 	 	 	 	 {  
 	 	 	 	 	 	 S y s t e m . C o l l e c t i o n s . I L i s t   c o l l e c t i o n   =   d a t a . G e t V a l u e   ( f i e l d I d )   a s   S y s t e m . C o l l e c t i o n s . I L i s t ;  
 	 	 	 	 	 	 S t r u c t u r e d D a t a   i t e m 0 ;  
  
 	 	 	 	 	 	 i f   ( c o l l e c t i o n . C o u n t   >   0 )  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 i t e m 0   =   c o l l e c t i o n [ 0 ]   a s   S t r u c t u r e d D a t a ;  
 	 	 	 	 	 	 }  
 	 	 	 	 	 	 e l s e  
 	 	 	 	 	 	 {  
 	 	 	 	 	 	 	 i t e m 0   =   n e w   S t r u c t u r e d D a t a   ( c o l l e c t i o n T y p e   a s   I S t r u c t u r e d T y p e ) ;  
 	 	 	 	 	 	 }  
  
 	 	 	 	 	 	 t h i s . D u m p S t r u c t u r e d D a t a   ( " *   "   +   i n d e n t ,   i t e m 0 ,   c o l l e c t i o n T y p e   a s   I S t r u c t u r e d T y p e ) ;  
 	 	 	 	 	 }  
 	 	 	 	 }  
 	 	 	 }  
 	 	 }  
  
 	 	 R e s o u r c e M a n a g e r   m a n a g e r ;  
 	 }  
 }  
 