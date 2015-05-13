#/usr/bin/perl -e
use IO::Socket;
print "perl is starting";
my $s = new IO::Socket::INET (Listen=>10, LocalHost=>"0.0.0.0", LocalPort=>2000, Proto=>"tcp", Reuse=>1);
$s->autoflush(1);
if( my $c = $s->accept() )
{
	if( sysread($c, my $byte, 1) )
	{
		print("A: \"$byte\"\n"); 
	}
	$c->flush();  
	if( sysread($c, my $byte, 1) )
	{
		print("B: \"$byte\"\n"); 
	}
} 
close($s);

1;