ÿþ/ / 	 C o p y r i g h t   ©   2 0 0 3 - 2 0 0 8 ,   E P S I T E C   S A ,   C H - 1 0 9 2   B E L M O N T ,   S w i t z e r l a n d  
 / / 	 A u t h o r :   D a n i e l   R O U X ,   M a i n t a i n e r :   D a n i e l   R O U X  
  
 u s i n g   E p s i t e c . C o m m o n . W i d g e t s ;  
 u s i n g   E p s i t e c . C o m m o n . S u p p o r t ;  
 u s i n g   E p s i t e c . C o m m o n . D r a w i n g ;  
  
 n a m e s p a c e   E p s i t e c . A p p . D o l p h i n . D i a l o g s  
 {  
 	 / / /   < s u m m a r y >  
 	 / / /   D i a l o g u e   " à   p o r p o s   d e " .  
 	 / / /   < / s u m m a r y >  
 	 p u b l i c   c l a s s   A b o u t   :   A b s t r a c t  
 	 {  
 	 	 p u b l i c   A b o u t ( D o l p h i n A p p l i c a t i o n   a p p l i c a t i o n )   :   b a s e ( a p p l i c a t i o n )  
 	 	 {  
 	 	 }  
  
 	 	 p u b l i c   o v e r r i d e   v o i d   S h o w ( )  
 	 	 {  
 	 	 	 / / 	 C r é e   e t   m o n t r e   l a   f e n ê t r e   d u   d i a l o g u e .  
 	 	 	 i f   (   t h i s . w i n d o w   = =   n u l l   )  
 	 	 	 {  
 	 	 	 	 t h i s . w i n d o w   =   n e w   W i n d o w ( ) ;  
 	 	 	 	 t h i s . w i n d o w . M a k e F i x e d S i z e W i n d o w ( ) ;  
 	 	 	 	 t h i s . w i n d o w . M a k e S e c o n d a r y W i n d o w ( ) ;  
 	 	 	 	 t h i s . w i n d o w . P r e v e n t A u t o C l o s e   =   t r u e ;  
 	 	 	 	 t h i s . W i n d o w I n i t ( " A b o u t " ,   3 5 0 ,   2 0 0 ,   t r u e ) ;  
 	 	 	 	 t h i s . w i n d o w . T e x t   =   T e x t L a y o u t . C o n v e r t T o S i m p l e T e x t ( R e s . S t r i n g s . W i n d o w . T i t l e ) ;  
 	 	 	 	 t h i s . w i n d o w . O w n e r   =   t h i s . a p p l i c a t i o n . W i n d o w ;  
 	 	 	 	 t h i s . w i n d o w . I c o n   =   B i t m a p . F r o m M a n i f e s t R e s o u r c e ( " E p s i t e c . A p p . D o l p h i n . I m a g e s . A p p l i c a t i o n . i c o n " ,   t y p e o f ( D o l p h i n A p p l i c a t i o n ) . A s s e m b l y ) ;  
 	 	 	 	 t h i s . w i n d o w . W i n d o w C l o s e C l i c k e d   + =   n e w   E v e n t H a n d l e r ( t h i s . H a n d l e W i n d o w I n f o s C l o s e C l i c k e d ) ;  
  
 	 	 	 	 S y s t e m . T e x t . S t r i n g B u i l d e r   b u i l d e r   =   n e w   S y s t e m . T e x t . S t r i n g B u i l d e r ( ) ;  
 	 	 	 	 b u i l d e r . A p p e n d ( M i s c . B o l d ( M i s c . F o n t S i z e ( D o l p h i n A p p l i c a t i o n . A p p l i c a t i o n T i t l e ,   1 5 0 ) ) ) ;  
 	 	 	 	 b u i l d e r . A p p e n d ( " < b r / > " ) ;  
 	 	 	 	 b u i l d e r . A p p e n d ( " < b r / > " ) ;  
 	 	 	 	 s t r i n g   w e b 1   =   s t r i n g . F o r m a t ( " < a   h r e f = \ " h t t p : / / { 0 } \ " > { 0 } < / a > " ,   R e s . S t r i n g s . D i a l o g . A b o u t . W e b . E p s i t e c ) ;  
 	 	 	 	 s t r i n g   w e b 2   =   s t r i n g . F o r m a t ( " < a   h r e f = \ " h t t p : / / { 0 } \ " > { 0 } < / a > " ,   R e s . S t r i n g s . D i a l o g . A b o u t . W e b . D a u p h i n ) ;  
 	 	 	 	 b u i l d e r . A p p e n d ( s t r i n g . F o r m a t ( R e s . S t r i n g s . D i a l o g . A b o u t . M e s s a g e ,   w e b 1 ,   w e b 2 ,   M i s c . G e t V e r s i o n ( ) ) ) ;  
  
 	 	 	 	 S t a t i c T e x t   t e x t   =   n e w   S t a t i c T e x t ( t h i s . w i n d o w . R o o t ) ;  
 	 	 	 	 t e x t . T e x t   =   b u i l d e r . T o S t r i n g ( ) ;  
 	 	 	 	 t e x t . C o n t e n t A l i g n m e n t   =   C o n t e n t A l i g n m e n t . T o p L e f t ;  
 	 	 	 	 t e x t . D o c k   =   D o c k S t y l e . F i l l ;  
 	 	 	 	 t e x t . M a r g i n s   =   n e w   M a r g i n s ( 1 0 ,   1 0 ,   1 0 ,   1 0 ) ;  
 	 	 	 	 t e x t . H y p e r t e x t C l i c k e d   + =   A b s t r a c t . H a n d l e L i n k H y p e r t e x t C l i c k e d ;  
  
 	 	 	 	 / / 	 B o u t o n   d e   f e r m e t u r e .  
 	 	 	 	 B u t t o n   b u t t o n C l o s e   =   n e w   B u t t o n ( t h i s . w i n d o w . R o o t ) ;  
 	 	 	 	 b u t t o n C l o s e . P r e f e r r e d W i d t h   =   7 5 ;  
 	 	 	 	 b u t t o n C l o s e . T e x t   =   R e s . S t r i n g s . D i a l o g . O K . B u t t o n ;  
 	 	 	 	 b u t t o n C l o s e . B u t t o n S t y l e   =   B u t t o n S t y l e . D e f a u l t A c c e p t A n d C a n c e l ;  
 	 	 	 	 b u t t o n C l o s e . A n c h o r   =   A n c h o r S t y l e s . B o t t o m R i g h t ;  
 	 	 	 	 b u t t o n C l o s e . M a r g i n s   =   n e w   M a r g i n s ( 0 ,   1 0 ,   0 ,   1 0 ) ;  
 	 	 	 	 b u t t o n C l o s e . C l i c k e d   + =   t h i s . H a n d l e I n f o s B u t t o n C l o s e C l i c k e d ;  
 	 	 	 	 b u t t o n C l o s e . T a b I n d e x   =   1 0 0 0 ;  
 	 	 	 	 b u t t o n C l o s e . T a b N a v i g a t i o n M o d e   =   T a b N a v i g a t i o n M o d e . A c t i v a t e O n T a b ;  
 	 	 	 }  
  
 	 	 	 t h i s . w i n d o w . S h o w D i a l o g ( ) ;  
 	 	 }  
  
 	 	 p u b l i c   o v e r r i d e   v o i d   S a v e ( )  
 	 	 {  
 	 	 	 / / 	 E n r e g i s t r e   l a   p o s i t i o n   d e   l a   f e n ê t r e   d u   d i a l o g u e .  
 	 	 	 t h i s . W i n d o w S a v e ( " A b o u t " ) ;  
 	 	 }  
  
  
 	 	 p r i v a t e   v o i d   H a n d l e W i n d o w I n f o s C l o s e C l i c k e d ( o b j e c t   s e n d e r )  
 	 	 {  
 	 	 	 t h i s . C l o s e W i n d o w ( ) ;  
 	 	 }  
  
 	 	 p r i v a t e   v o i d   H a n d l e I n f o s B u t t o n C l o s e C l i c k e d ( o b j e c t   s e n d e r ,   M e s s a g e E v e n t A r g s   e )  
 	 	 {  
 	 	 	 t h i s . C l o s e W i n d o w ( ) ;  
 	 	 }  
 	 }  
 }  
 