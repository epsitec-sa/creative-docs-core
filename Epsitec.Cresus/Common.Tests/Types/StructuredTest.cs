ÿþ/ / 	 C o p y r i g h t   ©   2 0 0 6 - 2 0 0 8 ,   E P S I T E C   S A ,   1 4 0 0   Y v e r d o n - l e s - B a i n s ,   S w i t z e r l a n d  
 / / 	 R e s p o n s a b l e :   P i e r r e   A R N A U D  
  
 u s i n g   N U n i t . F r a m e w o r k ;  
 u s i n g   S y s t e m . C o l l e c t i o n s . G e n e r i c ;  
 u s i n g   E p s i t e c . C o m m o n . T y p e s ;  
 u s i n g   E p s i t e c . C o m m o n . S u p p o r t ;  
  
 n a m e s p a c e   E p s i t e c . C o m m o n . T e s t s . T y p e s  
 {  
 	 [ T e s t F i x t u r e ]  
 	 p u b l i c   c l a s s   S t r u c t u r e d T e s t  
 	 {  
 	 	 [ S e t U p ]  
 	 	 p u b l i c   v o i d   S e t U p ( )  
 	 	 {  
 	 	 	 t h i s . b u f f e r   =   n e w   S y s t e m . T e x t . S t r i n g B u i l d e r   ( ) ;  
  
 	 	 	 t h i s . m a n a g e r   =   n e w   R e s o u r c e M a n a g e r   ( @ " S : \ E p s i t e c . C r e s u s \ C o m m o n . T y p e s . T e s t s " ) ;  
 	 	 	 t h i s . m a n a g e r . D e f i n e D e f a u l t M o d u l e N a m e   ( " T e s t " ) ;  
 	 	 	 t h i s . m a n a g e r . A c t i v e P r e f i x   =   " f i l e " ;  
 	 	 	 t h i s . m a n a g e r . A c t i v e C u l t u r e   =   E p s i t e c . C o m m o n . S u p p o r t . R e s o u r c e s . F i n d C u l t u r e I n f o   ( " e n " ) ;  
 	 	 }  
 	 	  
 	 	 [ T e s t ]  
 	 	 p u b l i c   v o i d   C h e c k S t r u c t u r e d D a t a ( )  
 	 	 {  
 	 	 	 S t r u c t u r e d D a t a   d a t a   =   n e w   S t r u c t u r e d D a t a   ( ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( 0 ,   C o l l e c t i o n . C o u n t   ( d a t a . G e t V a l u e I d s   ( ) ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( 0 ,   C o l l e c t i o n . C o u n t   ( d a t a . S t r u c t u r e d T y p e . G e t F i e l d I d s   ( ) ) ) ;  
 	 	 	  
 	 	 	 A s s e r t . I s T r u e   ( d a t a . I s E m p t y ) ;  
  
 	 	 	 d a t a . S e t V a l u e   ( " A " ,   1 0 ) ;  
 	 	 	 d a t a . S e t V a l u e   ( " B " ,   2 0 ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( 2 ,   C o l l e c t i o n . C o u n t   ( d a t a . G e t V a l u e I d s   ( ) ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( 2 ,   C o l l e c t i o n . C o u n t   ( d a t a . S t r u c t u r e d T y p e . G e t F i e l d I d s   ( ) ) ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( " A " ,   C o l l e c t i o n . E x t r a c t   ( d a t a . S t r u c t u r e d T y p e . G e t F i e l d I d s   ( ) ,   0 ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " B " ,   C o l l e c t i o n . E x t r a c t   ( d a t a . S t r u c t u r e d T y p e . G e t F i e l d I d s   ( ) ,   1 ) ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( t y p e o f   ( i n t ) ,   d a t a . S t r u c t u r e d T y p e . G e t F i e l d   ( " A " ) . T y p e . S y s t e m T y p e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( 1 0 ,   d a t a . G e t V a l u e   ( " A " ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( 2 0 ,   d a t a . G e t V a l u e   ( " B " ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( U n d e f i n e d V a l u e . V a l u e ,   d a t a . G e t V a l u e   ( " X " ) ) ;  
  
 	 	 	 d a t a . S e t V a l u e   ( " A " ,   U n d e f i n e d V a l u e . V a l u e ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( 1 ,   C o l l e c t i o n . C o u n t   ( d a t a . G e t V a l u e I d s   ( ) ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( 1 ,   C o l l e c t i o n . C o u n t   ( d a t a . S t r u c t u r e d T y p e . G e t F i e l d I d s   ( ) ) ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( " B " ,   C o l l e c t i o n . E x t r a c t   ( d a t a . S t r u c t u r e d T y p e . G e t F i e l d I d s   ( ) ,   0 ) ) ;  
  
 	 	 	 A s s e r t . I s F a l s e   ( d a t a . I s V a l u e L o c k e d   ( " A " ) ) ;  
 	 	 	 A s s e r t . I s F a l s e   ( d a t a . I s V a l u e L o c k e d   ( " B " ) ) ;  
  
 	 	 	 A s s e r t . I s F a l s e   ( d a t a . L o c k V a l u e   ( " A " ) ) ;  
 	 	 	 A s s e r t . I s T r u e   ( d a t a . L o c k V a l u e   ( " B " ) ) ;  
 	 	 	  
 	 	 	 A s s e r t . I s F a l s e   ( d a t a . I s V a l u e L o c k e d   ( " A " ) ) ;  
 	 	 	 A s s e r t . I s T r u e   ( d a t a . I s V a l u e L o c k e d   ( " B " ) ) ;  
  
 	 	 	 d a t a . S e t V a l u e   ( " A " ,   1 0 ) ;  
 	 	 	 d a t a . S e t V a l u e   ( " B " ,   2 0 ) ;  
  
 	 	 	 d a t a   =   n e w   S t r u c t u r e d D a t a   ( ) ;  
  
 	 	 	 d a t a . S e t V a l u e   ( " U " ,   U n d e f i n e d V a l u e . V a l u e ) ;  
 	 	 	 A s s e r t . I s T r u e   ( d a t a . I s E m p t y ) ;  
 	 	 	 d a t a . S e t V a l u e   ( " L " ,   n e w   L i s t < s t r i n g >   ( ) ) ;  
  
 	 	 	 A s s e r t . I s F a l s e   ( d a t a . I s E m p t y ) ;  
 	 	 	 d a t a . L o c k V a l u e   ( " L " ) ;  
 	 	 	 A s s e r t . I s T r u e   ( d a t a . I s E m p t y ) ;  
 	 	 	 ( ( L i s t < s t r i n g > )   d a t a . G e t V a l u e   ( " L " ) ) . A d d   ( " x " ) ;  
 	 	 	 A s s e r t . I s F a l s e   ( d a t a . I s E m p t y ) ;  
 	 	 	 ( ( L i s t < s t r i n g > )   d a t a . G e t V a l u e   ( " L " ) ) . C l e a r   ( ) ;  
 	 	 	 A s s e r t . I s T r u e   ( d a t a . I s E m p t y ) ;  
 	 	 }  
  
 	 	 [ T e s t ]  
 	 	 p u b l i c   v o i d   C h e c k S t r u c t u r e d D a t a E q u a l i t y ( )  
 	 	 {  
 	 	 	 S t r u c t u r e d D a t a   a   =   n e w   S t r u c t u r e d D a t a   ( ) ;  
 	 	 	 S t r u c t u r e d D a t a   b   =   n e w   S t r u c t u r e d D a t a   ( ) ;  
 	 	 	 S t r u c t u r e d D a t a   c   =   n u l l ;  
  
 	 	 	 A s s e r t . I s T r u e   ( a . E q u a l s   ( b ) ) ;  
 	 	 	 A s s e r t . I s F a l s e   ( a . E q u a l s   ( c ) ) ;  
  
 	 	 	 a . S e t V a l u e   ( " x " ,   1 2 3 ) ;  
 	 	 	 a . S e t V a l u e   ( " y " ,   4 5 6 ) ;  
  
 	 	 	 A s s e r t . I s F a l s e   ( a . E q u a l s   ( b ) ) ;  
  
 	 	 	 b . S e t V a l u e   ( " y " ,   4 5 6 ) ;  
 	 	 	 b . S e t V a l u e   ( " x " ,   1 2 3 ) ;  
 	 	 	  
 	 	 	 A s s e r t . I s T r u e   ( a . E q u a l s   ( b ) ) ;  
  
 	 	 	 S t r u c t u r e d D a t a   x 1   =   n e w   S t r u c t u r e d D a t a   ( ) ;  
 	 	 	 S t r u c t u r e d D a t a   x 2   =   n e w   S t r u c t u r e d D a t a   ( ) ;  
  
 	 	 	 x 1 . S e t V a l u e   ( " A " ,   1 ) ;  
 	 	 	 x 2 . S e t V a l u e   ( " A " ,   1 ) ;  
  
 	 	 	 a . S e t V a l u e   ( " l i s t " ,   n e w   L i s t < S t r u c t u r e d D a t a >   ( n e w   S t r u c t u r e d D a t a [ ]   {   x 1   } ) ) ;  
 	 	 	 b . S e t V a l u e   ( " l i s t " ,   n e w   L i s t < S t r u c t u r e d D a t a >   ( n e w   S t r u c t u r e d D a t a [ ]   {   x 2   } ) ) ;  
  
 	 	 	 A s s e r t . I s T r u e   ( a . E q u a l s   ( b ) ) ;  
  
 	 	 	 a . S e t V a l u e   ( " u " ,   U n d e f i n e d V a l u e . V a l u e ) ;  
  
 	 	 	 A s s e r t . I s T r u e   ( a . E q u a l s   ( b ) ) ;  
 	 	 }  
  
 	 	 [ T e s t ]  
 	 	 [ E x p e c t e d E x c e p t i o n   ( t y p e o f   ( S y s t e m . I n v a l i d O p e r a t i o n E x c e p t i o n ) ) ]  
 	 	 p u b l i c   v o i d   C h e c k S t r u r c t u r e D a t a E x 1 ( )  
 	 	 {  
 	 	 	 S t r u c t u r e d D a t a   d a t a   =   n e w   S t r u c t u r e d D a t a   ( ) ;  
  
 	 	 	 d a t a . S e t V a l u e   ( " A " ,   1 0 ) ;  
 	 	 	 d a t a . L o c k V a l u e   ( " A " ) ;  
 	 	 	 d a t a . S e t V a l u e   ( " A " ,   1 1 ) ;  
 	 	 }  
  
 	 	 [ T e s t ]  
 	 	 [ E x p e c t e d E x c e p t i o n   ( t y p e o f   ( S y s t e m . I n v a l i d O p e r a t i o n E x c e p t i o n ) ) ]  
 	 	 p u b l i c   v o i d   C h e c k S t r u r c t u r e D a t a E x 2 ( )  
 	 	 {  
 	 	 	 S t r u c t u r e d D a t a   d a t a   =   n e w   S t r u c t u r e d D a t a   ( ) ;  
  
 	 	 	 d a t a . S e t V a l u e   ( " A " ,   1 0 ) ;  
 	 	 	 d a t a . L o c k V a l u e   ( " A " ) ;  
 	 	 	 d a t a . S e t V a l u e   ( " A " ,   U n d e f i n e d V a l u e . V a l u e ) ;  
 	 	 }  
  
 	 	 [ T e s t ]  
 	 	 p u b l i c   v o i d   C h e c k S t r u c t u r e d D a t a W i t h T y p e ( )  
 	 	 {  
 	 	 	 S t r u c t u r e d T y p e   t y p e   =   n e w   S t r u c t u r e d T y p e   ( ) ;  
 	 	 	 S t r u c t u r e d D a t a   d a t a   =   n e w   S t r u c t u r e d D a t a   ( t y p e ) ;  
  
 	 	 	 t y p e . F i e l d s . A d d   ( " A " ,   n e w   I n t e g e r T y p e   ( 0 ,   1 0 0 ) ) ;  
 	 	 	 t y p e . F i e l d s . A d d   ( " B " ,   n e w   I n t e g e r T y p e   ( 0 ,   1 0 0 ) ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( 2 ,   C o l l e c t i o n . C o u n t   ( d a t a . G e t V a l u e I d s   ( ) ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( 2 ,   C o l l e c t i o n . C o u n t   ( d a t a . S t r u c t u r e d T y p e . G e t F i e l d I d s   ( ) ) ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( " A " ,   C o l l e c t i o n . E x t r a c t   ( d a t a . S t r u c t u r e d T y p e . G e t F i e l d I d s   ( ) ,   0 ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " B " ,   C o l l e c t i o n . E x t r a c t   ( d a t a . S t r u c t u r e d T y p e . G e t F i e l d I d s   ( ) ,   1 ) ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( U n d e f i n e d V a l u e . V a l u e ,   d a t a . G e t V a l u e   ( " A " ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( U n d e f i n e d V a l u e . V a l u e ,   d a t a . G e t V a l u e   ( " B " ) ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( - 1 ,   d a t a . I n t e r n a l G e t V a l u e C o u n t   ( ) ) ;  
  
 	 	 	 d a t a . S e t V a l u e   ( " A " ,   1 0 ) ;  
 	 	 	 d a t a . S e t V a l u e   ( " B " ,   2 0 ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( 2 ,   d a t a . I n t e r n a l G e t V a l u e C o u n t   ( ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( t y p e o f   ( I n t e g e r T y p e ) ,   d a t a . S t r u c t u r e d T y p e . G e t F i e l d   ( " A " ) . T y p e . G e t T y p e   ( ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( 1 0 ,   d a t a . G e t V a l u e   ( " A " ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( 2 0 ,   d a t a . G e t V a l u e   ( " B " ) ) ;  
  
 	 	 	 t h i s . b u f f e r . L e n g t h   =   0 ;  
  
 	 	 	 d a t a . A t t a c h L i s t e n e r   ( " A " ,   t h i s . H a n d l e D a t a P r o p e r t y C h a n g e d ) ;  
 	 	 	 d a t a . S e t V a l u e   ( " A " ,   1 5 ) ;  
 	 	 	 d a t a . S e t V a l u e   ( " A " ,   U n d e f i n e d V a l u e . V a l u e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( 2 ,   d a t a . I n t e r n a l G e t V a l u e C o u n t   ( ) ) ;  
 	 	 	 d a t a . D e t a c h L i s t e n e r   ( " A " ,   t h i s . H a n d l e D a t a P r o p e r t y C h a n g e d ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( 1 ,   d a t a . I n t e r n a l G e t V a l u e C o u n t   ( ) ) ;  
 	 	 	 d a t a . S e t V a l u e   ( " A " ,   1 0 ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( 2 ,   d a t a . I n t e r n a l G e t V a l u e C o u n t   ( ) ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( " [ A : 1 0 - > 1 5 ] [ A : 1 5 - > < U n d e f i n e d V a l u e > ] " ,   t h i s . b u f f e r . T o S t r i n g   ( ) ) ;  
  
 	 	 	 d a t a . S e t V a l u e   ( " A " ,   U n d e f i n e d V a l u e . V a l u e ) ;  
 	 	 	 d a t a . S e t V a l u e   ( " B " ,   U n d e f i n e d V a l u e . V a l u e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( 0 ,   d a t a . I n t e r n a l G e t V a l u e C o u n t   ( ) ) ;  
  
  
 	 	 	 b o o l   o r i g i n a l ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( U n d e f i n e d V a l u e . V a l u e ,   d a t a . G e t V a l u e   ( " A " ,   o u t   o r i g i n a l ) ) ;  
 	 	 	 A s s e r t . I s T r u e   ( o r i g i n a l ) ;  
  
 	 	 	 d a t a . R e s e t T o O r i g i n a l V a l u e   ( " A " ) ;  
 	 	 	  
 	 	 	 A s s e r t . A r e E q u a l   ( U n d e f i n e d V a l u e . V a l u e ,   d a t a . G e t V a l u e   ( " A " ,   o u t   o r i g i n a l ) ) ;  
 	 	 	 A s s e r t . I s T r u e   ( o r i g i n a l ) ;  
 	 	 	  
 	 	 	 d a t a . S e t V a l u e   ( " A " ,   1 0 ) ;  
 	 	 	 d a t a . S e t V a l u e   ( " B " ,   2 0 ) ;  
 	 	 	  
 	 	 	 d a t a . P r o m o t e T o O r i g i n a l V a l u e   ( " A " ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( 1 0 ,   d a t a . G e t V a l u e   ( " A " ,   o u t   o r i g i n a l ) ) ;  
 	 	 	 A s s e r t . I s T r u e   ( o r i g i n a l ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( 2 0 ,   d a t a . G e t V a l u e   ( " B " ,   o u t   o r i g i n a l ) ) ;  
 	 	 	 A s s e r t . I s F a l s e   ( o r i g i n a l ) ;  
 	 	 	  
 	 	 	 d a t a . S e t V a l u e   ( " A " ,   1 5 ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( 1 5 ,   d a t a . G e t V a l u e   ( " A " ,   o u t   o r i g i n a l ) ) ;  
 	 	 	 A s s e r t . I s F a l s e   ( o r i g i n a l ) ;  
  
 	 	 	 d a t a . R e s e t T o O r i g i n a l V a l u e   ( " A " ) ;  
 	 	 	 d a t a . R e s e t T o O r i g i n a l V a l u e   ( " B " ) ;  
 	 	 	  
 	 	 	 A s s e r t . A r e E q u a l   ( 1 0 ,   d a t a . G e t V a l u e   ( " A " ,   o u t   o r i g i n a l ) ) ;  
 	 	 	 A s s e r t . I s T r u e   ( o r i g i n a l ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( U n d e f i n e d V a l u e . V a l u e ,   d a t a . G e t V a l u e   ( " B " ,   o u t   o r i g i n a l ) ) ;  
 	 	 	 A s s e r t . I s T r u e   ( o r i g i n a l ) ;  
  
 	 	 	 d a t a . C o p y O r i g i n a l T o C u r r e n t V a l u e   ( " A " ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( 1 0 ,   d a t a . G e t V a l u e   ( " A " ,   o u t   o r i g i n a l ) ) ;  
 	 	 	 A s s e r t . I s F a l s e   ( o r i g i n a l ) ;  
  
 	 	 	 d a t a . S e t V a l u e   ( " A " ,   U n d e f i n e d V a l u e . V a l u e ) ;  
 	 	 	  
 	 	 	 A s s e r t . A r e E q u a l   ( U n d e f i n e d V a l u e . V a l u e ,   d a t a . G e t V a l u e   ( " A " ,   o u t   o r i g i n a l ) ) ;  
 	 	 	 A s s e r t . I s F a l s e   ( o r i g i n a l ) ;  
 	 	 	  
 	 	 	 d a t a . P r o m o t e T o O r i g i n a l V a l u e   ( " A " ) ;  
 	 	 	 d a t a . R e s e t T o O r i g i n a l V a l u e   ( " A " ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( U n d e f i n e d V a l u e . V a l u e ,   d a t a . G e t V a l u e   ( " A " ,   o u t   o r i g i n a l ) ) ;  
 	 	 	 A s s e r t . I s T r u e   ( o r i g i n a l ) ;  
 	 	 }  
  
 	 	 [ T e s t ]  
 	 	 p u b l i c   v o i d   C h e c k S t r u c t u r e d D a t a W i t h T y p e A n d R e l a t i o n ( )  
 	 	 {  
 	 	 	 S t r u c t u r e d T y p e   t y p e 1   =   n e w   S t r u c t u r e d T y p e   ( ) ;  
 	 	 	 S t r u c t u r e d T y p e   t y p e 2   =   n e w   S t r u c t u r e d T y p e   ( ) ;  
  
 	 	 	 S t r u c t u r e d D a t a   d a t a 1   =   n e w   S t r u c t u r e d D a t a   ( t y p e 1 ) ;  
 	 	 	 S t r u c t u r e d D a t a   d a t a 2   =   n e w   S t r u c t u r e d D a t a   ( t y p e 2 ) ;  
  
 	 	 	 d a t a 1 . U n d e f i n e d V a l u e M o d e   =   U n d e f i n e d V a l u e M o d e . U n d e f i n e d ;  
 	 	 	 d a t a 2 . U n d e f i n e d V a l u e M o d e   =   U n d e f i n e d V a l u e M o d e . U n d e f i n e d ;  
  
 	 	 	 t y p e 1 . F i e l d s . A d d   ( n e w   S t r u c t u r e d T y p e F i e l d   ( " A " ,   t y p e 2 ,   D r u i d . E m p t y ,   0 ,   F i e l d R e l a t i o n . R e f e r e n c e ) ) ;  
 	 	 	 t y p e 1 . F i e l d s . A d d   ( n e w   S t r u c t u r e d T y p e F i e l d   ( " B " ,   t y p e 2 ,   D r u i d . E m p t y ,   1 ,   F i e l d R e l a t i o n . C o l l e c t i o n ) ) ;  
  
 	 	 	 t y p e 2 . F i e l d s . A d d   ( " X " ,   n e w   I n t e g e r T y p e   ( 0 ,   1 0 0 ) ) ;  
 	 	 	 t y p e 2 . D e f i n e I s N u l l a b l e   ( t r u e ) ;  
  
 	 	 	 L i s t < S t r u c t u r e d D a t a >   l i s t   =   n e w   L i s t < S t r u c t u r e d D a t a >   ( ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( U n d e f i n e d V a l u e . V a l u e ,   d a t a 1 . G e t V a l u e   ( " A " ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( U n d e f i n e d V a l u e . V a l u e ,   d a t a 1 . G e t V a l u e   ( " B " ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( U n k n o w n V a l u e . V a l u e ,   d a t a 1 . G e t V a l u e   ( " C " ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( U n d e f i n e d V a l u e . V a l u e ,   d a t a 2 . G e t V a l u e   ( " X " ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( U n k n o w n V a l u e . V a l u e ,   d a t a 2 . G e t V a l u e   ( " Y " ) ) ;  
  
 	 	 	 d a t a 1 . S e t V a l u e   ( " A " ,   d a t a 2 ) ;  
 	 	 	 d a t a 1 . S e t V a l u e   ( " B " ,   l i s t ) ;  
  
 	 	 	 d a t a 2 . S e t V a l u e   ( " X " ,   1 0 ) ;  
  
 	 	 	 / / 	 W e   c a n   u s e   a n   e m p t y   l i s t   o f   S t r u c t u r e d D a t a   ( t h e r e   i s   n o   p o s s i b l e  
 	 	 	 / / 	 v e r i f i c a t i o n )   o r   a   l i s t   w h i c h   c o n t a i n s   a   f i r s t   i t e m   o f   t h e   p r o p e r  
 	 	 	 / / 	 s t r u c t u r e d   t y p e   :  
  
 	 	 	 A s s e r t . I s T r u e   ( T y p e R o s e t t a . I s V a l i d V a l u e   ( l i s t ,   t y p e 1 . G e t F i e l d   ( " B " ) ) ) ;  
 	 	 	 l i s t . A d d   ( d a t a 2 ) ;  
 	 	 	 A s s e r t . I s T r u e   ( T y p e R o s e t t a . I s V a l i d V a l u e   ( l i s t ,   t y p e 1 . G e t F i e l d   ( " B " ) ) ) ;  
  
 	 	 	 / / 	 . . . b u t   w e   c a n n o t   u s e   a   l i s t   o f   S t r u c t u r e d D a t a   o f   t h e   w r o n g   t y p e .  
  
 	 	 	 l i s t . C l e a r   ( ) ;  
 	 	 	 l i s t . A d d   ( d a t a 1 ) ;  
 	 	 	 A s s e r t . I s F a l s e   ( T y p e R o s e t t a . I s V a l i d V a l u e   ( l i s t ,   t y p e 1 . G e t F i e l d   ( " B " ) ) ) ;  
 	 	 }  
  
 	 	 [ T e s t ]  
 	 	 [ E x p e c t e d E x c e p t i o n   ( t y p e o f   ( S y s t e m . C o l l e c t i o n s . G e n e r i c . K e y N o t F o u n d E x c e p t i o n ) ) ]  
 	 	 p u b l i c   v o i d   C h e c k S t r u c t u r e d D a t a W i t h T y p e E x 1 ( )  
 	 	 {  
 	 	 	 S t r u c t u r e d T y p e   t y p e   =   n e w   S t r u c t u r e d T y p e   ( ) ;  
 	 	 	 S t r u c t u r e d D a t a   d a t a   =   n e w   S t r u c t u r e d D a t a   ( t y p e ) ;  
  
 	 	 	 t y p e . F i e l d s . A d d   ( " A " ,   I n t e g e r T y p e . D e f a u l t ) ;  
 	 	 	 t y p e . F i e l d s . A d d   ( " B " ,   I n t e g e r T y p e . D e f a u l t ) ;  
  
 	 	 	 d a t a . S e t V a l u e   ( " X " ,   1 0 0 ) ;  
 	 	 }  
  
 	 	 [ T e s t ]  
 	 	 p u b l i c   v o i d   C h e c k S t r u c t u r e d D a t a W i t h T y p e E x 2 ( )  
 	 	 {  
 	 	 	 S t r u c t u r e d T y p e   t y p e   =   n e w   S t r u c t u r e d T y p e   ( ) ;  
 	 	 	 S t r u c t u r e d D a t a   d a t a   =   n e w   S t r u c t u r e d D a t a   ( t y p e ) ;  
  
 	 	 	 t y p e . F i e l d s . A d d   ( " A " ,   I n t e g e r T y p e . D e f a u l t ) ;  
 	 	 	 t y p e . F i e l d s . A d d   ( " B " ,   I n t e g e r T y p e . D e f a u l t ) ;  
  
 	 	 	 A s s e r t . I s T r u e   ( U n k n o w n V a l u e . I s U n k n o w n V a l u e   ( d a t a . G e t V a l u e   ( " X " ) ) ) ;  
 	 	 }  
  
 	 	 [ T e s t ]  
 	 	 [ E x p e c t e d E x c e p t i o n   ( t y p e o f   ( S y s t e m . A r g u m e n t E x c e p t i o n ) ) ]  
 	 	 p u b l i c   v o i d   C h e c k S t r u c t u r e d D a t a W i t h T y p e E x 3 ( )  
 	 	 {  
 	 	 	 S t r u c t u r e d T y p e   t y p e   =   n e w   S t r u c t u r e d T y p e   ( ) ;  
 	 	 	 S t r u c t u r e d D a t a   d a t a   =   n e w   S t r u c t u r e d D a t a   ( t y p e ) ;  
  
 	 	 	 t y p e . F i e l d s . A d d   ( " A " ,   n e w   I n t e g e r T y p e   ( 0 ,   1 0 0 ) ) ;  
  
 	 	 	 d a t a . S e t V a l u e   ( " A " ,   2 0 0 ) ;  
 	 	 }  
  
 	 	 [ T e s t ]  
 	 	 [ E x p e c t e d E x c e p t i o n   ( t y p e o f   ( S y s t e m . A r g u m e n t E x c e p t i o n ) ) ]  
 	 	 p u b l i c   v o i d   C h e c k S t r u c t u r e d D a t a W i t h T y p e E x 4 ( )  
 	 	 {  
 	 	 	 S t r u c t u r e d T y p e   t y p e   =   n e w   S t r u c t u r e d T y p e   ( ) ;  
 	 	 	 S t r u c t u r e d D a t a   d a t a   =   n e w   S t r u c t u r e d D a t a   ( t y p e ) ;  
  
 	 	 	 t y p e . F i e l d s . A d d   ( " A " ,   n e w   I n t e g e r T y p e   ( 0 ,   1 0 0 ) ) ;  
  
 	 	 	 d a t a . S e t V a l u e   ( " A " ,   " - " ) ;  
 	 	 }  
  
 	 	 [ T e s t ]  
 	 	 p u b l i c   v o i d   C h e c k S t r u c t u r e d T r e e I s P a t h V a l i d ( )  
 	 	 {  
 	 	 	 S t r u c t u r e d T y p e   r e c o r d   =   n e w   S t r u c t u r e d T y p e   ( ) ;  
  
 	 	 	 S t r u c t u r e d T e s t . F i l l   ( r e c o r d ) ;  
  
 	 	 	 A s s e r t . I s T r u e   ( S t r u c t u r e d T r e e . I s P a t h V a l i d   ( r e c o r d ,   " N u m b e r 1 " ) ) ;  
 	 	 	 A s s e r t . I s T r u e   ( S t r u c t u r e d T r e e . I s P a t h V a l i d   ( r e c o r d ,   " N u m b e r 2 " ) ) ;  
 	 	 	 A s s e r t . I s T r u e   ( S t r u c t u r e d T r e e . I s P a t h V a l i d   ( r e c o r d ,   " T e x t 1 " ) ) ;  
 	 	 	 A s s e r t . I s F a l s e   ( S t r u c t u r e d T r e e . I s P a t h V a l i d   ( r e c o r d ,   " T e x t 2 " ) ) ;  
 	 	 	 A s s e r t . I s F a l s e   ( S t r u c t u r e d T r e e . I s P a t h V a l i d   ( r e c o r d ,   n u l l ) ) ;  
 	 	 	 A s s e r t . I s F a l s e   ( S t r u c t u r e d T r e e . I s P a t h V a l i d   ( r e c o r d ,   " " ) ) ;  
 	 	 	 A s s e r t . I s T r u e   ( S t r u c t u r e d T r e e . I s P a t h V a l i d   ( r e c o r d ,   " P e r s o n n e " ) ) ;  
 	 	 	 A s s e r t . I s T r u e   ( S t r u c t u r e d T r e e . I s P a t h V a l i d   ( r e c o r d ,   " P e r s o n n e . N o m " ) ) ;  
 	 	 	 A s s e r t . I s T r u e   ( S t r u c t u r e d T r e e . I s P a t h V a l i d   ( r e c o r d ,   " P e r s o n n e . P r é n o m " ) ) ;  
 	 	 	 A s s e r t . I s T r u e   ( S t r u c t u r e d T r e e . I s P a t h V a l i d   ( r e c o r d ,   " P e r s o n n e . A d r e s s e " ) ) ;  
 	 	 	 A s s e r t . I s T r u e   ( S t r u c t u r e d T r e e . I s P a t h V a l i d   ( r e c o r d ,   " P e r s o n n e . A d r e s s e . N P A " ) ) ;  
 	 	 	 A s s e r t . I s T r u e   ( S t r u c t u r e d T r e e . I s P a t h V a l i d   ( r e c o r d ,   " P e r s o n n e . A d r e s s e . V i l l e " ) ) ;  
 	 	 }  
  
 	 	 [ T e s t ]  
 	 	 p u b l i c   v o i d   C h e c k S t r u c t u r e d T r e e G e t F i e l d ( )  
 	 	 {  
 	 	 	 S t r u c t u r e d T y p e   r e c o r d   =   n e w   S t r u c t u r e d T y p e   ( ) ;  
  
 	 	 	 S t r u c t u r e d T e s t . F i l l   ( r e c o r d ) ;  
  
 	 	 	 A s s e r t . I s T r u e   ( S t r u c t u r e d T r e e . G e t F i e l d   ( r e c o r d ,   " N u m b e r 1 " ) . T y p e   i s   D e c i m a l T y p e ) ;  
 	 	 	 A s s e r t . I s T r u e   ( S t r u c t u r e d T r e e . G e t F i e l d   ( r e c o r d ,   " T e x t 1 " ) . T y p e   i s   S t r i n g T y p e ) ;  
  
 	 	 	 A s s e r t . I s T r u e   ( S t r u c t u r e d T r e e . G e t F i e l d   ( r e c o r d ,   " P e r s o n n e " ) . T y p e   i s   S t r u c t u r e d T y p e ) ;  
 	 	 	 A s s e r t . I s T r u e   ( S t r u c t u r e d T r e e . G e t F i e l d   ( r e c o r d ,   " P e r s o n n e . A d r e s s e " ) . T y p e   i s   S t r u c t u r e d T y p e ) ;  
 	 	 	 A s s e r t . I s T r u e   ( S t r u c t u r e d T r e e . G e t F i e l d   ( r e c o r d ,   " P e r s o n n e . A d r e s s e . N P A " ) . T y p e   i s   I n t e g e r T y p e ) ;  
 	 	 }  
  
 	 	 [ T e s t ]  
 	 	 p u b l i c   v o i d   C h e c k S t r u c t u r e d T r e e G e t S a m p l e V a l u e ( )  
 	 	 {  
 	 	 	 S t r i n g T y p e   s t r T y p e   =   n e w   S t r i n g T y p e   ( ) ;  
 	 	 	 D a t e T y p e   d a t e T y p e   =   n e w   D a t e T y p e   ( ) ;  
  
 	 	 	 s t r T y p e . D e f i n e S a m p l e V a l u e   ( " A b c " ) ;  
 	 	 	 d a t e T y p e . D e f i n e S a m p l e V a l u e   ( D a t e . T o d a y ) ;  
  
 	 	 	 S t r u c t u r e d T y p e   r e c 1   =   n e w   S t r u c t u r e d T y p e   ( ) ;  
 	 	 	 S t r u c t u r e d T y p e   r e c 2   =   n e w   S t r u c t u r e d T y p e   ( ) ;  
 	 	 	 S t r u c t u r e d T y p e   r e c 3   =   n e w   S t r u c t u r e d T y p e   ( ) ;  
  
 	 	 	 r e c 1 . F i e l d s . A d d   ( " E m p l o y e e " ,   r e c 2 ) ;  
 	 	 	 r e c 1 . F i e l d s . A d d   ( " C o m m e n t " ,   s t r T y p e ) ;  
  
 	 	 	 r e c 2 . F i e l d s . A d d   ( " F i r s t N a m e " ,   s t r T y p e ) ;  
 	 	 	 r e c 2 . F i e l d s . A d d   ( " L a s t N a m e " ,   s t r T y p e ) ;  
 	 	 	 r e c 2 . F i e l d s . A d d   ( " H i s t o r y " ,   r e c 3 ) ;  
  
 	 	 	 r e c 3 . F i e l d s . A d d   ( " H i r e D a t e " ,   d a t e T y p e ) ;  
 	 	 	 r e c 3 . F i e l d s . A d d   ( " F i r e D a t e " ,   d a t e T y p e ) ;  
  
 	 	 	 S t r u c t u r e d D a t a   d a t a   =   n e w   S t r u c t u r e d D a t a   ( r e c 1 ) ;  
  
 	 	 	 A s s e r t . I s N o t N u l l   ( S t r u c t u r e d T r e e . G e t S a m p l e V a l u e   ( d a t a ,   " E m p l o y e e " ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( t y p e o f   ( S t r u c t u r e d D a t a ) ,   S t r u c t u r e d T r e e . G e t S a m p l e V a l u e   ( d a t a ,   " E m p l o y e e " ) . G e t T y p e   ( ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " A b c " ,   S t r u c t u r e d T r e e . G e t S a m p l e V a l u e   ( d a t a ,   " C o m m e n t " ) ) ;  
  
 	 	 	 A s s e r t . I s N o t N u l l   ( S t r u c t u r e d T r e e . G e t S a m p l e V a l u e   ( d a t a ,   " E m p l o y e e . F i r s t N a m e " ) ) ;  
 	 	 	 A s s e r t . I s N o t N u l l   ( S t r u c t u r e d T r e e . G e t S a m p l e V a l u e   ( d a t a ,   " E m p l o y e e . L a s t N a m e " ) ) ;  
 	 	 	 A s s e r t . I s N o t N u l l   ( S t r u c t u r e d T r e e . G e t S a m p l e V a l u e   ( d a t a ,   " E m p l o y e e . H i s t o r y " ) ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( " A b c " ,   S t r u c t u r e d T r e e . G e t S a m p l e V a l u e   ( d a t a ,   " E m p l o y e e . F i r s t N a m e " ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " A b c " ,   S t r u c t u r e d T r e e . G e t S a m p l e V a l u e   ( d a t a ,   " E m p l o y e e . L a s t N a m e " ) ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( D a t e . T o d a y ,   S t r u c t u r e d T r e e . G e t S a m p l e V a l u e   ( d a t a ,   " E m p l o y e e . H i s t o r y . H i r e D a t e " ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( D a t e . T o d a y ,   S t r u c t u r e d T r e e . G e t S a m p l e V a l u e   ( d a t a ,   " E m p l o y e e . H i s t o r y . F i r e D a t e " ) ) ;  
 	 	 }  
  
 	 	 [ T e s t ]  
 	 	 p u b l i c   v o i d   C h e c k S t r u c t u r e d T r e e G e t V a l u e ( )  
 	 	 {  
 	 	 	 S t r u c t u r e d T y p e   r e c o r d   =   n e w   S t r u c t u r e d T y p e   ( ) ;  
 	 	 	 S t r u c t u r e d D a t a   d a t a   =   n e w   S t r u c t u r e d D a t a   ( r e c o r d ) ;  
  
 	 	 	 r e c o r d . F i e l d s . A d d   ( " X " ,   S t r i n g T y p e . N a t i v e D e f a u l t ) ;  
 	 	 	 r e c o r d . F i e l d s . A d d   ( " Z " ,   S t r i n g T y p e . N a t i v e D e f a u l t ) ;  
  
 	 	 	 d a t a . S e t V a l u e   ( " Z " ,   " z " ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( U n d e f i n e d V a l u e . V a l u e ,   S t r u c t u r e d T r e e . G e t V a l u e   ( d a t a ,   " X " ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( U n k n o w n V a l u e . V a l u e ,   S t r u c t u r e d T r e e . G e t V a l u e   ( d a t a ,   " Y " ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " z " ,   S t r u c t u r e d T r e e . G e t V a l u e   ( d a t a ,   " Z " ) ) ;  
 	 	 }  
  
 	 	 [ T e s t ]  
 	 	 p u b l i c   v o i d   C h e c k S t r u c t u r e d T r e e M i s c ( )  
 	 	 {  
 	 	 	 A s s e r t . A r e E q u a l   ( " a * b * c " ,   s t r i n g . J o i n   ( " * " ,   S t r u c t u r e d T r e e . S p l i t P a t h   ( " a . b . c " ) ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " a . b . c . d " ,   S t r u c t u r e d T r e e . C r e a t e P a t h   ( " a " ,   " b " ,   " c . d " ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " " ,   S t r u c t u r e d T r e e . C r e a t e P a t h   ( ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " " ,   S t r u c t u r e d T r e e . C r e a t e P a t h   ( " " ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( 0 ,   S t r u c t u r e d T r e e . S p l i t P a t h   ( n u l l ) . L e n g t h ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( 0 ,   S t r u c t u r e d T r e e . S p l i t P a t h   ( " " ) . L e n g t h ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " a . b . c . d " ,   S t r u c t u r e d T r e e . G e t S u b P a t h   ( " a . b . c . d " ,   0 ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " b . c . d " ,   S t r u c t u r e d T r e e . G e t S u b P a t h   ( " a . b . c . d " ,   1 ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " c . d " ,   S t r u c t u r e d T r e e . G e t S u b P a t h   ( " a . b . c . d " ,   2 ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " " ,   S t r u c t u r e d T r e e . G e t S u b P a t h   ( " a . b . c . d " ,   1 0 ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " a " ,   S t r u c t u r e d T r e e . G e t R o o t N a m e   ( " a . b . c . d " ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " a b c " ,   S t r u c t u r e d T r e e . G e t R o o t N a m e   ( " a b c " ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " " ,   S t r u c t u r e d T r e e . G e t R o o t N a m e   ( " " ) ) ;  
 	 	 	 A s s e r t . I s N u l l   ( S t r u c t u r e d T r e e . G e t R o o t N a m e   ( n u l l ) ) ;  
  
 	 	 	 s t r i n g   l e a f P a t h ;  
 	 	 	 s t r i n g   l e a f N a m e ;  
  
 	 	 	 l e a f P a t h   =   S t r u c t u r e d T r e e . G e t L e a f P a t h   ( " a . b . c . d " ,   o u t   l e a f N a m e ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( " a . b . c " ,   l e a f P a t h ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " d " ,   l e a f N a m e ) ;  
  
 	 	 	 l e a f P a t h   =   S t r u c t u r e d T r e e . G e t L e a f P a t h   ( " a " ,   o u t   l e a f N a m e ) ;  
  
 	 	 	 A s s e r t . I s N u l l   ( l e a f P a t h ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " a " ,   l e a f N a m e ) ;  
  
 	 	 	 l e a f P a t h   =   S t r u c t u r e d T r e e . G e t L e a f P a t h   ( " " ,   o u t   l e a f N a m e ) ;  
  
 	 	 	 A s s e r t . I s N u l l   ( l e a f P a t h ) ;  
 	 	 	 A s s e r t . I s N u l l   ( l e a f N a m e ) ;  
 	 	 }  
  
 	 	 [ T e s t ]  
 	 	 p u b l i c   v o i d   C h e c k S t r u c t u r e d T r e e ( )  
 	 	 {  
 	 	 	 S t r u c t u r e d T y p e   t y p e   =   n e w   S t r u c t u r e d T y p e   ( ) ;  
 	 	 	 S t r u c t u r e d D a t a   r e c o r d   =   n e w   S t r u c t u r e d D a t a   ( n u l l ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( 0 ,   C o l l e c t i o n . C o u n t   ( t y p e . G e t F i e l d I d s   ( ) ) ) ;  
  
 	 	 	 S t r u c t u r e d T e s t . F i l l   ( t y p e ) ;  
  
 	 	 	 r e c o r d   =   n e w   S t r u c t u r e d D a t a   ( t y p e ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( " N u m b e r 1 / N u m b e r 2 / P e r s o n n e / T e x t 1 " ,   s t r i n g . J o i n   ( " / " ,   C o l l e c t i o n . T o S o r t e d A r r a y   ( t y p e . G e t F i e l d I d s   ( ) ) ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " P e r s o n n e . A d r e s s e / P e r s o n n e . N o m / P e r s o n n e . P r é n o m " ,   s t r i n g . J o i n   ( " / " ,   C o l l e c t i o n . T o S o r t e d A r r a y   ( S t r u c t u r e d T r e e . G e t F i e l d P a t h s   ( t y p e ,   " P e r s o n n e " ) ) ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " P e r s o n n e . A d r e s s e . N P A / P e r s o n n e . A d r e s s e . V i l l e " ,   s t r i n g . J o i n   ( " / " ,   C o l l e c t i o n . T o S o r t e d A r r a y   ( S t r u c t u r e d T r e e . G e t F i e l d P a t h s   ( t y p e ,   " P e r s o n n e . A d r e s s e " ) ) ) ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( 0 ,   C o l l e c t i o n . C o u n t   ( S t r u c t u r e d T r e e . G e t F i e l d P a t h s   ( t y p e ,   " N u m b e r 1 " ) ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( 0 ,   C o l l e c t i o n . C o u n t   ( S t r u c t u r e d T r e e . G e t F i e l d P a t h s   ( t y p e ,   " P e r s o n n e . A d r e s s e . V i l l e " ) ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( 0 ,   C o l l e c t i o n . C o u n t   ( S t r u c t u r e d T r e e . G e t F i e l d P a t h s   ( t y p e ,   " X " ) ) ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( " N u m b e r 1 / N u m b e r 2 / P e r s o n n e / T e x t 1 " ,   s t r i n g . J o i n   ( " / " ,   C o l l e c t i o n . T o S o r t e d A r r a y   ( r e c o r d . S t r u c t u r e d T y p e . G e t F i e l d I d s   ( ) ) ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " P e r s o n n e . A d r e s s e / P e r s o n n e . N o m / P e r s o n n e . P r é n o m " ,   s t r i n g . J o i n   ( " / " ,   C o l l e c t i o n . T o S o r t e d A r r a y   ( S t r u c t u r e d T r e e . G e t F i e l d P a t h s   ( r e c o r d . S t r u c t u r e d T y p e ,   " P e r s o n n e " ) ) ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " P e r s o n n e . A d r e s s e . N P A / P e r s o n n e . A d r e s s e . V i l l e " ,   s t r i n g . J o i n   ( " / " ,   C o l l e c t i o n . T o S o r t e d A r r a y   ( S t r u c t u r e d T r e e . G e t F i e l d P a t h s   ( r e c o r d . S t r u c t u r e d T y p e ,   " P e r s o n n e . A d r e s s e " ) ) ) ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( 0 ,   C o l l e c t i o n . C o u n t   ( S t r u c t u r e d T r e e . G e t F i e l d P a t h s   ( r e c o r d . S t r u c t u r e d T y p e ,   " N u m b e r 1 " ) ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( 0 ,   C o l l e c t i o n . C o u n t   ( S t r u c t u r e d T r e e . G e t F i e l d P a t h s   ( r e c o r d . S t r u c t u r e d T y p e ,   " P e r s o n n e . A d r e s s e . V i l l e " ) ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( 0 ,   C o l l e c t i o n . C o u n t   ( S t r u c t u r e d T r e e . G e t F i e l d P a t h s   ( r e c o r d . S t r u c t u r e d T y p e ,   " X " ) ) ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( t y p e o f   ( D e c i m a l T y p e ) ,   r e c o r d . S t r u c t u r e d T y p e . G e t F i e l d   ( " N u m b e r 1 " ) . T y p e . G e t T y p e   ( ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( t y p e o f   ( S t r i n g T y p e ) ,   r e c o r d . S t r u c t u r e d T y p e . G e t F i e l d   ( " T e x t 1 " ) . T y p e . G e t T y p e   ( ) ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( t y p e o f   ( S t r u c t u r e d T y p e ) ,   r e c o r d . S t r u c t u r e d T y p e . G e t F i e l d   ( " P e r s o n n e " ) . T y p e . G e t T y p e   ( ) ) ;  
  
 	 	 	 A s s e r t . I s T r u e   ( S t r u c t u r e d T r e e . I s P a t h V a l i d   ( t y p e ,   " N u m b e r 1 " ) ) ;  
 	 	 	 A s s e r t . I s T r u e   ( S t r u c t u r e d T r e e . I s P a t h V a l i d   ( t y p e ,   " P e r s o n n e . N o m " ) ) ;  
 	 	 	 A s s e r t . I s T r u e   ( S t r u c t u r e d T r e e . I s P a t h V a l i d   ( t y p e ,   " P e r s o n n e . A d r e s s e . V i l l e " ) ) ;  
 	 	 	 A s s e r t . I s F a l s e   ( S t r u c t u r e d T r e e . I s P a t h V a l i d   ( t y p e ,   " P e r s o n n e . A d r e s s e . P a y s " ) ) ;  
 	 	 	 A s s e r t . I s F a l s e   ( S t r u c t u r e d T r e e . I s P a t h V a l i d   ( t y p e ,   " C l i e n t . A d r e s s e . P a y s " ) ) ;  
 	 	 }  
  
 	 	 [ T e s t ]  
 	 	 [ E x p e c t e d E x c e p t i o n   ( t y p e o f   ( S y s t e m . A r g u m e n t E x c e p t i o n ) ) ]  
 	 	 p u b l i c   v o i d   C h e c k S t r u c t u r e d T y p e E x 1 ( )  
 	 	 {  
 	 	 	 S t r u c t u r e d T y p e   t y p e   =   n e w   S t r u c t u r e d T y p e   ( S t r u c t u r e d T y p e C l a s s . E n t i t y ) ;  
 	 	 	 t y p e . S e t V a l u e   ( S t r u c t u r e d T y p e . D e b u g D i s a b l e C h e c k s P r o p e r t y ,   t r u e ) ;  
  
 	 	 	 t y p e . F i e l d s . A d d   ( n e w   S t r u c t u r e d T y p e F i e l d   ( " N a m e " ,   S t r i n g T y p e . N a t i v e D e f a u l t ,   D r u i d . E m p t y ,   0 ,   F i e l d R e l a t i o n . R e f e r e n c e ) ) ;  
 	 	 }  
  
 	 	 [ T e s t ]  
 	 	 [ E x p e c t e d E x c e p t i o n   ( t y p e o f   ( S y s t e m . I n v a l i d O p e r a t i o n E x c e p t i o n ) ) ]  
 	 	 [ I g n o r e ]  
 	 	 p u b l i c   v o i d   C h e c k S t r u c t u r e d T y p e E x 2 ( )  
 	 	 {  
 	 	 	 S t r u c t u r e d T y p e   t y p e   =   n e w   S t r u c t u r e d T y p e   ( S t r u c t u r e d T y p e C l a s s . E n t i t y ) ;  
 	 	 	 t y p e . S e t V a l u e   ( S t r u c t u r e d T y p e . D e b u g D i s a b l e C h e c k s P r o p e r t y ,   t r u e ) ;  
 	 	 	 D r u i d   t y p e I d   =   D r u i d . P a r s e   ( " [ 1 2 3 4 5 6 7 8 0 ] " ) ;  
  
 	 	 	 T y p e R o s e t t a . R e c o r d T y p e   ( t y p e I d ,   t y p e ) ;  
  
 	 	 	 t y p e . D e f i n e C a p t i o n I d   ( t y p e I d ) ;  
  
 	 	 	 t y p e . F i e l d s . A d d   ( n e w   S t r u c t u r e d T y p e F i e l d   ( " N a m e " ,   S t r i n g T y p e . N a t i v e D e f a u l t ,   D r u i d . E m p t y ,   0 ,   F i e l d R e l a t i o n . N o n e ) ) ;  
 / / - 	 	 	 t y p e . F i e l d s . A d d   ( n e w   S t r u c t u r e d T y p e F i e l d   ( " S e l f N a m e " ,   t y p e ,   D r u i d . E m p t y ,   1 ,   F i e l d R e l a t i o n . I n c l u s i o n ) ) ;  
 	 	 }  
  
 	 	 [ T e s t ]  
 	 	 [ E x p e c t e d E x c e p t i o n   ( t y p e o f   ( S y s t e m . A r g u m e n t E x c e p t i o n ) ) ]  
 	 	 [ I g n o r e ]  
 	 	 p u b l i c   v o i d   C h e c k S t r u c t u r e d T y p e E x 3 ( )  
 	 	 {  
 	 	 	 S t r u c t u r e d T y p e   i n t e r f   =   n e w   S t r u c t u r e d T y p e   ( S t r u c t u r e d T y p e C l a s s . I n t e r f a c e ) ;  
 	 	 	 S t r u c t u r e d T y p e   e n t i t y   =   n e w   S t r u c t u r e d T y p e   ( S t r u c t u r e d T y p e C l a s s . E n t i t y ) ;  
 	 	 	 i n t e r f . S e t V a l u e   ( S t r u c t u r e d T y p e . D e b u g D i s a b l e C h e c k s P r o p e r t y ,   t r u e ) ;  
 	 	 	 e n t i t y . S e t V a l u e   ( S t r u c t u r e d T y p e . D e b u g D i s a b l e C h e c k s P r o p e r t y ,   t r u e ) ;  
  
 	 	 	 D r u i d   t y p e I d 1   =   D r u i d . P a r s e   ( " [ 1 2 3 4 5 6 7 8 1 ] " ) ;  
 	 	 	 D r u i d   t y p e I d 2   =   D r u i d . P a r s e   ( " [ 1 2 3 4 5 6 7 8 2 ] " ) ;  
  
 	 	 	 T y p e R o s e t t a . R e c o r d T y p e   ( t y p e I d 1 ,   i n t e r f ) ;  
 	 	 	 T y p e R o s e t t a . R e c o r d T y p e   ( t y p e I d 2 ,   e n t i t y ) ;  
  
 	 	 	 i n t e r f . D e f i n e C a p t i o n I d   ( t y p e I d 1 ) ;  
 	 	 	 e n t i t y . D e f i n e C a p t i o n I d   ( t y p e I d 2 ) ;  
  
 	 	 	 i n t e r f . F i e l d s . A d d   ( n e w   S t r u c t u r e d T y p e F i e l d   ( " N a m e " ,   S t r i n g T y p e . N a t i v e D e f a u l t ,   D r u i d . E m p t y ,   0 ,   F i e l d R e l a t i o n . N o n e ) ) ;  
  
 / / - 	 	 	 e n t i t y . F i e l d s . A d d   ( n e w   S t r u c t u r e d T y p e F i e l d   ( " N a m e " ,   i n t e r f ,   D r u i d . E m p t y ,   1 ,   F i e l d R e l a t i o n . I n c l u s i o n ) ) ;  
 / / - 	 	 	 e n t i t y . F i e l d s . A d d   ( n e w   S t r u c t u r e d T y p e F i e l d   ( " N a m e " ,   i n t e r f ,   D r u i d . E m p t y ,   2 ,   F i e l d R e l a t i o n . I n c l u s i o n ) ) ;  
 	 	 }  
  
 	 	 [ T e s t ]  
 	 	 p u b l i c   v o i d   C h e c k S t r u c t u r e d T y p e I n c l u s i o n ( )  
 	 	 {  
 	 	 	 / / A s s e r t . I s N u l l   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . T y p e s . T e s t I n t e r f a c e U s e r . B a s e T y p e ) ;  
 	 	 	 / / A s s e r t . A r e E q u a l   ( 1 ,   C o l l e c t i o n . C o u n t   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . T y p e s . T e s t I n t e r f a c e U s e r . G e t I n t e r f a c e I d s   ( f a l s e ) ) ) ;  
 	 	 	 / / A s s e r t . A r e E q u a l   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . T y p e s . T e s t I n t e r f a c e . C a p t i o n I d ,   C o l l e c t i o n . G e t F i r s t   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . T y p e s . T e s t I n t e r f a c e U s e r . G e t I n t e r f a c e I d s   ( f a l s e ) ) ) ;  
  
 	 	 	 / / s t r i n g [ ]   i d s   =   C o l l e c t i o n . T o A r r a y   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . T y p e s . T e s t I n t e r f a c e U s e r . G e t F i e l d I d s   ( ) ) ;  
  
 	 	 	 / / A s s e r t . A r e E q u a l   ( 3 ,   i d s . L e n g t h ) ;  
 	 	 	 / / A s s e r t . A r e E q u a l   ( " [ 7 0 0 J 2 ] " ,   i d s [ 0 ] ) ; 	 / / 	 f r o m   i n t e r f a c e ,   " N a m e "   - -   f i e l d   r e d e f i n e d   b y   T e s t I n t e r f a c e U s e r  
 	 	 	 / / A s s e r t . A r e E q u a l   ( " [ 7 0 1 2 ] " ,   i d s [ 1 ] ) ; 	 	 / / 	 f r o m   i n t e r f a c e ,   " R e s o u r c e "  
 	 	 	 / / A s s e r t . A r e E q u a l   ( " [ 7 0 1 4 ] " ,   i d s [ 2 ] ) ; 	 	 / / 	 l o c a l l y   d e f i n e d ,   " E x t e n s i o n 1 "  
  
 	 	 	 / / A s s e r t . A r e E q u a l   ( F i e l d S o u r c e . E x p r e s s i o n ,   E p s i t e c . C o m m o n . S u p p o r t . R e s . T y p e s . T e s t I n t e r f a c e U s e r . G e t F i e l d   ( " [ 7 0 0 J 2 ] " ) . S o u r c e ) ;  
 	 	 	 / / A s s e r t . A r e E q u a l   ( F i e l d S o u r c e . E x p r e s s i o n ,   E p s i t e c . C o m m o n . S u p p o r t . R e s . T y p e s . T e s t I n t e r f a c e U s e r . G e t F i e l d   ( " [ 7 0 1 2 ] " ) . S o u r c e ) ;  
 	 	 	 / / A s s e r t . A r e E q u a l   ( F i e l d S o u r c e . E x p r e s s i o n ,   E p s i t e c . C o m m o n . S u p p o r t . R e s . T y p e s . T e s t I n t e r f a c e U s e r . G e t F i e l d   ( " [ 7 0 1 4 ] " ) . S o u r c e ) ;  
 	 	 	  
 	 	 	 / / A s s e r t . A r e E q u a l   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . T y p e s . T e s t I n t e r f a c e . C a p t i o n I d ,   E p s i t e c . C o m m o n . S u p p o r t . R e s . T y p e s . T e s t I n t e r f a c e U s e r . G e t F i e l d   ( " [ 7 0 0 J 2 ] " ) . D e f i n i n g T y p e I d ) ;  
 	 	 	 / / A s s e r t . A r e E q u a l   ( E p s i t e c . C o m m o n . S u p p o r t . R e s . T y p e s . T e s t I n t e r f a c e . C a p t i o n I d ,   E p s i t e c . C o m m o n . S u p p o r t . R e s . T y p e s . T e s t I n t e r f a c e U s e r . G e t F i e l d   ( " [ 7 0 1 2 ] " ) . D e f i n i n g T y p e I d ) ;  
 	 	 	 / / A s s e r t . A r e E q u a l   ( D r u i d . E m p t y ,   E p s i t e c . C o m m o n . S u p p o r t . R e s . T y p e s . T e s t I n t e r f a c e U s e r . G e t F i e l d   ( " [ 7 0 1 4 ] " ) . D e f i n i n g T y p e I d ) ;  
  
 	 	 	 / / A s s e r t . A r e E q u a l   ( F i e l d M e m b e r s h i p . L o c a l O v e r r i d e ,   E p s i t e c . C o m m o n . S u p p o r t . R e s . T y p e s . T e s t I n t e r f a c e U s e r . G e t F i e l d   ( " [ 7 0 0 J 2 ] " ) . M e m b e r s h i p ) ;  
 	 	 	 / / A s s e r t . A r e E q u a l   ( F i e l d M e m b e r s h i p . L o c a l ,   E p s i t e c . C o m m o n . S u p p o r t . R e s . T y p e s . T e s t I n t e r f a c e U s e r . G e t F i e l d   ( " [ 7 0 1 2 ] " ) . M e m b e r s h i p ) ;  
 	 	 	 / / A s s e r t . A r e E q u a l   ( F i e l d M e m b e r s h i p . L o c a l ,   E p s i t e c . C o m m o n . S u p p o r t . R e s . T y p e s . T e s t I n t e r f a c e U s e r . G e t F i e l d   ( " [ 7 0 1 4 ] " ) . M e m b e r s h i p ) ;  
 	 	 	  
 	 	 	 / / A s s e r t . A r e E q u a l   ( " »/ c # \ r \ n x   = >   s t r i n g . E m p t y " ,   E p s i t e c . C o m m o n . S u p p o r t . R e s . T y p e s . T e s t I n t e r f a c e U s e r . G e t F i e l d   ( " [ 7 0 0 J 2 ] " ) . E x p r e s s i o n ) ;  
 	 	 	 / / A s s e r t . A r e E q u a l   ( " »/ c # \ r \ n x   = >   x . N a m e . T o U p p e r   ( ) " ,   E p s i t e c . C o m m o n . S u p p o r t . R e s . T y p e s . T e s t I n t e r f a c e U s e r . G e t F i e l d   ( " [ 7 0 1 4 ] " ) . E x p r e s s i o n ) ;  
 	 	 }  
  
 	 	 [ T e s t ]  
 	 	 p u b l i c   v o i d   C h e c k S t r u c t u r e d T y p e R e l a t i o n s ( )  
 	 	 {  
 	 	 	 S t r u c t u r e d T y p e   i n t e r f   =   n e w   S t r u c t u r e d T y p e   ( S t r u c t u r e d T y p e C l a s s . I n t e r f a c e ) ;  
 	 	 	 S t r u c t u r e d T y p e   e n t i t y   =   n e w   S t r u c t u r e d T y p e   ( S t r u c t u r e d T y p e C l a s s . E n t i t y ) ;  
 	 	 	 i n t e r f . S e t V a l u e   ( S t r u c t u r e d T y p e . D e b u g D i s a b l e C h e c k s P r o p e r t y ,   t r u e ) ;  
 	 	 	 e n t i t y . S e t V a l u e   ( S t r u c t u r e d T y p e . D e b u g D i s a b l e C h e c k s P r o p e r t y ,   t r u e ) ;  
  
 	 	 	 D r u i d   t y p e I d   =   D r u i d . P a r s e   ( " [ 1 2 3 4 5 6 7 8 A ] " ) ;  
  
 	 	 	 T y p e R o s e t t a . R e c o r d T y p e   ( t y p e I d ,   i n t e r f ) ;  
  
 	 	 	 i n t e r f . D e f i n e C a p t i o n I d   ( t y p e I d ) ;  
  
 	 	 	 i n t e r f . F i e l d s . A d d   ( n e w   S t r u c t u r e d T y p e F i e l d   ( " N a m e " ,   S t r i n g T y p e . N a t i v e D e f a u l t ,   D r u i d . E m p t y ,   0 ,   F i e l d R e l a t i o n . N o n e ) ) ;  
 	 	 	 i n t e r f . F i e l d s . A d d   ( n e w   S t r u c t u r e d T y p e F i e l d   ( " S e l f R e f " ,   i n t e r f ,   D r u i d . E m p t y ,   1 ,   F i e l d R e l a t i o n . R e f e r e n c e ) ) ;  
 	 	 	 i n t e r f . F i e l d s . A d d   ( n e w   S t r u c t u r e d T y p e F i e l d   ( " S e l f C o l l e c t i o n " ,   i n t e r f ,   D r u i d . E m p t y ,   2 ,   F i e l d R e l a t i o n . C o l l e c t i o n ) ) ;  
  
 / / - 	 	 	 e n t i t y . F i e l d s . A d d   ( n e w   S t r u c t u r e d T y p e F i e l d   ( " N a m e " ,   i n t e r f ,   D r u i d . E m p t y ,   0 ,   F i e l d R e l a t i o n . I n c l u s i o n ) ) ;  
 / / - 	 	 	 e n t i t y . F i e l d s . A d d   ( n e w   S t r u c t u r e d T y p e F i e l d   ( " S e l f R e f " ,   i n t e r f ,   D r u i d . E m p t y ,   0 ,   F i e l d R e l a t i o n . I n c l u s i o n ) ) ;  
 	 	 }  
  
 	 	 [ T e s t ]  
 	 	 p u b l i c   v o i d   C h e c k S t r u c t u r e d T y p e S e r i a l i z a t i o n ( )  
 	 	 {  
 	 	 	 S t r u c t u r e d T y p e   t y p e   =   n e w   S t r u c t u r e d T y p e   ( S t r u c t u r e d T y p e C l a s s . E n t i t y ) ;  
 	 	 	 t y p e . S e t V a l u e   ( S t r u c t u r e d T y p e . D e b u g D i s a b l e C h e c k s P r o p e r t y ,   t r u e ) ;  
 	 	 	 D r u i d   t y p e I d   =   D r u i d . P a r s e   ( " [ 1 2 3 4 5 6 7 8 B ] " ) ;  
  
 	 	 	 t y p e . C a p t i o n . N a m e   =   " T e s t S t r u c t " ;  
 	 	 	 t y p e . D e f i n e C a p t i o n I d   ( t y p e I d ) ;  
  
 	 	 	 t y p e . F i e l d s . A d d   ( " N a m e " ,   S t r i n g T y p e . N a t i v e D e f a u l t ) ;  
 	 	 	 t y p e . F i e l d s . A d d   ( " A g e " ,   I n t e g e r T y p e . D e f a u l t ) ;  
  
 	 	 	 t y p e . F i e l d s [ " N a m e " ]   =   n e w   S t r u c t u r e d T y p e F i e l d   ( " N a m e " ,   S t r i n g T y p e . N a t i v e D e f a u l t ,   D r u i d . E m p t y ,   1 ) ;  
 	 	 	 t y p e . I n t e r f a c e I d s . A d d   ( D r u i d . P a r s e   ( " [ 1 2 3 4 5 6 7 8 C ] " ) ) ;  
 	 	 	 t y p e . I n t e r f a c e I d s . A d d   ( D r u i d . P a r s e   ( " [ 1 2 3 4 5 6 7 8 D ] " ) ) ;  
  
 	 	 	 A s s e r t . I s N u l l   ( t y p e . S y s t e m T y p e ) ;  
  
 	 	 	 T y p e R o s e t t a . R e c o r d T y p e   ( t y p e I d ,   t y p e ) ;  
  
 	 	 	 s t r i n g   x m l   =   t y p e . C a p t i o n . S e r i a l i z e T o S t r i n g   ( ) ;  
  
 	 	 	 S y s t e m . C o n s o l e . O u t . W r i t e L i n e   ( " S t r u c t u r e d T y p e :   { 0 } " ,   x m l ) ;  
  
 	 	 	 C a p t i o n   c a p t i o n   =   n e w   C a p t i o n   ( ) ;  
 	 	 	 c a p t i o n . D e s e r i a l i z e F r o m S t r i n g   ( x m l ) ;  
  
 	 	 	 S t r u c t u r e d T y p e   r e s t o r e d T y p e   =   T y p e R o s e t t a . C r e a t e T y p e O b j e c t   ( c a p t i o n )   a s   S t r u c t u r e d T y p e ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( 2 ,   t y p e . I n t e r f a c e I d s . C o u n t ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( D r u i d . P a r s e   ( " [ 1 2 3 4 5 6 7 8 C ] " ) ,   t y p e . I n t e r f a c e I d s [ 0 ] ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( D r u i d . P a r s e   ( " [ 1 2 3 4 5 6 7 8 D ] " ) ,   t y p e . I n t e r f a c e I d s [ 1 ] ) ;  
  
 	 	 	 / / 	 - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  
 	 	 	 / / 	 W e   n e e d   t o   c l e a r   t h e   i n t e r f a c e   l i s t   s i n c e   t h e y   d o n ' t   r e a l l y   e x i s t  
 	 	 	 / / 	 a n d   t h e   a c c e s s   t o   t h e   F i e l d s   p r o p e r t y   w o u l d   c r a s h   :  
 	 	 	 t y p e . I n t e r f a c e I d s . C l e a r   ( ) ;  
 	 	 	 r e s t o r e d T y p e . I n t e r f a c e I d s . C l e a r   ( ) ;  
 	 	 	 / / 	 - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  
  
 	 	 	 A s s e r t . A r e E q u a l   ( t y p e . F i e l d s . C o u n t ,   r e s t o r e d T y p e . F i e l d s . C o u n t ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( t y p e . G e t F i e l d   ( " N a m e " ) . T y p e . G e t T y p e   ( ) . N a m e ,   r e s t o r e d T y p e . G e t F i e l d   ( " N a m e " ) . T y p e . G e t T y p e   ( ) . N a m e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( t y p e . G e t F i e l d   ( " A g e " ) . T y p e . G e t T y p e   ( ) . N a m e ,   r e s t o r e d T y p e . G e t F i e l d   ( " A g e " ) . T y p e . G e t T y p e   ( ) . N a m e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " N a m e " ,   r e s t o r e d T y p e . F i e l d s [ " N a m e " ] . I d ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " A g e " ,   r e s t o r e d T y p e . F i e l d s [ " A g e " ] . I d ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( 1 ,   r e s t o r e d T y p e . F i e l d s [ " N a m e " ] . R a n k ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( - 1 ,   r e s t o r e d T y p e . F i e l d s [ " A g e " ] . R a n k ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( S t r u c t u r e d T y p e C l a s s . E n t i t y ,   r e s t o r e d T y p e . C l a s s ) ;  
  
 	 	 	 L i s t < S t r u c t u r e d T y p e F i e l d >   f i e l d s   =   n e w   L i s t < S t r u c t u r e d T y p e F i e l d >   ( r e s t o r e d T y p e . F i e l d s . V a l u e s ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( " N a m e " ,   f i e l d s [ 0 ] . I d ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " A g e " ,   f i e l d s [ 1 ] . I d ) ;  
  
 	 	 	 f i e l d s . S o r t   ( S t r u c t u r e d T y p e . R a n k C o m p a r e r ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( " A g e " ,   f i e l d s [ 0 ] . I d ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " N a m e " ,   f i e l d s [ 1 ] . I d ) ;  
  
 	 	 	 s t r i n g [ ]   f i e l d I d s   =   C o l l e c t i o n . T o A r r a y < s t r i n g >   ( r e s t o r e d T y p e . G e t F i e l d I d s   ( ) ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( " A g e " ,   f i e l d I d s [ 0 ] ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " N a m e " ,   f i e l d I d s [ 1 ] ) ;  
  
 	 	 	 r e s t o r e d T y p e . F i e l d s . R e m o v e   ( " A g e " ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( 1 ,   r e s t o r e d T y p e . F i e l d s . C o u n t ) ;  
  
 	 	 	 r e s t o r e d T y p e . F i e l d s . R e m o v e   ( " X x x " ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( 1 ,   r e s t o r e d T y p e . F i e l d s . C o u n t ) ;  
 	 	 }  
  
 	 	 [ T e s t ]  
 	 	 [ E x p e c t e d E x c e p t i o n   ( t y p e o f   ( S y s t e m . A r g u m e n t E x c e p t i o n ) ) ]  
 	 	 p u b l i c   v o i d   C h e c k S t r u c t u r e d T y p e F i e l d I n s e r t i o n E x 1 ( )  
 	 	 {  
 	 	 	 S t r u c t u r e d T y p e   t y p e   =   n e w   S t r u c t u r e d T y p e   ( ) ;  
 	 	 	 S t r u c t u r e d T y p e F i e l d   f i e l d A b c   =   n e w   S t r u c t u r e d T y p e F i e l d   ( " A b c " ,   S t r i n g T y p e . N a t i v e D e f a u l t ) ;  
 	 	 	 S t r u c t u r e d T y p e F i e l d   f i e l d X y z   =   n e w   S t r u c t u r e d T y p e F i e l d   ( " X y z " ,   S t r i n g T y p e . N a t i v e D e f a u l t ) ;  
  
 	 	 	 t y p e . F i e l d s . A d d   ( f i e l d X y z ) ;  
 	 	 	 t y p e . F i e l d s [ " X y z " ]   =   f i e l d A b c ;  
 	 	 }  
  
 	 	 [ T e s t ]  
 	 	 p u b l i c   v o i d   C h e c k S t r u c t u r e d T y p e C r e a t e E n t i t i e s ( )  
 	 	 {  
 	 	 	 S t r u c t u r e d T y p e   e 1 ;  
 	 	 	 S t r u c t u r e d T y p e   e 2 ;  
 	 	 	 S t r u c t u r e d T y p e   e 3 ;  
  
 	 	 	 t h i s . C r e a t e E n t i t i e s   ( o u t   e 1 ,   o u t   e 2 ,   o u t   e 3 ) ;  
 	 	 }  
  
 	 	 [ T e s t ]  
 	 	 p u b l i c   v o i d   C h e c k S t r u c t u r e d T y p e C r e a t e I n t e r f a c e ( )  
 	 	 {  
 	 	 	 S t r u c t u r e d T y p e   i   =   t h i s . C r e a t e I n t e r f a c e   ( ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( 2 ,   i . F i e l d s . C o u n t ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " [ 4 0 0 I ] " ,   C o l l e c t i o n . T o A r r a y   ( i . G e t F i e l d I d s   ( ) ) [ 0 ] ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " [ 4 0 0 J ] " ,   C o l l e c t i o n . T o A r r a y   ( i . G e t F i e l d I d s   ( ) ) [ 1 ] ) ;  
  
 	 	 	 S t r u c t u r e d T y p e   e 1 ;  
 	 	 	 S t r u c t u r e d T y p e   e 2 ;  
 	 	 	 S t r u c t u r e d T y p e   e 3 ;  
  
 	 	 	 t h i s . C r e a t e E n t i t i e s   ( o u t   e 1 ,   o u t   e 2 ,   o u t   e 3 ) ;  
  
 	 	 	 i n t   n   =   e 1 . F i e l d s . C o u n t ;  
  
 	 	 	 e 1 . I n t e r f a c e I d s . A d d   ( i . C a p t i o n I d ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( n + 2 ,   e 1 . F i e l d s . C o u n t ) ;  
 	 	 	  
 	 	 	 A s s e r t . A r e E q u a l   ( F i e l d M e m b e r s h i p . L o c a l ,   e 1 . F i e l d s [ " [ 4 0 0 E ] " ] . M e m b e r s h i p ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( F i e l d M e m b e r s h i p . L o c a l ,   e 1 . F i e l d s [ " [ 4 0 0 F ] " ] . M e m b e r s h i p ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( F i e l d M e m b e r s h i p . L o c a l ,   e 1 . F i e l d s [ " [ 4 0 0 I ] " ] . M e m b e r s h i p ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( F i e l d M e m b e r s h i p . L o c a l ,   e 1 . F i e l d s [ " [ 4 0 0 J ] " ] . M e m b e r s h i p ) ;  
 	 	 	  
 	 	 	 A s s e r t . A r e E q u a l   ( D r u i d . E m p t y ,   e 1 . F i e l d s [ " [ 4 0 0 E ] " ] . D e f i n i n g T y p e I d ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( D r u i d . E m p t y ,   e 1 . F i e l d s [ " [ 4 0 0 F ] " ] . D e f i n i n g T y p e I d ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( i . C a p t i o n I d ,   e 1 . F i e l d s [ " [ 4 0 0 I ] " ] . D e f i n i n g T y p e I d ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( i . C a p t i o n I d ,   e 1 . F i e l d s [ " [ 4 0 0 J ] " ] . D e f i n i n g T y p e I d ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( 1 ,   e 1 . G e t I n t e r f a c e I d s   ( f a l s e ) . C o u n t ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " [ 4 0 0 H ] " ,   e 1 . G e t I n t e r f a c e I d s   ( f a l s e ) [ 0 ] . T o S t r i n g   ( ) ) ;  
 	 	 }  
  
 	 	 [ T e s t ]  
 	 	 p u b l i c   v o i d   C h e c k S t r u c t u r e d T y p e M e r g e 1 ( )  
 	 	 {  
 	 	 	 S t r u c t u r e d T y p e   e 1 ;  
 	 	 	 S t r u c t u r e d T y p e   e 2 ;  
 	 	 	 S t r u c t u r e d T y p e   e 3 ;  
  
 	 	 	 t h i s . C r e a t e E n t i t i e s   ( o u t   e 1 ,   o u t   e 2 ,   o u t   e 3 ) ;  
  
 	 	 	 S t r u c t u r e d T y p e   e 1 2   =   S t r u c t u r e d T y p e . M e r g e   ( e 1 ,   e 2 ) ;  
 	 	 	 S t r u c t u r e d T y p e   e 2 1   =   S t r u c t u r e d T y p e . M e r g e   ( e 2 ,   e 1 ) ;  
 	 	 	 S t r u c t u r e d T y p e   e 1 3   =   S t r u c t u r e d T y p e . M e r g e   ( e 1 ,   e 3 ) ;  
 	 	 	 S t r u c t u r e d T y p e   e 3 1   =   S t r u c t u r e d T y p e . M e r g e   ( e 3 ,   e 1 ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( " E 2 " ,   e 1 2 . N a m e ) ; 	 / / 	 E 1   m e r g e d   w i t h   E 2 ,   c a p t i o n   o f   E 2   w i n s  
 	 	 	 A s s e r t . A r e E q u a l   ( " E 1 " ,   e 2 1 . N a m e ) ; 	 / / 	 E 2   m e r g e d   w i t h   E 1 ,   c a p t i o n   o f   E 1   w i n s  
 	 	 	 A s s e r t . A r e E q u a l   ( " U 1 " ,   e 1 3 . N a m e ) ; 	 / / 	 E 1   m e r g e d   w i t h   U 1 ,   U 1   w i n s   a s   i t   i s   o f   a   h i g h e r   l a y e r  
 	 	 	 A s s e r t . A r e E q u a l   ( " U 1 " ,   e 3 1 . N a m e ) ; 	 / / 	 U 1   m e r g e d   w i t h   E 1 ,   U 1   w i n s   a s   i t   i s   o f   a   h i g h e r   l a y e r  
  
 	 	 	 A s s e r t . A r e E q u a l   ( R e s o u r c e M o d u l e L a y e r . A p p l i c a t i o n ,   e 1 . M o d u l e . L a y e r ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( R e s o u r c e M o d u l e L a y e r . A p p l i c a t i o n ,   e 2 . M o d u l e . L a y e r ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( R e s o u r c e M o d u l e L a y e r . U s e r ,   e 3 . M o d u l e . L a y e r ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( R e s o u r c e M o d u l e L a y e r . A p p l i c a t i o n ,   e 1 2 . M o d u l e . L a y e r ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( R e s o u r c e M o d u l e L a y e r . A p p l i c a t i o n ,   e 2 1 . M o d u l e . L a y e r ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( R e s o u r c e M o d u l e L a y e r . A p p l i c a t i o n ,   e 1 3 . M o d u l e . L a y e r ) ; 	 / / 	 A p p + U s e r   - - >   A p p  
 	 	 	 A s s e r t . A r e E q u a l   ( R e s o u r c e M o d u l e L a y e r . A p p l i c a t i o n ,   e 3 1 . M o d u l e . L a y e r ) ; 	 / / 	 U s e r + A p p   - - >   A p p  
  
 	 	 	 A s s e r t . A r e E q u a l   ( S t r u c t u r e d T y p e C l a s s . E n t i t y ,   e 1 2 . C l a s s ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( S t r u c t u r e d T y p e C l a s s . E n t i t y ,   e 2 1 . C l a s s ) ;  
 	 	 }  
  
 	 	 [ T e s t ]  
 	 	 p u b l i c   v o i d   C h e c k S t r u c t u r e d T y p e M e r g e 2 ( )  
 	 	 {  
 	 	 	 S t r u c t u r e d T y p e   e 1 ;  
 	 	 	 S t r u c t u r e d T y p e   e 2 ;  
 	 	 	 S t r u c t u r e d T y p e   e 3 ;  
  
 	 	 	 t h i s . C r e a t e E n t i t i e s   ( o u t   e 1 ,   o u t   e 2 ,   o u t   e 3 ) ;  
  
 	 	 	 S t r u c t u r e d T y p e   e 1 2   =   S t r u c t u r e d T y p e . M e r g e   ( e 1 ,   e 2 ) ;  
 	 	 	 S t r u c t u r e d T y p e   e 2 1   =   S t r u c t u r e d T y p e . M e r g e   ( e 2 ,   e 1 ) ;  
 	 	 	 S t r u c t u r e d T y p e   e 1 3   =   S t r u c t u r e d T y p e . M e r g e   ( e 1 ,   e 3 ) ;  
 	 	 	 S t r u c t u r e d T y p e   e 3 1   =   S t r u c t u r e d T y p e . M e r g e   ( e 3 ,   e 1 ) ;  
  
 	 	 	 s t r i n g [ ]   e 1 2 F i e l d s   =   C o l l e c t i o n . T o A r r a y   ( e 1 2 . G e t F i e l d I d s   ( ) ) ;  
 	 	 	 s t r i n g [ ]   e 2 1 F i e l d s   =   C o l l e c t i o n . T o A r r a y   ( e 2 1 . G e t F i e l d I d s   ( ) ) ;  
 	 	 	 s t r i n g [ ]   e 1 3 F i e l d s   =   C o l l e c t i o n . T o A r r a y   ( e 1 3 . G e t F i e l d I d s   ( ) ) ;  
 	 	 	 s t r i n g [ ]   e 3 1 F i e l d s   =   C o l l e c t i o n . T o A r r a y   ( e 3 1 . G e t F i e l d I d s   ( ) ) ;  
 	 	 	  
 	 	 	 A s s e r t . A r e E q u a l   ( 3 ,   e 1 2 . F i e l d s . C o u n t ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( 3 ,   e 2 1 . F i e l d s . C o u n t ) ;  
  
 	 	 	 / / 	 V e r i f y   f i e l d   m e r g e   a n d   r a n k   a s s i g n m e n t   :  
 	 	 	  
 	 	 	 A s s e r t . A r e E q u a l   ( " [ 4 0 0 E ] " ,   e 1 2 F i e l d s [ 0 ] ) ; 	 / / 	 E 1 . X  
 	 	 	 A s s e r t . A r e E q u a l   ( " [ 4 0 0 F ] " ,   e 1 2 F i e l d s [ 1 ] ) ; 	 / / 	 E 1 . Y  
 	 	 	 A s s e r t . A r e E q u a l   ( " [ 4 0 0 G ] " ,   e 1 2 F i e l d s [ 2 ] ) ; 	 / / 	 E 2 . Z  
 	 	 	 A s s e r t . A r e E q u a l   ( 0 ,   e 1 2 . F i e l d s [ e 1 2 F i e l d s [ 0 ] ] . R a n k ) ; 	 / / 	 E 1 . X ,   r a n k   =   0  
 	 	 	 A s s e r t . A r e E q u a l   ( 1 ,   e 1 2 . F i e l d s [ e 1 2 F i e l d s [ 1 ] ] . R a n k ) ; 	 / / 	 E 1 . Y ,   r a n k   =   1  
 	 	 	 A s s e r t . A r e E q u a l   ( 2 ,   e 1 2 . F i e l d s [ e 1 2 F i e l d s [ 2 ] ] . R a n k ) ; 	 / / 	 E 2 . Z ,   r a n k   =   2  
  
 	 	 	 A s s e r t . A r e E q u a l   ( " [ 4 0 0 G ] " ,   e 2 1 F i e l d s [ 0 ] ) ; 	 / / 	 E 2 . Z  
 	 	 	 A s s e r t . A r e E q u a l   ( " [ 4 0 0 E ] " ,   e 2 1 F i e l d s [ 1 ] ) ; 	 / / 	 E 1 . X  
 	 	 	 A s s e r t . A r e E q u a l   ( " [ 4 0 0 F ] " ,   e 2 1 F i e l d s [ 2 ] ) ; 	 / / 	 E 1 . Y  
 	 	 	 A s s e r t . A r e E q u a l   ( 0 ,   e 2 1 . F i e l d s [ e 2 1 F i e l d s [ 0 ] ] . R a n k ) ; 	 / / 	 E 2 . Z ,   r a n k   =   0  
 	 	 	 A s s e r t . A r e E q u a l   ( 1 ,   e 2 1 . F i e l d s [ e 2 1 F i e l d s [ 1 ] ] . R a n k ) ; 	 / / 	 E 1 . X ,   r a n k   =   1  
 	 	 	 A s s e r t . A r e E q u a l   ( 2 ,   e 2 1 . F i e l d s [ e 2 1 F i e l d s [ 2 ] ] . R a n k ) ; 	 / / 	 E 1 . Y ,   r a n k   =   2  
  
 	 	 	 A s s e r t . A r e E q u a l   ( 4 ,   e 1 3 . F i e l d s . C o u n t ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( 4 ,   e 3 1 . F i e l d s . C o u n t ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( " [ 4 0 0 E ] " ,   e 1 3 F i e l d s [ 0 ] ) ; 	 / / 	 E 1 . X  
 	 	 	 A s s e r t . A r e E q u a l   ( " [ 4 0 0 F ] " ,   e 1 3 F i e l d s [ 1 ] ) ; 	 / / 	 E 1 . Y  
 	 	 	 A s s e r t . A r e E q u a l   ( " [ V 0 0 2 ] " ,   e 1 3 F i e l d s [ 2 ] ) ; 	 / / 	 U 1 . V  
 	 	 	 A s s e r t . A r e E q u a l   ( " [ V 0 0 3 ] " ,   e 1 3 F i e l d s [ 3 ] ) ; 	 / / 	 U 1 . W  
  
 	 	 	 A s s e r t . A r e E q u a l   ( " [ 4 0 0 E ] " ,   e 3 1 F i e l d s [ 0 ] ) ; 	 / / 	 E 1 . X  
 	 	 	 A s s e r t . A r e E q u a l   ( " [ 4 0 0 F ] " ,   e 3 1 F i e l d s [ 1 ] ) ; 	 / / 	 E 1 . Y  
 	 	 	 A s s e r t . A r e E q u a l   ( " [ V 0 0 2 ] " ,   e 3 1 F i e l d s [ 2 ] ) ; 	 / / 	 U 1 . V  
 	 	 	 A s s e r t . A r e E q u a l   ( " [ V 0 0 3 ] " ,   e 3 1 F i e l d s [ 3 ] ) ; 	 / / 	 U 1 . W  
 	 	 }  
 	 	  
 	 	 [ T e s t ]  
 	 	 p u b l i c   v o i d   C h e c k S t r u c t u r e d T y p e M e r g e 3 ( )  
 	 	 {  
 	 	 	 S t r u c t u r e d T y p e   e 1 ;  
 	 	 	 S t r u c t u r e d T y p e   e 2 ;  
 	 	 	 S t r u c t u r e d T y p e   e 3 ;  
  
 	 	 	 t h i s . C r e a t e E n t i t i e s   ( o u t   e 1 ,   o u t   e 2 ,   o u t   e 3 ) ;  
  
 	 	 	 e 2 . F i e l d s . A d d   ( n e w   S t r u c t u r e d T y p e F i e l d   ( n u l l ,   I n t e g e r T y p e . D e f a u l t ,   D r u i d . P a r s e   ( " [ 4 0 0 E ] " ) ,   1 ) ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( S t r i n g T y p e . N a t i v e D e f a u l t ,   e 1 . F i e l d s [ " [ 4 0 0 E ] " ] . T y p e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( S t r i n g T y p e . N a t i v e D e f a u l t ,   e 1 . F i e l d s [ " [ 4 0 0 F ] " ] . T y p e ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( S t r i n g T y p e . N a t i v e D e f a u l t ,   e 2 . F i e l d s [ " [ 4 0 0 G ] " ] . T y p e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( I n t e g e r T y p e . D e f a u l t ,   e 2 . F i e l d s [ " [ 4 0 0 E ] " ] . T y p e ) ;  
  
  
 	 	 	 / / 	 M e r g i n g   t w o   e n t i t i e s   w h e r e   t h e   s e c o n d   o n e   r e d e f i n e s   a   f i e l d   o f   t h e  
 	 	 	 / / 	 f i r s t   o n e   ( f i e l d   [ 4 0 0 E ]   i s   r e d e f i n e d   t o   b e   o f   t y p e   I n t e g e r )   :  
  
 	 	 	 S t r u c t u r e d T y p e   e 1 2   =   S t r u c t u r e d T y p e . M e r g e   ( e 1 ,   e 2 ) ;  
  
 	 	 	 s t r i n g [ ]   e 1 2 F i e l d s   =   C o l l e c t i o n . T o A r r a y   ( e 1 2 . G e t F i e l d I d s   ( ) ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( 3 ,   e 1 2 . F i e l d s . C o u n t ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( " [ 4 0 0 E ] " ,   e 1 2 F i e l d s [ 0 ] ) ; 	 / / 	 E 2 . X   < - -  
 	 	 	 A s s e r t . A r e E q u a l   ( " [ 4 0 0 F ] " ,   e 1 2 F i e l d s [ 1 ] ) ; 	 / / 	 E 1 . Y  
 	 	 	 A s s e r t . A r e E q u a l   ( " [ 4 0 0 G ] " ,   e 1 2 F i e l d s [ 2 ] ) ; 	 / / 	 E 2 . Z  
  
 	 	 	 A s s e r t . A r e E q u a l   ( 0 ,   e 1 2 . F i e l d s [ e 1 2 F i e l d s [ 0 ] ] . R a n k ) ; 	 / / 	 E 1 . X ,   r a n k   =   0  
 	 	 	 A s s e r t . A r e E q u a l   ( 1 ,   e 1 2 . F i e l d s [ e 1 2 F i e l d s [ 1 ] ] . R a n k ) ; 	 / / 	 E 1 . Y ,   r a n k   =   1  
 	 	 	 A s s e r t . A r e E q u a l   ( 2 ,   e 1 2 . F i e l d s [ e 1 2 F i e l d s [ 2 ] ] . R a n k ) ; 	 / / 	 E 2 . Z ,   r a n k   =   2  
  
 	 	 	 A s s e r t . A r e E q u a l   ( I n t e g e r T y p e . D e f a u l t ,   e 1 2 . F i e l d s [ e 1 2 F i e l d s [ 0 ] ] . T y p e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( S t r i n g T y p e . N a t i v e D e f a u l t ,   e 1 2 . F i e l d s [ e 1 2 F i e l d s [ 1 ] ] . T y p e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( S t r i n g T y p e . N a t i v e D e f a u l t ,   e 1 2 . F i e l d s [ e 1 2 F i e l d s [ 2 ] ] . T y p e ) ;  
 	 	 }  
  
  
 	 	 [ T e s t ]  
 	 	 [ E x p e c t e d E x c e p t i o n   ( t y p e o f   ( S y s t e m . A r g u m e n t E x c e p t i o n ) ) ]  
 	 	 p u b l i c   v o i d   C h e c k S t r u c t u r e d T y p e M e r g e E x 1 ( )  
 	 	 {  
 	 	 	 S t r u c t u r e d T y p e   e 1 ;  
 	 	 	 S t r u c t u r e d T y p e   e 2 ;  
 	 	 	 S t r u c t u r e d T y p e   e 3 ;  
 	 	 	  
 	 	 	 t h i s . C r e a t e E n t i t i e s   ( o u t   e 1 ,   o u t   e 2 ,   o u t   e 3 ) ;  
  
 	 	 	 e 1 . S e t V a l u e   ( S t r u c t u r e d T y p e . C l a s s P r o p e r t y ,   S t r u c t u r e d T y p e C l a s s . I n t e r f a c e ) ;  
  
 	 	 	 / / 	 W e   c a n n o t   m e r g e   t w o   e n t i t i e s   o f   d i f f e r e n t   c l a s s e s ;   v e r i f y  
 	 	 	 / / 	 t h a t   t h i s   r a i s e s   t h e   A r g u m e n t E x c e p t i o n   e x c e p t i o n   :  
  
 	 	 	 S t r u c t u r e d T y p e . M e r g e   ( e 1 ,   e 2 ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   C r e a t e E n t i t i e s ( o u t   S t r u c t u r e d T y p e   e 1 ,   o u t   S t r u c t u r e d T y p e   e 2 ,   o u t   S t r u c t u r e d T y p e   e 3 )  
 	 	 {  
 	 	 	 / / 	 M a n u a l l y   c r e a t e   3   e n t i t i e s   b a s e d   o n   c a p t i o n s   s t o r e d   i n   t h e   T e s t  
 	 	 	 / / 	 a n d   O t h e r M o d u l e   m o d u l e s   :  
  
 	 	 	 e 1   =   n e w   S t r u c t u r e d T y p e   ( S t r u c t u r e d T y p e C l a s s . E n t i t y ,   D r u i d . E m p t y ) ;  
 	 	 	 e 2   =   n e w   S t r u c t u r e d T y p e   ( S t r u c t u r e d T y p e C l a s s . E n t i t y ,   D r u i d . E m p t y ) ;  
 	 	 	 e 3   =   n e w   S t r u c t u r e d T y p e   ( S t r u c t u r e d T y p e C l a s s . E n t i t y ,   D r u i d . E m p t y ) ;  
  
 	 	 	 e 1 . D e f i n e C a p t i o n   ( t h i s . m a n a g e r . G e t C a p t i o n   ( D r u i d . P a r s e   ( " [ 4 0 0 C ] " ) ) ) ; 	 / / 	 f r o m   T e s t ,   A p p l i c a t i o n   l a y e r  
 	 	 	 e 2 . D e f i n e C a p t i o n   ( t h i s . m a n a g e r . G e t C a p t i o n   ( D r u i d . P a r s e   ( " [ 4 0 0 D ] " ) ) ) ; 	 / / 	 f r o m   T e s t ,   A p p l i c a t i o n   l a y e r  
 	 	 	 e 3 . D e f i n e C a p t i o n   ( t h i s . m a n a g e r . G e t C a p t i o n   ( D r u i d . P a r s e   ( " [ V 0 0 1 ] " ) ) ) ; 	 / / 	 f r o m   O t h e r M o d u l e ,   U s e r   l a y e r  
  
 	 	 	 A s s e r t . A r e E q u a l   ( R e s o u r c e M o d u l e L a y e r . A p p l i c a t i o n ,   e 1 . M o d u l e . L a y e r ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( R e s o u r c e M o d u l e L a y e r . A p p l i c a t i o n ,   e 2 . M o d u l e . L a y e r ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( R e s o u r c e M o d u l e L a y e r . U s e r ,   e 3 . M o d u l e . L a y e r ) ;  
  
 	 	 	 e 1 . F i e l d s . A d d   ( n e w   S t r u c t u r e d T y p e F i e l d   ( n u l l ,   S t r i n g T y p e . N a t i v e D e f a u l t ,   D r u i d . P a r s e   ( " [ 4 0 0 E ] " ) ,   0 ) ) ;  
 	 	 	 e 1 . F i e l d s . A d d   ( n e w   S t r u c t u r e d T y p e F i e l d   ( n u l l ,   S t r i n g T y p e . N a t i v e D e f a u l t ,   D r u i d . P a r s e   ( " [ 4 0 0 F ] " ) ,   1 ) ) ;  
 	 	 	 e 2 . F i e l d s . A d d   ( n e w   S t r u c t u r e d T y p e F i e l d   ( n u l l ,   S t r i n g T y p e . N a t i v e D e f a u l t ,   D r u i d . P a r s e   ( " [ 4 0 0 G ] " ) ,   0 ) ) ;  
 	 	 	 e 3 . F i e l d s . A d d   ( n e w   S t r u c t u r e d T y p e F i e l d   ( n u l l ,   S t r i n g T y p e . N a t i v e D e f a u l t ,   D r u i d . P a r s e   ( " [ V 0 0 2 ] " ) ,   0 ) ) ;  
 	 	 	 e 3 . F i e l d s . A d d   ( n e w   S t r u c t u r e d T y p e F i e l d   ( n u l l ,   S t r i n g T y p e . N a t i v e D e f a u l t ,   D r u i d . P a r s e   ( " [ V 0 0 3 ] " ) ,   1 ) ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( " E 1 " ,   e 1 . N a m e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " E 2 " ,   e 2 . N a m e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " U 1 " ,   e 3 . N a m e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " X " ,   t h i s . m a n a g e r . G e t C a p t i o n   ( e 1 . G e t F i e l d   ( " [ 4 0 0 E ] " ) . C a p t i o n I d ) . N a m e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " Y " ,   t h i s . m a n a g e r . G e t C a p t i o n   ( e 1 . G e t F i e l d   ( " [ 4 0 0 F ] " ) . C a p t i o n I d ) . N a m e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " Z " ,   t h i s . m a n a g e r . G e t C a p t i o n   ( e 2 . G e t F i e l d   ( " [ 4 0 0 G ] " ) . C a p t i o n I d ) . N a m e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " V " ,   t h i s . m a n a g e r . G e t C a p t i o n   ( e 3 . G e t F i e l d   ( " [ V 0 0 2 ] " ) . C a p t i o n I d ) . N a m e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " W " ,   t h i s . m a n a g e r . G e t C a p t i o n   ( e 3 . G e t F i e l d   ( " [ V 0 0 3 ] " ) . C a p t i o n I d ) . N a m e ) ;  
 	 	 }  
  
 	 	 p r i v a t e   S t r u c t u r e d T y p e   C r e a t e I n t e r f a c e ( )  
 	 	 {  
 	 	 	 S t r u c t u r e d T y p e   i   =   n e w   S t r u c t u r e d T y p e   ( S t r u c t u r e d T y p e C l a s s . I n t e r f a c e ,   D r u i d . E m p t y ) ;  
  
 	 	 	 i . D e f i n e C a p t i o n   ( t h i s . m a n a g e r . G e t C a p t i o n   ( D r u i d . P a r s e   ( " [ 4 0 0 H ] " ) ) ) ; 	 / / 	 f r o m   T e s t ,   A p p l i c a t i o n   l a y e r  
  
 	 	 	 A s s e r t . A r e E q u a l   ( R e s o u r c e M o d u l e L a y e r . A p p l i c a t i o n ,   i . M o d u l e . L a y e r ) ;  
  
 	 	 	 i . F i e l d s . A d d   ( n e w   S t r u c t u r e d T y p e F i e l d   ( n u l l ,   S t r i n g T y p e . N a t i v e D e f a u l t ,   D r u i d . P a r s e   ( " [ 4 0 0 I ] " ) ,   0 ) ) ;  
 	 	 	 i . F i e l d s . A d d   ( n e w   S t r u c t u r e d T y p e F i e l d   ( n u l l ,   S t r i n g T y p e . N a t i v e D e f a u l t ,   D r u i d . P a r s e   ( " [ 4 0 0 J ] " ) ,   1 ) ) ;  
  
 	 	 	 A s s e r t . A r e E q u a l   ( " I " ,   i . N a m e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " I f 1 " ,   t h i s . m a n a g e r . G e t C a p t i o n   ( i . G e t F i e l d   ( " [ 4 0 0 I ] " ) . C a p t i o n I d ) . N a m e ) ;  
 	 	 	 A s s e r t . A r e E q u a l   ( " I f 2 " ,   t h i s . m a n a g e r . G e t C a p t i o n   ( i . G e t F i e l d   ( " [ 4 0 0 J ] " ) . C a p t i o n I d ) . N a m e ) ;  
  
 	 	 	 r e t u r n   i ;  
 	 	 }  
  
  
  
 	 	 p r i v a t e   v o i d   H a n d l e D a t a P r o p e r t y C h a n g e d ( o b j e c t   s e n d e r ,   D e p e n d e n c y P r o p e r t y C h a n g e d E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . b u f f e r . A p p e n d   ( " [ " ) ;  
 	 	 	 t h i s . b u f f e r . A p p e n d   ( e . P r o p e r t y N a m e ) ;  
 	 	 	 t h i s . b u f f e r . A p p e n d   ( " : " ) ;  
 	 	 	 t h i s . b u f f e r . A p p e n d   ( e . O l d V a l u e ) ;  
 	 	 	 t h i s . b u f f e r . A p p e n d   ( " - > " ) ;  
 	 	 	 t h i s . b u f f e r . A p p e n d   ( e . N e w V a l u e ) ;  
 	 	 	 t h i s . b u f f e r . A p p e n d   ( " ] " ) ;  
 	 	 }  
  
 	 	 p r i v a t e   S y s t e m . T e x t . S t r i n g B u i l d e r   b u f f e r ;  
 	 	 p r i v a t e   R e s o u r c e M a n a g e r   m a n a g e r ;  
  
 	 	 p r i v a t e   s t a t i c   v o i d   F i l l ( S t r u c t u r e d T y p e   r e c o r d )  
 	 	 {  
 	 	 	 S t r u c t u r e d T y p e   s u b R e c 1   =   n e w   S t r u c t u r e d T y p e   ( ) ;  
 	 	 	 S t r u c t u r e d T y p e   s u b R e c 2   =   n e w   S t r u c t u r e d T y p e   ( ) ;  
  
 	 	 	 r e c o r d . F i e l d s . A d d   ( " N u m b e r 1 " ,   n e w   D e c i m a l T y p e   ( ) ) ;  
 	 	 	 r e c o r d . F i e l d s . A d d   ( " T e x t 1 " ,   n e w   S t r i n g T y p e   ( ) ) ;  
 	 	 	 r e c o r d . F i e l d s . A d d   ( " N u m b e r 2 " ,   n e w   D e c i m a l T y p e   ( n e w   D e c i m a l R a n g e   ( 0 . 0 M ,   9 9 9 . 9 M ,   0 . 1 M ) ) ) ;  
  
 	 	 	 r e c o r d . F i e l d s . A d d   ( " P e r s o n n e " ,   s u b R e c 1 ) ;  
  
 	 	 	 s u b R e c 1 . F i e l d s . A d d   ( " N o m " ,   n e w   S t r i n g T y p e   ( ) ) ;  
 	 	 	 s u b R e c 1 . F i e l d s . A d d   ( " P r é n o m " ,   n e w   S t r i n g T y p e   ( ) ) ;  
 	 	 	 s u b R e c 1 . F i e l d s . A d d   ( " A d r e s s e " ,   s u b R e c 2 ) ;  
  
 	 	 	 s u b R e c 2 . F i e l d s . A d d   ( " N P A " ,   n e w   I n t e g e r T y p e   ( 1 ,   9 9 9 9 9 9 ) ) ;  
 	 	 	 s u b R e c 2 . F i e l d s . A d d   ( " V i l l e " ,   n e w   S t r i n g T y p e   ( ) ) ;  
 	 	 }  
 	 }  
 }  
 