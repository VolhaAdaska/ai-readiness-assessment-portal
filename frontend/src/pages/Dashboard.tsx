import React from 'react';
import { Link } from 'react-router-dom';
import { Card, CardHeader, CardTitle, CardContent } from '../components/common/Card';
import { Button } from '../components/common/Button';

export const Dashboard: React.FC = () => {
  return (
    <div className="space-y-6">
      <div className="flex justify-between items-center">
        <h2 className="text-2xl font-bold text-gray-900">Dashboard</h2>
        <Link to="/create">
          <Button>New Assessment</Button>
        </Link>
      </div>

      <Card>
        <CardHeader>
          <CardTitle>Initial Assessment MVP</CardTitle>
        </CardHeader>
        <CardContent>
          <p className="text-gray-600 mb-4">
            Start a new baseline assessment for an organization and progress through category scoring.
          </p>
          <p className="text-sm text-gray-500">
            Existing assessment listing is intentionally out of scope for this MVP shell.
          </p>
        </CardContent>
      </Card>
    </div>
  );
};