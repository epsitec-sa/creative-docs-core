ÿþ/ / 	 C o p y r i g h t   ©   2 0 1 0 ,   E P S I T E C   S A ,   C H - 1 4 0 0   Y v e r d o n - l e s - B a i n s ,   S w i t z e r l a n d  
 / / 	 A u t h o r :   D a n i e l   R O U X ,   M a i n t a i n e r :   D a n i e l   R O U X  
  
 u s i n g   S y s t e m . C o l l e c t i o n s . G e n e r i c ;  
  
 n a m e s p a c e   E p s i t e c . C r e s u s . C o r e . T a b l e D e s i g n e r  
 {  
 	 p u b l i c   c l a s s   D e s i g n e r V a l u e s  
 	 {  
 	 	 p u b l i c   D e s i g n e r V a l u e s ( )  
 	 	 {  
 	 	 	 t h i s . v a l u e s   =   n e w   D i c t i o n a r y < s t r i n g ,   d e c i m a l >   ( ) ;  
 	 	 }  
  
  
 	 	 p u b l i c   D i c t i o n a r y < s t r i n g ,   d e c i m a l >   D a t a  
 	 	 {  
 	 	 	 g e t  
 	 	 	 {  
 	 	 	 	 r e t u r n   t h i s . v a l u e s ;  
 	 	 	 }  
 	 	 }  
  
 	 	 p u b l i c   v o i d   C l e a r ( )  
 	 	 {  
 	 	 	 t h i s . v a l u e s . C l e a r   ( ) ;  
 	 	 }  
  
  
 	 	 p u b l i c   d e c i m a l ?   G e t V a l u e ( i n t [ ]   i n d e x e s )  
 	 	 {  
 	 	 	 s t r i n g   k e y   =   D e s i g n e r V a l u e s . G e t K e y   ( i n d e x e s ) ;  
  
 	 	 	 i f   ( t h i s . v a l u e s . C o n t a i n s K e y   ( k e y ) )  
 	 	 	 {  
 	 	 	 	 r e t u r n   t h i s . v a l u e s [ k e y ] ;  
 	 	 	 }  
  
 	 	 	 r e t u r n   n u l l ;  
 	 	 }  
  
 	 	 p u b l i c   v o i d   S e t V a l u e ( i n t [ ]   i n d e x e s ,   d e c i m a l ?   v a l u e )  
 	 	 {  
 	 	 	 s t r i n g   k e y   =   D e s i g n e r V a l u e s . G e t K e y   ( i n d e x e s ) ;  
  
 	 	 	 i f   ( v a l u e   = =   n u l l )  
 	 	 	 {  
 	 	 	 	 i f   ( t h i s . v a l u e s . C o n t a i n s K e y   ( k e y ) )  
 	 	 	 	 {  
 	 	 	 	 	 t h i s . v a l u e s . R e m o v e   ( k e y ) ;  
 	 	 	 	 }  
 	 	 	 }  
 	 	 	 e l s e  
 	 	 	 {  
 	 	 	 	 t h i s . v a l u e s [ k e y ]   =   v a l u e . V a l u e ;  
 	 	 	 }  
 	 	 }  
  
  
 	 	 p r i v a t e   s t a t i c   s t r i n g   G e t K e y ( i n t [ ]   i n d e x e s )  
 	 	 {  
 	 	 	 s t r i n g [ ]   l i s t   =   n e w   s t r i n g [ i n d e x e s . L e n g t h ] ;  
  
 	 	 	 f o r   ( i n t   i   =   0 ;   i   <   l i s t . L e n g t h ;   i + + )  
 	 	 	 {  
 	 	 	 	 l i s t [ i ]   =   i n d e x e s [ i ] . T o S t r i n g   ( S y s t e m . G l o b a l i z a t i o n . C u l t u r e I n f o . I n v a r i a n t C u l t u r e ) ;  
 	 	 	 }  
  
 	 	 	 r e t u r n   s t r i n g . J o i n   ( " . " ,   l i s t ) ;  
 	 	 }  
  
  
 	 	 p r i v a t e   r e a d o n l y   D i c t i o n a r y < s t r i n g ,   d e c i m a l > 	 	 	 v a l u e s ;  
 	 }  
 }  
 