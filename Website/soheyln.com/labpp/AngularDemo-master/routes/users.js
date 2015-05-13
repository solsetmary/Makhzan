var mysql      = require('mysql');
var connection = mysql.createConnection({
  host     : 'localhost',
  user     : 'soheyln_com',
  password : 'soheyln1357',
  database: 'soheyln_com'
});


exports.getUser = function(req, res) {
	var user = req.body;
	
	//connection.query('SELECT * FROM user_s WHERE username = ? and password = md5(?)', [user.username, user.password], function(err, results) {
	connection.query('SELECT * FROM user_s WHERE username = ?', [user.username], function(err, results) {
	  if (err) throw err;
	  
	  if (results != '')
	  {
		  var response = { success: results[0].username };
		  res.send(response);
	  }
	  else
      {
		  var response = { error: '' };
	      res.send(response);
	  }	
	});
};
