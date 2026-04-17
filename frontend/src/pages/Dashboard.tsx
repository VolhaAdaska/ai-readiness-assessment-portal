import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { Card, CardHeader, CardTitle, CardContent } from '../components/common/Card';
import { Button } from '../components/common/Button';
import { Loading } from '../components/common/Loading';
import { Error } from '../components/common/Error';
import { assessmentApi } from '../api/client';
import { AssessmentStatus, getAssessmentStatusLabel, getDashboardReadinessLabel } from '../types/assessment';
import type { AssessmentSummary } from '../types/assessment';

export const Dashboard: React.FC = () => {
  const [assessments, setAssessments] = useState<AssessmentSummary[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchAssessments = async () => {
      try {
        const response = await assessmentApi.list();
        setAssessments(response);
      } catch (err) {
        const message = err instanceof globalThis.Error ? err.message : 'Failed to load assessments';
        setError(message);
      } finally {
        setLoading(false);
      }
    };

    fetchAssessments();
  }, []);

  const formatShortId = (id: string): string => id.split('-')[0] ?? id;

  const statusBadgeClass = (status: AssessmentStatus): string => {
    if (status === AssessmentStatus.Completed) {
      return 'bg-emerald-50 text-emerald-700 ring-1 ring-emerald-200';
    }

    if (status === AssessmentStatus.InProgress) {
      return 'bg-indigo-50 text-indigo-700 ring-1 ring-indigo-200';
    }

    return 'bg-slate-100 text-slate-700 ring-1 ring-slate-200';
  };

  if (loading) {
    return (
      <div className="rounded-2xl border border-slate-200 bg-white py-20 shadow-sm">
        <Loading size="lg" />
      </div>
    );
  }

  if (error) {
    return (
      <div className="space-y-7">
        <div className="flex flex-col gap-3 sm:flex-row sm:items-center sm:justify-between">
          <h2 className="text-3xl font-semibold tracking-tight text-slate-900">Dashboard</h2>
          <Link to="/create">
            <Button>New Assessment</Button>
          </Link>
        </div>
        <Error message={error} />
      </div>
    );
  }

  return (
    <div className="space-y-7">
      <div className="flex flex-col gap-3 sm:flex-row sm:items-center sm:justify-between">
        <div>
          <h2 className="text-3xl font-semibold tracking-tight text-slate-900 sm:text-4xl">Dashboard</h2>
          <p className="mt-1.5 text-sm text-slate-600">Track progress across active and completed assessments.</p>
        </div>
        <Link to="/create">
          <Button>New Assessment</Button>
        </Link>
      </div>

      {assessments.length === 0 ? (
        <Card>
          <CardHeader>
            <CardTitle>No Assessments Yet</CardTitle>
          </CardHeader>
          <CardContent>
            <p className="mb-4 text-sm leading-6 text-slate-600">
              Start a baseline assessment to track AI readiness progress for your organization.
            </p>
            <Link to="/create">
              <Button>Create First Assessment</Button>
            </Link>
          </CardContent>
        </Card>
      ) : (
        <div className="grid grid-cols-1 gap-6 lg:grid-cols-2">
          {assessments.map((assessment) => {
            const isCompleted = assessment.status === AssessmentStatus.Completed;
            const organizationName = assessment.organizationName?.trim();

            return (
              <Card key={assessment.assessmentId}>
                <CardHeader className="flex flex-row items-start justify-between gap-3">
                  <div>
                    <CardTitle className="text-xl sm:text-2xl">Assessment {formatShortId(assessment.assessmentId)}</CardTitle>
                    <p className="mt-1 text-xs font-medium uppercase tracking-wider text-slate-500">{assessment.assessmentId}</p>
                  </div>
                  <span className={`rounded-full px-3 py-1 text-xs font-semibold ${statusBadgeClass(assessment.status)}`}>
                    {getAssessmentStatusLabel(assessment.status)}
                  </span>
                </CardHeader>
                <CardContent className="space-y-5">
                  <div className="grid grid-cols-1 gap-4 text-sm sm:grid-cols-2">
                    <div>
                      <p className="text-xs font-semibold uppercase tracking-wider text-slate-500">Organization</p>
                      <p className="mt-1 text-sm font-semibold text-slate-900 break-all">
                        {organizationName || 'Organization unavailable'}
                      </p>
                      {!organizationName && (
                        <p className="mt-1 text-xs text-slate-500 break-all">ID: {assessment.organizationId}</p>
                      )}
                    </div>
                    <div>
                      <p className="text-xs font-semibold uppercase tracking-wider text-slate-500">Created</p>
                      <p className="mt-1 text-sm font-semibold text-slate-900">
                        {new Date(assessment.createdAt).toLocaleDateString()}
                      </p>
                    </div>
                    <div>
                      <p className="text-xs font-semibold uppercase tracking-wider text-slate-500">Readiness Level</p>
                      <p className="mt-1 text-sm font-semibold text-slate-900">
                        {getDashboardReadinessLabel(assessment.readinessLevel)}
                      </p>
                    </div>
                    <div>
                      <p className="text-xs font-semibold uppercase tracking-wider text-slate-500">Overall Score</p>
                      <p className="mt-1 text-sm font-semibold text-slate-900">
                        {assessment.overallScore === undefined ? 'N/A' : `${Math.round(assessment.overallScore)}%`}
                      </p>
                    </div>
                  </div>

                  <div className="rounded-xl border border-slate-200 bg-slate-50/70 p-3.5">
                    <div className="mb-2 flex items-center justify-between text-sm">
                      <span className="text-slate-600">
                        Categories: {assessment.completedCategories}/{assessment.totalCategories}
                      </span>
                      <span className="font-semibold text-slate-800">{assessment.completionPercentage}%</span>
                    </div>
                    <div className="h-2.5 w-full overflow-hidden rounded-full bg-slate-200">
                      <div
                        className="h-full rounded-full bg-indigo-600/90 transition-all duration-300"
                        style={{ width: `${assessment.completionPercentage}%` }}
                      />
                    </div>
                  </div>

                  <div className="pt-1">
                    <Link
                      to={
                        isCompleted
                          ? `/assessment/${assessment.assessmentId}/complete`
                          : `/assessment/${assessment.assessmentId}`
                      }
                    >
                      <Button className="w-full sm:w-auto" variant={isCompleted ? 'secondary' : 'primary'}>
                        {isCompleted ? 'View Summary' : 'Continue Assessment'}
                      </Button>
                    </Link>
                  </div>
                </CardContent>
              </Card>
            );
          })}
        </div>
      )}
    </div>
  );
};